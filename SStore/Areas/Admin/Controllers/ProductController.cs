﻿using System;
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
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Product
        [HttpGet]
        public ActionResult Index(int? page, string sortOrder, string searchString)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name_Desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "Price_Desc" : "Price";
            /*            var products = db.Products.Include(p => p.productBrand).Include(p => p.ProductCategory).OrderBy(p => p.ProductName).ToList();
            */
            var products = db.Products.Include(p => p.productBrand).Include(p => p.ProductCategory);

            switch (sortOrder)
            {
                case "Name_Desc":
                    products = db.Products.Include(p => p.productBrand).Include(p => p.ProductCategory).OrderByDescending(p => p.ProductName);
                    break;
                case "Price":
                    products = db.Products.Include(p => p.productBrand).Include(p => p.ProductCategory).OrderBy(p => p.Price);
                    break;
                case "Price_Desc":
                    products = db.Products.Include(p => p.productBrand).Include(p => p.ProductCategory).OrderByDescending(p => p.Price);
                    break;
                default:
                    products = db.Products.Include(p => p.productBrand).Include(p => p.ProductCategory).OrderBy(p => p.ProductName);
                    break;
            }
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(products.Where(p => p.ProductName.Contains(searchString) || searchString == null).ToList().ToPagedList(pageNumber, pageSize));

        }

        // GET: Admin/Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Include(p => p.productBrand).Include(p => p.ProductCategory).SingleOrDefault(p => p.Id == id);


            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }


        // GET: Admin/Product/Create
        public ActionResult Create()
        {
            ViewBag.BrandId = new SelectList(db.ProductBrands, "BrandId", "BrandName");
            ViewBag.CategoryId = new SelectList(db.ProductCategories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Admin/Product/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.CreatedDate = DateTime.Now;
                product.ModifiedDate = DateTime.Now;
                product.View = 0;
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BrandId = new SelectList(db.ProductBrands, "BrandId", "BrandName", product.BrandId);
            ViewBag.CategoryId = new SelectList(db.ProductCategories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Admin/Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandId = new SelectList(db.ProductBrands, "BrandId", "BrandName", product.BrandId);
            ViewBag.CategoryId = new SelectList(db.ProductCategories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                var productdb = db.Products.SingleOrDefault(p => p.Id.Equals(product.Id));

                productdb.ProductName = product.ProductName;
                productdb.CategoryId = product.CategoryId;
                productdb.Price = product.Price;
                productdb.Weight = product.Weight;
                productdb.Size = product.Size;
                productdb.Color = product.Color;
                productdb.BrandId = product.BrandId;
                productdb.Status = product.Status;
                productdb.Description = product.Description;
                productdb.Image = product.Image;
                productdb.Hot = product.Hot;
                productdb.ModifiedDate = DateTime.Now;
                /*                db.Entry(product).State = EntityState.Modified;
                */
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BrandId = new SelectList(db.ProductBrands, "BrandId", "BrandName", product.BrandId);
            ViewBag.CategoryId = new SelectList(db.ProductCategories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Admin/Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
