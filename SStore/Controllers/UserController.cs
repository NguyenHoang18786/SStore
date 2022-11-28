using Microsoft.AspNet.Identity;
using SStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SStore.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: User/Details
        public ActionResult Details()
        {
            var userId = User.Identity.GetUserId();
            UserInfo userInfo = db.UserInfos.Where(x => x.UserId == userId).FirstOrDefault();
            return View(userInfo);
        }

        // GET: User/Edit
        public ActionResult Edit()
        {
            var userId = User.Identity.GetUserId();
            var userInfo = db.UserInfos.SingleOrDefault(u => u.UserId.Equals(userId));
            if (userInfo == null) return HttpNotFound();

            return View(userInfo);
        }

        // POST: User/Edit
        [HttpPost]
        public ActionResult Edit(UserInfo userInfo)
        {
            var userId = User.Identity.GetUserId();
            var userInfoDb = db.UserInfos.SingleOrDefault(u => u.UserId.Equals(userId));
            if (userInfoDb == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (ModelState.IsValid)
            {
                userInfoDb.FirstName = userInfo.FirstName;
                userInfoDb.LastName = userInfo.LastName;
                userInfoDb.Gender = userInfo.Gender;
                userInfoDb.Country = userInfo.Country;
                userInfoDb.City = userInfo.City;
                userInfoDb.Address = userInfo.Address;
                userInfoDb.Phone = userInfo.Phone;
                db.SaveChanges();
                return RedirectToAction("Details");
            }
            return View(userInfo);
        }
    }
}
