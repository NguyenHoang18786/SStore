using Microsoft.AspNet.Identity;
using SStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PayPal.Api;

namespace SStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private string Cart = "Cart";
        // GET: ShoppingCart
        public ActionResult Index()
        {
            var item = 0;
            List<Cart> lsCart = (List<Cart>)Session[Cart];
            if (Session[Cart] != null)
            {
                item = lsCart.Sum(s => s.Quantity);
            }
            Session[Cart] = lsCart;
            ViewBag.infoCart = item;
            return View();
        }
        public ActionResult OrderNow(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session[Cart] == null)
            {
                List<Cart> lsCart = new List<Cart>
                {
                    new Cart(db.Products.Find(id),1)
                };
                Session[Cart] = lsCart;
            }
            else
            {
                Session.Remove(Cart);
                List<Cart> lsCart = new List<Cart>
                {
                    new Cart(db.Products.Find(id),1)
                };
                Session[Cart] = lsCart;
            }
            return RedirectToAction("CashPayment");
        }
        public ActionResult AddToCart(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session[Cart] == null)
            {
                List<Cart> lsCart = new List<Cart>
                {
                    new Cart(db.Products.Find(id),1)
                };
                Session[Cart] = lsCart;
            }
            else
            {
                List<Cart> lsCart = (List<Cart>)Session[Cart];
                int check = isExisting(id);
                if (check == -1)
                    lsCart.Add(new Cart(db.Products.Find(id), 1));
                else
                    lsCart[check].Quantity++;
                Session[Cart] = lsCart;
            }
            ViewBag.Message = String.Format("Successfully sent feedback!");
            return View("Index");
        }
        private int isExisting(int? id)
        {
            List<Cart> lsCart = (List<Cart>)Session[Cart];
            for (int i = 0; i < lsCart.Count; i++)
            {
                if (lsCart[i].Product.Id == id) return i;
            }
            return -1;
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int check = isExisting((id));
            List<Cart> lsCart = (List<Cart>)Session[Cart];
            lsCart.RemoveAt(check);
            return RedirectToAction("Index");
        }
        public ActionResult UpdateCart(FormCollection formCollection)
        {
            List<Cart> lsCart = (List<Cart>)Session[Cart];
            int productId = int.Parse(formCollection["productId"]);
            int quantity = int.Parse(formCollection["Quantity"]);
            var item = lsCart.Find(s => s.Product.Id == productId);
            if (item != null)
            {
                item.Quantity = quantity;
            };

            Session[Cart] = lsCart;
            return RedirectToAction("Index");
        }
        public PartialViewResult BagCart()
        {
            var item = 0;
            List<Cart> lsCart = (List<Cart>)Session[Cart];
            if (Session[Cart] != null)
            {
                item = lsCart.Sum(s => s.Quantity);
            }
            Session[Cart] = lsCart;
            ViewBag.infoCart = item;
            return PartialView();
        }
        public ActionResult CashPayment()
        {
            var itemCount = 0;
            List<Cart> lsCart = (List<Cart>)Session[Cart];
            if (Session[Cart] != null)
            {
                itemCount = lsCart.Sum(s => s.Quantity);
            }
            Session[Cart] = lsCart;
            ViewBag.CartItemCount = itemCount;
            return View();
        }
        [HttpPost]
        public ActionResult CashPayment(Models.Order order)
        {
            List<Cart> lsCart = (List<Cart>)Session[Cart];
            //Save the order into Order table
            if (ModelState.IsValid)
            {
                order.OrderDate = DateTime.Now;
                order.Status = 0;
                order.UserId = User.Identity.GetUserId();
                order.PaymentType = "Cash";
                order.PaymentStatus = 0;
                db.Orders.Add(order);
                db.SaveChanges();
                //Save the order detail into Order Detail table
                foreach (Cart cart in lsCart)
                {
                    OrderDetail orderDetail = new OrderDetail()
                    {
                        OrderId = order.OrderId,
                        ProductId = cart.Product.Id,
                        Quantity = cart.Quantity,
                        Price = cart.Product.Price
                    };
                    db.OrderDetails.Add(orderDetail);
                    db.SaveChanges();
                }
                Session.Remove(Cart);
                return View("OrderSuccess");
            };
            return View();
        }

        // Work with Paypal 
        private Payment payment;

        // Create a payment
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            var listItems = new ItemList() { items = new List<Item>() };

            List<Cart> listCarts = (List<Cart>)Session[Cart];
            foreach (var cart in listCarts)
            {
                listItems.items.Add(new Item()
                {
                    name = cart.Product.ProductName,
                    currency = "USD",
                    price = cart.Product.Price.ToString(),
                    quantity = cart.Quantity.ToString(),
                    sku = "sku"
                });
            }

            var payer = new Payer() { payment_method = "paypal" };

            var redirUrls = new RedirectUrls()
            {
                cancel_url = "http://sstore.local/",
                return_url = redirectUrl
            };

            // Create details object
            var details = new Details()
            {
                tax = "5",
                shipping = "2",
                subtotal = listCarts.Sum(x => x.Quantity * x.Product.Price).ToString()
            };

            // Create amount object
            var amount = new Amount()
            {
                currency = "USD",
                total = (Convert.ToDecimal(details.tax) + Convert.ToDecimal(details.shipping) + Convert.ToDecimal(details.subtotal)).ToString(),
                details = details
            };

            // Create transaction
            var transactionList = new List<Transaction>();
            transactionList.Add(new Transaction()
            {
                description = "Sstore transaction description",
                invoice_number = Convert.ToString((new Random()).Next(100000)),
                amount = amount,
                item_list = listItems
            });

            payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            return payment.Create(apiContext);
        }

        // Create Execute Payment method
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            payment = new Payment() { id = paymentId };
            return payment.Execute(apiContext, paymentExecution);
        }

        // Payment With Paypal action
        public ActionResult PaymentWithPaypal()
        {
            // Getting context from the paypal bases on clientId and clientSecret 
            APIContext apiContext = PaypalConfiguration.GetAPIContext();

            try
            {
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    // Create a payment
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/ShoppingCart/PaymentWithPaypal?";
                    var guid = Convert.ToString((new Random()).Next(100000));
                    var createdPayment = CreatePayment(apiContext, baseURI + "guid=" + guid);

                    // Get links returned from paypal response to create call function
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = string.Empty;

                    while (links.MoveNext())
                    {
                        Links link = links.Current;
                        if (link.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            paypalRedirectUrl = link.href;
                        }
                    }

                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This one will execute when we have received all the payment params from previous call
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("Failure");
                    }
                }
            }
            catch (Exception ex)
            {
                PaypalLogger.Log("Error: " + ex.Message);
                return View("Failure");
            }
            Session.Remove(Cart);
            return View("OrderSuccess");
        }
    }
}
