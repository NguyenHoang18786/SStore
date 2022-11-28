using SStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SStore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Manager")]
    public class DashboardController : Controller
    {
        private ApplicationDbContext db;
        private UserManager<ApplicationUser> _userManager;

        public DashboardController()
        {
            db = new ApplicationDbContext();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        }
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult GetProductView()
        {
            var products = db.Products.Take(5).OrderByDescending(p => p.View);
            var listProducts = new List<Product>();
            foreach (var product in products)
            {
                listProducts.Add(product);
            }
            return PartialView(listProducts);
        }
        public PartialViewResult GetOrderByWeek()
        {
            /* Get this week */
            DateTime today = DateTime.Now.AddDays(-1);
            DateTime secondDay = today.AddDays(-1);
            DateTime thirtDay = secondDay.AddDays(-1);
            DateTime fourthDay = thirtDay.AddDays(-1);
            DateTime fifthDay = fourthDay.AddDays(-1);
            DateTime sixthDay = fifthDay.AddDays(-1);
            DateTime seventhDay = fifthDay.AddDays(-1);
            /* Get days name */
            var TodayName = DateTime.Now.ToString("ddd");
            var secondDayName = today.ToString("ddd");
            var thirtDayName = secondDay.ToString("ddd");
            var fourthDayName = thirtDay.ToString("ddd");
            var fifthDayName = fourthDay.ToString("ddd");
            var sixthDayName = fifthDay.ToString("ddd");
            var seventhDayName = seventhDay.ToString("ddd");
            /* Pass days name to view */
            ViewBag.TodayName = TodayName;
            ViewBag.SecondDayName = secondDayName;
            ViewBag.ThirtDayName = thirtDayName;
            ViewBag.FourthDayName = fourthDayName;
            ViewBag.FifthDayName = fifthDayName;
            ViewBag.SixthDayName = sixthDayName;
            ViewBag.SeventhDayName = seventhDayName;
            /* Get orders total value of each day */
            var todayOrders = db.Orders.Where(u => u.OrderDate <= DateTime.Now && u.OrderDate >= today).ToList();
            var secondDayOrders = db.Orders.Where(u => u.OrderDate <= today && u.OrderDate >= secondDay).ToList();
            var thirtDayOrders = db.Orders.Where(u => u.OrderDate <= secondDay && u.OrderDate >= thirtDay).ToList();
            var fourtDayOrders = db.Orders.Where(u => u.OrderDate <= thirtDay && u.OrderDate >= fourthDay).ToList();
            var fifthDayOrders = db.Orders.Where(u => u.OrderDate <= fourthDay && u.OrderDate >= fifthDay).ToList();
            var sixthDayOrders = db.Orders.Where(u => u.OrderDate <= fifthDay && u.OrderDate >= sixthDay).ToList();
            var seventhDayOrders = db.Orders.Where(u => u.OrderDate <= sixthDay && u.OrderDate >= seventhDay).ToList();
            /* Pass orders total value to view */
            ViewBag.Today = todayOrders.Sum(v => v.TotalPrice);
            ViewBag.SecondDay = secondDayOrders.Sum(v => v.TotalPrice);
            ViewBag.ThirtDay = thirtDayOrders.Sum(v => v.TotalPrice);
            ViewBag.FourthDay = fourtDayOrders.Sum(v => v.TotalPrice);
            ViewBag.FifthDay = fifthDayOrders.Sum(v => v.TotalPrice);
            ViewBag.SixthDay = sixthDayOrders.Sum(v => v.TotalPrice);
            ViewBag.SeventhDay = seventhDayOrders.Sum(v => v.TotalPrice);
            /* End this week */

            /* Get last week */
            DateTime lastWeekFirstDay = seventhDay.AddDays(-1);
            DateTime lastWeeksecondDay = lastWeekFirstDay.AddDays(-1);
            DateTime lastWeekthirtDay = lastWeeksecondDay.AddDays(-1);
            DateTime lastWeekfourthDay = lastWeekthirtDay.AddDays(-1);
            DateTime lastWeekfifthDay = lastWeekfourthDay.AddDays(-1);
            DateTime lastWeeksixthDay = lastWeekfifthDay.AddDays(-1);
            DateTime lastWeekseventhDay = lastWeeksixthDay.AddDays(-1);
            /* Get orders total value of each day of last week */
            var lastWeekFirstDayOrders = db.Orders.Where(u => u.OrderDate <= seventhDay && u.OrderDate >= lastWeekFirstDay).ToList();
            var lastWeekSecondDayOrders = db.Orders.Where(u => u.OrderDate <= lastWeekFirstDay && u.OrderDate >= lastWeeksecondDay).ToList();
            var lastWeekThirtDayOrders = db.Orders.Where(u => u.OrderDate <= lastWeeksecondDay && u.OrderDate >= lastWeekthirtDay).ToList();
            var lastWeekFourtDayOrders = db.Orders.Where(u => u.OrderDate <= lastWeekthirtDay && u.OrderDate >= lastWeekfourthDay).ToList();
            var lastWeekFifthDayOrders = db.Orders.Where(u => u.OrderDate <= lastWeekfourthDay && u.OrderDate >= lastWeekfifthDay).ToList();
            var lastWeekSixthDayOrders = db.Orders.Where(u => u.OrderDate <= lastWeekfifthDay && u.OrderDate >= lastWeeksixthDay).ToList();
            var lastWeekSeventhDayOrders = db.Orders.Where(u => u.OrderDate <= lastWeeksixthDay && u.OrderDate >= lastWeekseventhDay).ToList();
            /* Pass orders total value to view */
            ViewBag.LastWeekFirstDay = lastWeekFirstDayOrders.Sum(v => v.TotalPrice);
            ViewBag.LastWeekSecondDay = lastWeekSecondDayOrders.Sum(v => v.TotalPrice);
            ViewBag.LastWeekThirtDay = lastWeekThirtDayOrders.Sum(v => v.TotalPrice);
            ViewBag.LastWeekFourthDay = lastWeekFourtDayOrders.Sum(v => v.TotalPrice);
            ViewBag.LastWeekFifthDay = lastWeekFifthDayOrders.Sum(v => v.TotalPrice);
            ViewBag.LastWeekSixthDay = lastWeekSixthDayOrders.Sum(v => v.TotalPrice);
            ViewBag.LastWeekSeventhDay = lastWeekSeventhDayOrders.Sum(v => v.TotalPrice);
            /* End last week */
            return PartialView();
        }
        public PartialViewResult GetOrderByMonth()
        {
            /* Get 6 near months */
            DateTime thisMonth = DateTime.Now.AddMonths(-1);
            DateTime secondMonth = thisMonth.AddMonths(-1);
            DateTime thirtMonth = secondMonth.AddMonths(-1);
            DateTime fourthMonth = thirtMonth.AddMonths(-1);
            DateTime fifthMonth = fourthMonth.AddMonths(-1);
            DateTime SixthMonth = fifthMonth.AddMonths(-1);

            /* Get 6 months name */
            var ThisMonthName = DateTime.Now.ToString("MMM");
            var secondMonthName = thisMonth.ToString("MMM");
            var thirtMonthName = secondMonth.ToString("MMM");
            var fourthMonthName = thirtMonth.ToString("MMM");
            var fifthMonthName = fourthMonth.ToString("MMM");
            var SixthMonthName = fifthMonth.ToString("MMM");
            /* Pass months name to view */
            ViewBag.ThisMonthName = ThisMonthName;
            ViewBag.SecondMonthName = secondMonthName;
            ViewBag.ThirtMonthName = thirtMonthName;
            ViewBag.FourthMonthName = fourthMonthName;
            ViewBag.FifthMonthName = fifthMonthName;
            ViewBag.SixthMonthName = SixthMonthName;
            /* Get orders total value of each month */
            var thisMonthOrders = db.Orders.Where(u => u.OrderDate <= DateTime.Now && u.OrderDate >= thisMonth).ToList();
            var secondMonthOrders = db.Orders.Where(u => u.OrderDate <= thisMonth && u.OrderDate >= secondMonth).ToList();
            var thirtMonthOrders = db.Orders.Where(u => u.OrderDate <= secondMonth && u.OrderDate >= thirtMonth).ToList();
            var fourthMonthOrders = db.Orders.Where(u => u.OrderDate <= thirtMonth && u.OrderDate >= fourthMonth).ToList();
            var fifthMonthOrders = db.Orders.Where(u => u.OrderDate <= fourthMonth && u.OrderDate >= fifthMonth).ToList();
            var SixthMonthOrders = db.Orders.Where(u => u.OrderDate <= fifthMonth && u.OrderDate >= SixthMonth).ToList();
            /* Pass orders total value to view */
            ViewBag.ThisMonth = thisMonthOrders.Sum(v => v.TotalPrice);
            ViewBag.SecondMonth = secondMonthOrders.Sum(v => v.TotalPrice);
            ViewBag.ThirtMonth = thirtMonthOrders.Sum(v => v.TotalPrice);
            ViewBag.FourthMonth = fourthMonthOrders.Sum(v => v.TotalPrice);
            ViewBag.FifthMonth = fifthMonthOrders.Sum(v => v.TotalPrice);
            ViewBag.SixthMonth = SixthMonthOrders.Sum(v => v.TotalPrice);
            return PartialView();
        }

        public PartialViewResult GetCounts()
        {
            DateTime MonthEndDate = DateTime.Now.AddMonths(-1);
            DateTime LastMonthEndDate = MonthEndDate.AddMonths(-1);

            var users = db.Users.ToList();
            var Customers = new List<ApplicationUser>();
            foreach (var user in users)
            {
                if (_userManager.GetRoles(user.Id)[0].Equals("Customer"))
                {
                    Customers.Add(user);
                }
            }
            var thisMonthUser = db.UserInfos.Where(u => u.RegisterDate <= DateTime.Now && u.RegisterDate >= MonthEndDate).ToList();
            var lastMonthUser = db.UserInfos.Where(u => u.RegisterDate <= MonthEndDate && u.RegisterDate >= LastMonthEndDate).ToList();
            ViewBag.Users = Customers.Count();
            ViewBag.ThisMonthUsers = thisMonthUser.Count();
            ViewBag.LastMonthUsers = lastMonthUser.Count();
            var thisMonthOrders = db.Orders.Where(c => c.OrderDate <= DateTime.Now && c.OrderDate >= MonthEndDate).ToList();
            var lastMonthOrders = db.Orders.Where(c => c.OrderDate <= MonthEndDate && c.OrderDate >= LastMonthEndDate).ToList();
            ViewBag.ThisMonthOrders = thisMonthOrders.Count();
            ViewBag.LastMonthOrders = lastMonthOrders.Count();
            var thisMonthcurrency = db.Orders.Where(c => c.OrderDate <= DateTime.Now && c.OrderDate >= MonthEndDate).ToList();
            var lastMonthcurrency = db.Orders.Where(c => c.OrderDate <= MonthEndDate && c.OrderDate >= LastMonthEndDate).ToList();
            ViewBag.ThisMonthCurrency = thisMonthcurrency.Sum(c => c.TotalPrice);
            ViewBag.LastMonthCurrency = lastMonthcurrency.Sum(c => c.TotalPrice);
            var thisMonthProduct = db.Products.Where(c => c.CreatedDate <= DateTime.Now && c.CreatedDate >= MonthEndDate).ToList();
            var lastMonthProduct = db.Products.Where(c => c.CreatedDate <= MonthEndDate && c.CreatedDate >= LastMonthEndDate).ToList();
            ViewBag.Products = db.Products.Count();
            ViewBag.ThisMonthProduct = thisMonthProduct.Count();
            ViewBag.LastMonthProduct = lastMonthProduct.Count();
            return PartialView();
        }
    }
}