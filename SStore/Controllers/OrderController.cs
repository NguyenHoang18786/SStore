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
using System.IO;
using Microsoft.AspNet.Identity;

namespace SStore.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Order
        public ActionResult Index(int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var userId = User.Identity.GetUserId();
            var orders = db.Orders.OrderByDescending(o => o.OrderDate).Where(o => o.UserId == userId).ToList();
            return View(orders.ToPagedList(pageNumber, pageSize));
        }
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
    }
}