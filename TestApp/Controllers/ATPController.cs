using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Web.Mvc;
using TestApp.Models;

namespace TestApp.Controllers
{
    public class ATPController : Controller
    {
        private IList<BoxModel> _models;

        public ATPController()
        {
            _models = SingletonBoxList.Models ;

        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Box()
        {
            BoxModel viewModel;

            if (TempData["viewModel"] != null)
            {
                viewModel = (BoxModel)TempData["viewModel"];
            }
            else
            {
                viewModel = new BoxModel();
            }

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Boxes()
        {
            return View(_models);
        }

        public ActionResult DeleteBox(string id)
        {
            _models.Remove(_models.Where(box => box.ID == id).FirstOrDefault());
            return RedirectToAction("Boxes");
        }


        public ActionResult EditBox(string id)
        {
            BoxModel model = _models.Where(element => element.ID == id).FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        public ActionResult EditBox(BoxModel viewModel)
        {
            if (ModelState.IsValid)
            {
                BoxModel old = _models.Where(model => model.ID == viewModel.ID).FirstOrDefault();
                foreach (PropertyInfo property in old.GetType().GetProperties())
                {
                    property.SetValue(old, property.GetValue(viewModel));
                }

                return RedirectToAction("Boxes");
            }
            else
                return RedirectToAction("Edit",new { id= viewModel.ID });
        }

        [HttpPost]
        public ActionResult RequestBox(BoxModel viewModel)
        {
            #region Testing Zone
#if Test

            if (viewModel.Width == 1) { viewModel.Width = (int)2e5; }
#endif
            #endregion
            if (ModelState.IsValid)
            {
                string message = string.Format("The request for box with with size {0}:{1}:{2} and weight {3}, color {4} and material {5} was successfully accepted."
                , viewModel.Width, viewModel.Height, viewModel.Length, viewModel.Weight, viewModel.Colour, viewModel.Material);

                ViewBag.SuccessMessage = message;
                _models.Add(viewModel);
                Session["models"] = _models;
                return View();

            }
            else
            {
                TempData["viewModel"] = viewModel;
                return RedirectToAction("Box");
            }
        }
    }
}