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

namespace SStore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Product
        [HttpGet]
        public ActionResult Index(int? page, string sortOrder, string searchString, string BrandFilter, string CategoryFilter, Nullable<decimal> HighPrice, Nullable<decimal> LowPrice)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name_Desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "Price_Desc" : "Price";
            ViewBag.BrandSortParm = sortOrder == "Brand" ? "Brand_Desc" : "Brand";
            ViewBag.CategorySortParm = sortOrder == "Category" ? "Category_Desc" : "Category";
            int pageSize = 8;
            int pageNumber = (page ?? 1);
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
                case "Brand":
                    products = db.Products.Include(p => p.productBrand).Include(p => p.ProductCategory).OrderBy(p => p.productBrand.BrandName);
                    break;
                case "Brand_Desc":
                    products = db.Products.Include(p => p.productBrand).Include(p => p.ProductCategory).OrderByDescending(p => p.productBrand.BrandName);
                    break;
                case "Category":
                    products = db.Products.Include(p => p.productBrand).Include(p => p.ProductCategory).OrderBy(p => p.ProductCategory.CategoryName);
                    break;
                case "Category_Desc":
                    products = db.Products.Include(p => p.productBrand).Include(p => p.ProductCategory).OrderByDescending(p => p.ProductCategory.CategoryName);
                    break;
                default:
                    products = db.Products.Include(p => p.productBrand).Include(p => p.ProductCategory).OrderBy(p => p.ProductName);
                    break;
            }

            var brands = db.ProductBrands;
            ViewBag.ListBrand = brands;
            var categories = db.ProductCategories;
            ViewBag.ListCategory = categories;

            if (!String.IsNullOrEmpty(BrandFilter))
            {
                products = products.Where(p => p.productBrand.BrandName.Equals(BrandFilter));
                return View(products.ToPagedList(pageNumber, pageSize));
            }
            else if (!String.IsNullOrEmpty(CategoryFilter))
            {
                products = products.Where(p => p.ProductCategory.CategoryName.Equals(CategoryFilter));
                return View(products.ToPagedList(pageNumber, pageSize));
            }
            else if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.ProductName.Contains(searchString));
                if (products.Count() > 0)
                {
                    return View(products.ToPagedList(pageNumber, pageSize));
                }
                else
                {
                    return RedirectToAction("NotFound");
                }
            }
            else if (HighPrice >= 0 && LowPrice >= 0)
            {
                products = products.Where(p => p.Price >= LowPrice && p.Price <= HighPrice);
                return View(products.ToPagedList(pageNumber, pageSize));
            }
            return View(products.ToList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult NotFound()
        {
            return View();
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
        public ActionResult Create(Product product, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                product.CreatedDate = DateTime.Now;
                product.ModifiedDate = DateTime.Now;
                product.View = 0;
                db.Products.Add(product);
                db.SaveChanges();

                if (Image != null && Image.ContentLength > 0)
                {
                    int id = int.Parse(db.Products.ToList().Last().Id.ToString());

                    string fileName = "";
                    int index = Image.FileName.IndexOf('.');
                    fileName = "product" + id.ToString() + "." + Image.FileName.Substring(index + 1);
                    string path = Path.Combine(Server.MapPath("~/Image"), fileName);
                    Image.SaveAs(path);

                    Product ipro = db.Products.FirstOrDefault(x => x.Id == id);
                    ipro.Image = fileName;
                    db.SaveChanges();
                }

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
        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase Image)
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
                productdb.Hot = product.Hot;
                productdb.ModifiedDate = DateTime.Now;
                /*                db.Entry(product).State = EntityState.Modified;
                */
                db.SaveChanges();

                if (Image != null && Image.ContentLength > 0)
                {
                    int id = product.Id;

                    string fileName = "";
                    int index = Image.FileName.IndexOf('.');
                    fileName = "product" + id.ToString() + "." + Image.FileName.Substring(index + 1);
                    string path = Path.Combine(Server.MapPath("~/Image"), fileName);
                    Image.SaveAs(path);

                    Product ipro = db.Products.FirstOrDefault(x => x.Id == id);
                    ipro.Image = fileName;
                    db.SaveChanges();
                }

                return RedirectToAction("Details", new { id = product.Id });
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
            Product product = db.Products.Include(p => p.productBrand).Include(p => p.ProductCategory).SingleOrDefault(p => p.Id == id);
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
            Product product = db.Products.Include(p => p.productBrand).Include(p => p.ProductCategory).SingleOrDefault(p => p.Id == id);
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
