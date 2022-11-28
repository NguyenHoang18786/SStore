using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SStore.Models;

namespace SStore.Controllers
{
    public class FeedbackClientController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Feedback/Create
        public ActionResult Index()
        {
            return View();
        }

        // POST: Admin/Feedback/Create
        [HttpPost]
        public ActionResult Index(Feedback feedback)
        {
            var response = Request["g-recaptcha-response"];
            const string secret = "6LdpyMMiAAAAAIlgxpjOLj0ijeut9siC0KVfZbWD";
            var client = new WebClient();
            var reply = client.DownloadString(
                        string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));
            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);
            if (ModelState.IsValid && captchaResponse.Success)
            {
                db.Feedbacks.Add(feedback);
                feedback.CreateDate = DateTime.Now;
                db.SaveChanges();
                ViewBag.Messages = String.Format("Successfully sent feedback!");
            }
            else if (!captchaResponse.Success)
            {
                if (captchaResponse.ErrorCodes.Count() <= 0) return View();
                var error = captchaResponse.ErrorCodes[0].ToLower();

                switch (error)
                {
                    case "missing-input-secret":
                        ViewBag.Message = "Missing secret parameter";
                        break;
                    case "invalid-input-secret":
                        ViewBag.Message = "The secret is invalid or malformed";
                        break;
                    case "missing-input-response":
                        ViewBag.Message = "Missing response parameter";
                        break;
                    case "invalid-input-response":
                        ViewBag.Message = "The response parameter is invalid or malformed";
                        break;
                    default:
                        ViewBag.Message = "Error orrcured. Please try again";
                        break;
                }
            }
            return View(feedback);
        }
    }
}