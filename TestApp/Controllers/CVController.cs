using CommonFiles.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infrastructure;
using TestApp.Models;
using reCaptcha;
using System.Configuration;
using System.Diagnostics;
using System.Web.Helpers;

namespace TestApp.Controllers
{
    public class CVController : Controller
    {
        // GET: CV
        public ActionResult Index()
        {
            return RedirectToAction("Create");
        }

        [HttpGet]
        public ActionResult List()
        {
            IEnumerable<CVModel> dtos = new CVService().GetAll().Select(element => Convert(element));
            return View(dtos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckModel(CVModel model)
        {
            model.PictureBytes = new byte[model.Picture.ContentLength];
            model.Picture.InputStream.Read(model.PictureBytes, 0, model.Picture.ContentLength);

            if (ModelState.IsValid)
            {
                new CVService().Insert(Convert(model));
                return View(model);
            }
            else
            {
                TempData["CVModel"] = model;
                return RedirectToAction("Create");
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Recaptcha = ReCaptcha.GetHtml(ConfigurationManager.AppSettings["ReCaptcha:SiteKey"]);
            ViewBag.publicKey = ConfigurationManager.AppSettings["ReCaptcha:SiteKey"];
            CVModel model = (CVModel)TempData["CVModel"] ?? new CVModel();

            return View(model);
        }

        private CVModel Convert(CVDTO dto) => new CVModel
        {
            Address = dto.Address,
            Education = dto.Education,
            ID = dto.ID,
            Email = dto.Email,
            Experience = dto.Experience,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PictureBytes = dto.PictureBytes,
            PictureName = dto.PictureName
        };
        private CVDTO Convert(CVModel model) => new CVDTO
        {
            Address = model.Address,
            Education = model.Education,
            ID = model.ID,
            Email = model.Email,
            Experience = model.Experience,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Qualities = model.Qualities,
            PictureBytes = model.PictureBytes,
            PictureName = model.Picture.FileName
        };
    }
}