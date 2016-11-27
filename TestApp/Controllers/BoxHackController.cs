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
    public class BoxHackController : Controller
    {
        // GET: Box
        public ActionResult Index()
        {
            return View("Box");
        }
        [HttpGet]
        //Create a new Box Model if the previous session had created one and show page for Box Creation
        public ActionResult Create()
        {
            BoxModelHack viewModel = (BoxModelHack)TempData["viewModel"] ?? new BoxModelHack();

            return View(viewModel);
        }

        [HttpGet]
        //List all Boxes
        public ActionResult List()
        {
            List<BoxModelHack> boxes = new List<BoxModelHack>();
            foreach(BoxDTOHack dto in new BoxServiceHack().GetAll())
            {
                boxes.Add(Convert(dto));
            }
            return View(boxes);
        }

        [HttpPost]
        //Output information of created box
        public ActionResult RequestBox(BoxModelHack viewModel)
        {
            //Show a view consisting of the fields of the box
            if (ModelState.IsValid)
            {
                string message = string.Format("The request for box with with size {0}:{1}:{2} and weight {3}, color {4} and material {5} was successfully accepted."
                , viewModel.Width, viewModel.Height, viewModel.Length, viewModel.Weight, viewModel.Colour, viewModel.Material);

                //Create DTO
                BoxDTOHack box = new BoxDTOHack();
                //Set Properties of DTO
                foreach (PropertyInfo property in box.GetType().GetProperties())
                {
                    property.SetValue(box, viewModel.GetType().GetProperty(property.Name).GetValue(viewModel));
                }
                new BoxServiceHack().Insert(box);

                ViewBag.SuccessMessage = message;
                return View();

            }
            //Go back to creating a box, because something was not valid
            else
            {
                TempData["viewModel"] = viewModel;
                return RedirectToAction("Create");
            }
        }

        [HttpGet]
        //Delete a box with the specified id
        public ActionResult Delete(string id)
        {
            new BoxServiceHack().Delete(id);//Find box if there exists one
            return RedirectToAction("List");
        }

        [HttpGet]
        //Edit information of a specified by id box
        public ActionResult Edit(string id)
        {
            BoxModelHack model = Convert(new BoxServiceHack().Get(id));//Find box if there exists one
            return View(model);
        }

        [HttpPost]
        //Edit box with a specified DatabaseBoxModelHack
        public ActionResult Edit(BoxModelHack viewModel)
        {
            if (ModelState.IsValid)
            {
                BoxDTOHack @new = new BoxDTOHack();
                foreach (PropertyInfo property in @new.GetType().GetProperties())
                {
                    property.SetValue(@new, viewModel.GetType().GetProperty(property.Name).GetValue(viewModel));
                }

                new BoxServiceHack().Edit(@new);

                return RedirectToAction("List");
            }
            else
                return RedirectToAction("Edit", new { id = viewModel.ID });
        }

        //DTO to Model Converter
        private BoxModelHack Convert(BoxDTOHack dto)
        {
            BoxModelHack viewModel = new BoxModelHack();
            foreach (PropertyInfo property in viewModel.GetType().GetProperties())
            {
                property.SetValue(viewModel, dto.GetType().GetProperty(property.Name).GetValue(dto));
            }
            return viewModel;
        }
    }
}