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
        private IList<BoxModel> _models;//The "database" for the boxes

        public ATPController()
        {
            _models = SingletonBoxList.Models ; //Create the instance of the Singleton

        }

        //Open index page
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        //Create a new Box Model if the previous session had created one and show page for Box Creation
        public ActionResult Box()
        {
            BoxModel viewModel = (BoxModel)TempData["viewModel"] ?? new BoxModel();

            return View(viewModel);
        }

        [HttpGet]
        //List all Boxes
        public ActionResult Boxes()
        {
            return View(_models);
        }
        //Delete a box with the specified id
        public ActionResult DeleteBox(string id)
        {
            _models.Remove(_models.Where(box => box.ID == id).FirstOrDefault());//Find box if there exists one
            return RedirectToAction("Boxes");
        }

        //Edit information of a specified by id box
        public ActionResult EditBox(string id)
        {
            BoxModel model = _models.Where(element => element.ID == id).FirstOrDefault();//Find box if there exists one
            return View(model);
        }

        [HttpPost]
        //Edit box with a specified BoxModel
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
        //Output information of created box
        public ActionResult RequestBox(BoxModel viewModel)
        {

            //Show a view consisting of the fields of the box
            if (ModelState.IsValid)
            {
                string message = string.Format("The request for box with with size {0}:{1}:{2} and weight {3}, color {4} and material {5} was successfully accepted."
                , viewModel.Width, viewModel.Height, viewModel.Length, viewModel.Weight, viewModel.Colour, viewModel.Material);

                ViewBag.SuccessMessage = message;
                _models.Add(viewModel);
                Session["models"] = _models;
                return View();

            }
            //Go back to creating a box
            else
            {
                TempData["viewModel"] = viewModel;
                return RedirectToAction("Box");
            }
        }
    }
}