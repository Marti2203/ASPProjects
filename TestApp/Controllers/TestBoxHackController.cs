using DataAccess;
using InfrastructureInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestApp.Controllers
{
    public class TestBoxHackController : Controller
    {
        // GET: TestBoxHack
        public ActionResult Index()
        {
            IService<User> service = ServiceFactory.CreateService<User>();
            
            return View(service.EntitiesQuery);
        }
    }
}