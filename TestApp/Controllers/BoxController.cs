using CommonFiles.DTO;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using TestApp.Models;

namespace TestApp.Controllers
{
    public class BoxController : Controller
    {
        // GET: Box
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
            List<BoxModel> boxes = new List<BoxModel>();
            foreach(BoxDTO dto in new BoxService().GetBoxes())
            {
                boxes.Add(Convert(dto));
            }
            return View(boxes);
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

                BoxDTO box = new BoxDTO();
                foreach(PropertyInfo property in box.GetType().GetProperties())
                {
                    property.SetValue(box, viewModel.GetType().GetProperty(property.Name).GetValue(viewModel));
                }
                new BoxService().InsertBox(box);

                ViewBag.SuccessMessage = message;
                return View();

            }
            //Go back to creating a box
            else
            {
                TempData["viewModel"] = viewModel;
                return RedirectToAction("Box");
            }
        }
        
        //Delete a box with the specified id
        public ActionResult DeleteBox(string id)
        {
            new BoxService().RemoveBox(id);//Find box if there exists one
            return RedirectToAction("Boxes");
        }

        //Edit information of a specified by id box
        public ActionResult EditBox(string id)
        {
            BoxModel model = Convert(new BoxService().GetBox(id));//Find box if there exists one
            return View(model);
        }

        [HttpPost]
        //Edit box with a specified BoxModel
        public ActionResult EditBox(BoxModel viewModel)
        {
            if (ModelState.IsValid)
            {
                BoxModel old = Convert(new BoxService().GetBox(viewModel.ID));
                foreach (PropertyInfo property in old.GetType().GetProperties())
                {
                    property.SetValue(old, property.GetValue(viewModel));
                }

                return RedirectToAction("Boxes");
            }
            else
                return RedirectToAction("Edit", new { id = viewModel.ID });
        }

        //DTO to Model Converter
        private BoxModel Convert(BoxDTO dto)
        {
            if (dto == null)
                return null;
            BoxModel viewModel = new BoxModel();
            foreach (PropertyInfo property in dto.GetType().GetProperties())
            {
                property.SetValue(viewModel, viewModel.GetType().GetProperty(property.Name).GetValue(viewModel));
            }
            return viewModel;
        }
    }
}