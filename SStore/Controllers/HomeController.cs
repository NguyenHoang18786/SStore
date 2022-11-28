using System;
using System.Data;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SStore.Models;

namespace SStore.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult NewProductListPartial()
        {
            var newProductList = db.Products.Include(p => p.productBrand).Include(p => p.ProductCategory).OrderByDescending(p => p.CreatedDate).Take(8).ToList();
            return PartialView(newProductList);
        }

        public PartialViewResult HotProductListPartial()
        {
            var hotProductList = db.Products.Include(p => p.productBrand).Include(p => p.ProductCategory).OrderByDescending(p => p.View).Take(8).ToList();
            return PartialView(hotProductList);
        }
        public PartialViewResult FashionProductListPartial()
        {
            var fashionProductList = db.Products.Include(p => p.productBrand).Include(p => p.ProductCategory).Where(p => p.Hot == true).OrderByDescending(p => p.ModifiedDate).Take(4).ToList();
            return PartialView(fashionProductList);
        }
        public PartialViewResult Slider()
        {
            var sliders = db.Slides.OrderByDescending(s => s.Id).ToList();
            return PartialView(sliders);
        }
        public ActionResult Policy()
        {
            return View();
        }
    }
}