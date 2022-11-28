using SStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using Microsoft.Ajax.Utilities;
using System.Net;
using PagedList;
using PagedList.Mvc;

namespace SStore.Controllers
{
    public class AllProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: AllProduct
        public ActionResult Index(int? page, string sortOrder, string searchString, string BrandFilter, string CategoryFilter, Nullable<decimal> HighPrice, Nullable<decimal> LowPrice)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "Price" : "Price";
            ViewBag.NameSortDescParm = sortOrder == "Name_Desc" ? "Name_Desc" : "Name_Desc";
            ViewBag.PriceSortDescParm = sortOrder == "Price_Desc" ? "Price_Desc" : "Price_Desc";
            int pageSize = 18;
            int pageNumber = (page ?? 1);
            var allProduct = db.Products.Include(p => p.productBrand).OrderBy(p => p.ProductName).Include(p => p.ProductCategory);
            var brands = db.ProductBrands;
            ViewBag.ListBrand = brands;
            var categories = db.ProductCategories;
            ViewBag.ListCategory = categories;

            switch (sortOrder)
            {
                case "Name_Desc":
                    allProduct = allProduct.OrderByDescending(p => p.ProductName);
                    break;
                case "Price":
                    allProduct = allProduct.OrderBy(p => p.Price);
                    break;
                case "Price_Desc":
                    allProduct = allProduct.OrderByDescending(p => p.Price);
                    break;
                default:
                    allProduct = allProduct.OrderBy(p => p.ProductName);
                    break;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                allProduct = allProduct.Where(p => p.ProductName.Contains(searchString));
                if (allProduct.Count() > 0)
                {
                    return View(allProduct.ToPagedList(pageNumber, pageSize));
                }
                else
                {
                    return RedirectToAction("NotFound");
                }
            }
            else if (!String.IsNullOrEmpty(BrandFilter))
            {
                allProduct = allProduct.Where(p => p.productBrand.BrandName.Equals(BrandFilter));
                return View(allProduct.ToPagedList(pageNumber, pageSize));
            }
            else if (!String.IsNullOrEmpty(CategoryFilter))
            {
                allProduct = allProduct.Where(p => p.ProductCategory.CategoryName.Equals(CategoryFilter));
                return View(allProduct.ToPagedList(pageNumber, pageSize));
            }
            else if (HighPrice >= 0 && LowPrice >= 0)
            {
                allProduct = allProduct.Where(p => p.Price >= LowPrice && p.Price <= HighPrice);
                return View(allProduct.ToPagedList(pageNumber, pageSize));
            }
            return View(allProduct.ToList().ToPagedList(pageNumber, pageSize));
        }

        // Shoes page
        public ActionResult Shoes(int? page, string sortOrder, string BrandFilter, Nullable<decimal> HighPrice, Nullable<decimal> LowPrice)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "Price" : "Price";
            ViewBag.NameSortDescParm = sortOrder == "Name_Desc" ? "Name_Desc" : "Name_Desc";
            ViewBag.PriceSortDescParm = sortOrder == "Price_Desc" ? "Price_Desc" : "Price_Desc";
            int pageSize = 18;
            int pageNumber = (page ?? 1);
            var Shoes = db.Products.Include(p => p.productBrand).Include(p => p.ProductCategory).Where(p => p.ProductCategory.CategoryName.Equals("Shoes")).OrderBy(p => p.ProductName);
            var brands = db.ProductBrands;
            ViewBag.ListBrand = brands;

            switch (sortOrder)
            {
                case "Name_Desc":
                    Shoes = Shoes.OrderByDescending(p => p.ProductName);
                    break;
                case "Price":
                    Shoes = Shoes.OrderBy(p => p.Price);
                    break;
                case "Price_Desc":
                    Shoes = Shoes.OrderByDescending(p => p.Price);
                    break;
                default:
                    Shoes = Shoes.OrderBy(p => p.ProductName);
                    break;
            }

            if (!String.IsNullOrEmpty(BrandFilter))
            {
                Shoes = Shoes.Where(p => p.productBrand.BrandName.Equals(BrandFilter)).OrderBy(p => p.ProductName);
                return View(Shoes.ToPagedList(pageNumber, pageSize));
            }
            else if (HighPrice >= 0 && LowPrice >= 0)
            {
                Shoes = Shoes.Where(p => p.Price >= LowPrice && p.Price <= HighPrice).OrderBy(p => p.ProductName);
                return View(Shoes.ToPagedList(pageNumber, pageSize));
            }
            return View(Shoes.ToList().ToPagedList(pageNumber, pageSize));
        }

        // Shirt page
        public ActionResult Shirt(int? page, string sortOrder, string BrandFilter, Nullable<decimal> HighPrice, Nullable<decimal> LowPrice)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "Price" : "Price";
            ViewBag.NameSortDescParm = sortOrder == "Name_Desc" ? "Name_Desc" : "Name_Desc";
            ViewBag.PriceSortDescParm = sortOrder == "Price_Desc" ? "Price_Desc" : "Price_Desc";
            int pageSize = 18;
            int pageNumber = (page ?? 1);
            var Shirt = db.Products.Include(p => p.productBrand).Include(p => p.ProductCategory).Where(p => p.ProductCategory.CategoryName.Equals("Shirt")).OrderBy(p => p.ProductName);
            var brands = db.ProductBrands;
            ViewBag.ListBrand = brands;

            switch (sortOrder)
            {
                case "Name_Desc":
                    Shirt = Shirt.OrderByDescending(p => p.ProductName);
                    break;
                case "Price":
                    Shirt = Shirt.OrderBy(p => p.Price);
                    break;
                case "Price_Desc":
                    Shirt = Shirt.OrderByDescending(p => p.Price);
                    break;
                default:
                    Shirt = Shirt.OrderBy(p => p.ProductName);
                    break;
            }

            if (!String.IsNullOrEmpty(BrandFilter))
            {
                Shirt = Shirt.Where(p => p.productBrand.BrandName.Equals(BrandFilter)).OrderBy(p => p.ProductName);
                return View(Shirt.ToPagedList(pageNumber, pageSize));
            }
            else if (HighPrice >= 0 && LowPrice >= 0)
            {
                Shirt = Shirt.Where(p => p.Price >= LowPrice && p.Price <= HighPrice).OrderBy(p => p.ProductName);
                return View(Shirt.ToPagedList(pageNumber, pageSize));
            }
            return View(Shirt.ToList().ToPagedList(pageNumber, pageSize));
        }

        // Hat page
        public ActionResult Hat(int? page, string sortOrder, string BrandFilter, Nullable<decimal> HighPrice, Nullable<decimal> LowPrice)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "Price" : "Price";
            ViewBag.NameSortDescParm = sortOrder == "Name_Desc" ? "Name_Desc" : "Name_Desc";
            ViewBag.PriceSortDescParm = sortOrder == "Price_Desc" ? "Price_Desc" : "Price_Desc";
            int pageSize = 18;
            int pageNumber = (page ?? 1);
            var Hat = db.Products.Include(p => p.productBrand).Include(p => p.ProductCategory).Where(p => p.ProductCategory.CategoryName.Equals("Hat")).OrderBy(p => p.ProductName);
            var brands = db.ProductBrands;
            ViewBag.ListBrand = brands;

            switch (sortOrder)
            {
                case "Name_Desc":
                    Hat = Hat.OrderByDescending(p => p.ProductName);
                    break;
                case "Price":
                    Hat = Hat.OrderBy(p => p.Price);
                    break;
                case "Price_Desc":
                    Hat = Hat.OrderByDescending(p => p.Price);
                    break;
                default:
                    Hat = Hat.OrderBy(p => p.ProductName);
                    break;
            }

            if (!String.IsNullOrEmpty(BrandFilter))
            {
                Hat = Hat.Where(p => p.productBrand.BrandName.Equals(BrandFilter)).OrderBy(p => p.ProductName);
                return View(Hat.ToPagedList(pageNumber, pageSize));
            }
            else if (HighPrice >= 0 && LowPrice >= 0)
            {
                Hat = Hat.Where(p => p.Price >= LowPrice && p.Price <= HighPrice).OrderBy(p => p.ProductName);
                return View(Hat.ToPagedList(pageNumber, pageSize));
            }
            return View(Hat.ToList().ToPagedList(pageNumber, pageSize));
        }

        // Accessories page
        public ActionResult Accessories(int? page, string sortOrder, string BrandFilter, Nullable<decimal> HighPrice, Nullable<decimal> LowPrice)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "Price" : "Price";
            ViewBag.NameSortDescParm = sortOrder == "Name_Desc" ? "Name_Desc" : "Name_Desc";
            ViewBag.PriceSortDescParm = sortOrder == "Price_Desc" ? "Price_Desc" : "Price_Desc";
            int pageSize = 18;
            int pageNumber = (page ?? 1);
            var Accessories = db.Products.Include(p => p.productBrand).Include(p => p.ProductCategory).Where(p => p.ProductCategory.CategoryName.Equals("Accessories")).OrderBy(p => p.ProductName);
            var brands = db.ProductBrands;
            ViewBag.ListBrand = brands;

            switch (sortOrder)
            {
                case "Name_Desc":
                    Accessories = Accessories.OrderByDescending(p => p.ProductName);
                    break;
                case "Price":
                    Accessories = Accessories.OrderBy(p => p.Price);
                    break;
                case "Price_Desc":
                    Accessories = Accessories.OrderByDescending(p => p.Price);
                    break;
                default:
                    Accessories = Accessories.OrderBy(p => p.ProductName);
                    break;
            }

            if (!String.IsNullOrEmpty(BrandFilter))
            {
                Accessories = Accessories.Where(p => p.productBrand.BrandName.Equals(BrandFilter)).OrderBy(p => p.ProductName);
                return View(Accessories.ToPagedList(pageNumber, pageSize));
            }
            else if (HighPrice >= 0 && LowPrice >= 0)
            {
                Accessories = Accessories.Where(p => p.Price >= LowPrice && p.Price <= HighPrice).OrderBy(p => p.ProductName);
                return View(Accessories.ToPagedList(pageNumber, pageSize));
            }
            return View(Accessories.ToList().ToPagedList(pageNumber, pageSize));
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
            product.View++;
            Random rand = new Random();
            int skipper = rand.Next(0, db.Products.Count());
            var randomProductList = db.Products.OrderBy(p => p.Id).Skip(skipper).Take(4).ToList();
            ViewBag.RandomProduct = randomProductList;
            db.SaveChanges();
            if (product == null)
            {
                return RedirectToAction("NotFound");
            }
            return View(product);
        }
    }
}