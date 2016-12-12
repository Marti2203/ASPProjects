using CommonFiles.DTO;
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
            IService<UserDTO> service = Services.UserService;
            
            return View(service.Entities());
        }
    }
}