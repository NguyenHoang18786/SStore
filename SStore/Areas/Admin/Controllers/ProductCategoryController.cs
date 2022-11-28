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
    public class ProductCategoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/ProductCategory
        public ActionResult Index(int? page, string sortOrder, string searchString)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name" : "Name_Desc";
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            var categories = db.ProductCategories.ToList();
            switch (sortOrder)
            {
                case "Name":
                    categories = db.ProductCategories.OrderBy(c => c.CategoryName).ToList();
                    break;
                case "Name_Desc":
                    categories = db.ProductCategories.OrderByDescending(c => c.CategoryName).ToList();
                    break;
                default:
                    categories = db.ProductCategories.OrderBy(c => c.CategoryId).ToList();
                    break;
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                categories = categories.Where(p => p.CategoryName.ToUpper().Contains(searchString.ToUpper())).ToList();
                if (categories.Count() > 0)
                {
                    return View(categories.ToPagedList(pageNumber, pageSize));
                }
                else
                {
                    return RedirectToAction("NotFound");
                }

            }
            return View(categories.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult NotFound()
        {
            return View();
        }

        // GET: Admin/ProductCategory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ProductCategory/Create
        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                db.ProductCategories.Add(productCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productCategory);
        }

        // GET: Admin/ProductCategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productCategory = db.ProductCategories.Find(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            return View(productCategory);
        }

        // POST: Admin/ProductCategory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,CategoryName,Description")] ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productCategory);
        }

        // GET: Admin/ProductCategory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productCategory = db.ProductCategories.Find(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            return View(productCategory);
        }

        // POST: Admin/ProductCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductCategory productCategory = db.ProductCategories.Find(id);
            db.ProductCategories.Remove(productCategory);
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
