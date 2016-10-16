using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestApp.Models;

namespace TestApp.Controllers
{
    public class ATPController : Controller
    {
        // GET: ATP
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Box()
        {
            BoxModel viewModel = new BoxModel();
            return View(viewModel);
        }


        [HttpPost]
        public ActionResult RequestBox(BoxModel viewModel)
        {
            if (viewModel.Width == 1) { viewModel.Width = (int)2e5; }


            string message = string.Format("The request for box with with size {0}:{1}:{2} and weight {3}, color {4} and material {5} was successfully accepted."
                ,viewModel.Width,viewModel.Weight,viewModel.Length,viewModel.Weight,viewModel.Color,viewModel.Material);
            //return View(viewModel);
            //return RedirectToAction("Box",viewModel);
            ViewBag.SuccessMessage = message;
            return View();
        }

    }
}