using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SStore.Models;
using PagedList;
using PagedList.Mvc;

namespace SStore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Manager")]
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Orders
        public ActionResult Index(int? page, string sortOrder, string orderIdSearch)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.OrdDateSortParm = String.IsNullOrEmpty(sortOrder) ? "Date" : "";
            ViewBag.StatusSortParm = sortOrder == "Status" ? "Status_Desc" : "Status";
            ViewBag.PaymentStatusSortParm = sortOrder == "PayStatus" ? "PayStatus_Desc" : "PayStatus";
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            var orders = db.Orders.ToList();

            switch (sortOrder)
            {
                case "Date":
                    orders = db.Orders.OrderBy(p => p.OrderDate).ToList();
                    break;
                case "Status":
                    orders = db.Orders.OrderBy(p => p.Status).ToList();
                    break;
                case "Status_Desc":
                    orders = db.Orders.OrderByDescending(p => p.Status).ToList();
                    break;
                case "PayStatus":
                    orders = db.Orders.OrderBy(p => p.PaymentStatus).ToList();
                    break;
                case "PayStatus_Desc":
                    orders = db.Orders.OrderByDescending(p => p.PaymentStatus).ToList();
                    break;
                default:
                    orders = db.Orders.OrderByDescending(p => p.OrderDate).ToList();
                    break;
            }
            if (!String.IsNullOrEmpty(orderIdSearch))
            {
                orders = orders.Where(p => p.OrderId.ToString().Equals(orderIdSearch.ToString())).ToList();
                if (orders.Count() > 0)
                {
                    return View(orders.ToPagedList(pageNumber, pageSize));
                }
                else
                {
                    return RedirectToAction("NotFound");
                }
            }
            return View(orders.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult NotFound()
        {
            return View();
        }

        // GET: Admin/Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order = db.Orders.Find(id);
            var orderDetails = db.OrderDetails.Include(o => o.Product).Where(o => o.OrderId == id).ToList();
            ViewBag.OrderDetails = orderDetails;
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            var orderDetails = db.OrderDetails.Include(o => o.Product).Where(o => o.OrderId == id).ToList();
            ViewBag.OrderDetails = orderDetails;
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: /Movies/Edit/5
        [HttpPost]
        public ActionResult Edit(Order order)
        {
            var orderDetails = db.OrderDetails.Include(o => o.Product).Where(o => o.OrderId == order.OrderId).ToList();
            ViewBag.OrderDetails = orderDetails;
            if (ModelState.IsValid)
            {
                var orderInDb = db.Orders.SingleOrDefault(o => o.OrderId.Equals(order.OrderId));

                orderInDb.PaymentStatus = order.PaymentStatus;
                orderInDb.Status = order.Status;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = order.OrderId });
            }
            return View(order);
        }

        // GET: Admin/Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            var orderDetails = db.OrderDetails.Include(o => o.Product).Where(o => o.OrderId == id).ToList();
            ViewBag.OrderDetails = orderDetails;
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
