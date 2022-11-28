using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SStore.Models;

namespace SStore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Manager")]
    public class SlideController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Slide
        public ActionResult Index(int? page)
        {
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            var slides = db.Slides.ToList();
            return View(slides.ToList().ToPagedList(pageNumber, pageSize));
        }


        // GET: Admin/Slide/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Slide/Create
        [HttpPost]
        public ActionResult Create(Slide slide, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                db.Slides.Add(slide);
                db.SaveChanges();

                if (Image != null && Image.ContentLength > 0)
                {
                    int id = int.Parse(db.Slides.ToList().Last().Id.ToString());

                    string fileName = "";
                    int index = Image.FileName.IndexOf('.');
                    fileName = "slide" + id.ToString() + "." + Image.FileName.Substring(index + 1);
                    string path = Path.Combine(Server.MapPath("~/Image"), fileName);
                    Image.SaveAs(path);

                    Slide islide = db.Slides.FirstOrDefault(x => x.Id == id);
                    islide.Image = fileName;
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            return View(slide);
        }

        // GET: Admin/Slide/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = db.Slides.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        // POST: Admin/Slide/Edit/5
        [HttpPost]
        public ActionResult Edit(Slide slide, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                db.Entry(slide).State = EntityState.Modified;
                db.SaveChanges();
                if (Image != null && Image.ContentLength > 0)
                {
                    int id = slide.Id;

                    string fileName = "";
                    int index = Image.FileName.IndexOf('.');
                    fileName = "slide" + id.ToString() + "." + Image.FileName.Substring(index + 1);
                    string path = Path.Combine(Server.MapPath("~/Image"), fileName);
                    Image.SaveAs(path);

                    Slide islide = db.Slides.FirstOrDefault(x => x.Id == id);
                    islide.Image = fileName;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(slide);
        }

        // GET: Admin/Slide/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = db.Slides.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        // POST: Admin/Slide/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slide slide = db.Slides.Find(id);
            db.Slides.Remove(slide);
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
