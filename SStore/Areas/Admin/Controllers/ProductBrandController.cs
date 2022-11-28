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
    public class ProductBrandController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/ProductBrand
        public ActionResult Index(int? page, string sortOrder, string searchString)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name" : "Name_Desc";
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            var brands = db.ProductBrands.ToList();
            switch (sortOrder)
            {
                case "Name":
                    brands = db.ProductBrands.OrderBy(c => c.BrandName).ToList();
                    break;
                case "Name_Desc":
                    brands = db.ProductBrands.OrderByDescending(c => c.BrandName).ToList();
                    break;
                default:
                    brands = db.ProductBrands.OrderBy(c => c.BrandId).ToList();
                    break;
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                brands = brands.Where(p => p.BrandName.ToUpper().Contains(searchString.ToUpper())).ToList();
                if (brands.Count() > 0)
                {
                    return View(brands.ToPagedList(pageNumber, pageSize));
                }
                else
                {
                    return RedirectToAction("NotFound");
                }

            }
            return View(brands.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult NotFound()
        {
            return View();
        }

        // GET: Admin/ProductBrand/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ProductBrand/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BrandId,BrandName")] ProductBrand productBrand)
        {
            if (ModelState.IsValid)
            {
                db.ProductBrands.Add(productBrand);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productBrand);
        }

        // GET: Admin/ProductBrand/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductBrand productBrand = db.ProductBrands.Find(id);
            if (productBrand == null)
            {
                return HttpNotFound();
            }
            return View(productBrand);
        }

        // POST: Admin/ProductBrand/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BrandId,BrandName")] ProductBrand productBrand)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productBrand).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productBrand);
        }

        // GET: Admin/ProductBrand/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductBrand productBrand = db.ProductBrands.Find(id);
            if (productBrand == null)
            {
                return HttpNotFound();
            }
            return View(productBrand);
        }

        // POST: Admin/ProductBrand/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductBrand productBrand = db.ProductBrands.Find(id);
            db.ProductBrands.Remove(productBrand);
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
