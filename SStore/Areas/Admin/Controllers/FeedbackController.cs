using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SStore.Models;

namespace SStore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Manager")]
    public class FeedbackController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Feedback
        public ActionResult Index(int? page, string sortOrder, string searchString)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TimeSortParm = sortOrder == "Time" ? "Time_Desc" : "Time";
            ViewBag.TitleSortParm = sortOrder == "Title" ? "Title_Desc" : "Title";
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            var feedbacks = db.Feedbacks.ToList();
            switch (sortOrder)
            {
                case "Title_Desc":
                    feedbacks = db.Feedbacks.OrderByDescending(f => f.Title).ToList();
                    break;
                case "Title":
                    feedbacks = db.Feedbacks.OrderBy(f => f.Title).ToList();
                    break;
                case "Time":
                    feedbacks = db.Feedbacks.OrderBy(f => f.CreateDate).ToList();
                    break;
                case "Time_Desc":
                    feedbacks = db.Feedbacks.OrderByDescending(f => f.CreateDate).ToList();
                    break;
                default:
                    feedbacks = db.Feedbacks.OrderByDescending(f => f.CreateDate).ToList();
                    break;
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                feedbacks = feedbacks.Where(p => p.Title.ToUpper().Contains(searchString.ToUpper())).ToList();
                if (feedbacks.Count() > 0)
                {
                    return View(feedbacks.ToPagedList(pageNumber, pageSize));
                }
                else
                {
                    return RedirectToAction("NotFound");
                }

            }
            return View(feedbacks.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult NotFound()
        {
            return View();
        }

        // GET: Admin/Feedback/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        // GET: Admin/Feedback/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        // POST: Admin/Feedback/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Feedback feedback = db.Feedbacks.Find(id);
            db.Feedbacks.Remove(feedback);
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
