using CommonFiles.DTO;
using Infrastructure;
using reCaptcha;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestApp.Models;
using System.Security.Cryptography;
using System.Text;
using System.Reflection;

namespace TestApp.Controllers
{
    public class UserController : Controller
    {
        //By default, accessing the User Controller without a specified action, you are prompted to create a user
        public ActionResult Index()
        {
            return RedirectToAction("CreateUser");
        }
        [HttpGet]
        //Create User model and check if the last session has tried to create a user and fills out the needed info in the view
        //if the model existed 
        public ActionResult CreateUser()
        {
            ViewBag.Recaptcha = ReCaptcha.GetHtml(ConfigurationManager.AppSettings["ReCaptcha:SiteKey"]);
            ViewBag.publicKey = ConfigurationManager.AppSettings["ReCaptcha:SiteKey"];
            UserModel viewModel;
            viewModel = (UserModel)TempData["userModel"] ?? new UserModel();
            return View(viewModel);
        }

        [HttpPost]
        //After submit is clicked
        public ActionResult RequestUser(UserModel viewModel)
        {
            //Checks if the user is valid and has agreed to the terms and validated the captcha
            if (viewModel.Agreement && ModelState.IsValid && ReCaptcha.Validate(ConfigurationManager.AppSettings["ReCaptcha:SecretKey"]))
            {
                //Creates a hash from the password
                byte[] hash=SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(viewModel.PasswordTest));
                StringBuilder builder = new StringBuilder(256);
                foreach (byte element in hash) builder.Append(element.ToString("x2"));

                Type modelType = viewModel.GetType();
                UserDTO userDTO = new UserDTO{ Password=builder.ToString()};
                foreach (PropertyInfo property in userDTO.GetType()
                    .GetProperties()
                    .Where(property => property.Name!="ID" && property.Name!="Password"))
                {
                    property.SetValue(userDTO, modelType.GetProperty(property.Name).GetValue(viewModel).ToString());
                }

                //Submits the user to the database via the service
                new UserService().InsertUser(userDTO);

                return View();
            }
            //User info was not valid, so the model is stored and you are prompted to create the user
            else
            {
                ViewBag.RecaptchaLastErrors = ReCaptcha.GetLastErrors(HttpContext);

                ViewBag.publicKey = ConfigurationManager.AppSettings["ReCaptcha:SiteKey"];

                TempData["userModel"] = viewModel;
                return RedirectToAction("CreateUser");
            }
        }
    }
}