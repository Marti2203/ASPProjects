using reCaptcha;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestApp.Models;
namespace TestApp.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return RedirectToAction("Create");
        }
        [HttpGet]
        public ActionResult CreateUser()
        {
            ViewBag.Recaptcha = ReCaptcha.GetHtml(ConfigurationManager.AppSettings["ReCaptcha:SiteKey"]);
            ViewBag.publicKey = ConfigurationManager.AppSettings["ReCaptcha:SiteKey"];
            UserModel viewModel;
            if (TempData["userModel"] != null)
                viewModel = (UserModel)TempData["userModel"];
            else viewModel = new UserModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult RequestUser(UserModel viewModel)
        {
            if (viewModel.Agreement && ModelState.IsValid && ReCaptcha.Validate(ConfigurationManager.AppSettings["ReCaptcha:SecretKey"]))
            {
                return View();
            }

            else
            {
                ViewBag.RecaptchaLastErrors = ReCaptcha.GetLastErrors(HttpContext);

                ViewBag.publicKey = ConfigurationManager.AppSettings["ReCaptcha:SiteKey"];

                TempData["viewModel"] = viewModel;
                return RedirectToAction("CreateUser");
            }
        }
    }
}