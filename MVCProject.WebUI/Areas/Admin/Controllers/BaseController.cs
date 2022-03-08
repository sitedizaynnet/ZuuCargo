using MVCProject.BLL.Services;
using MVCProject.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace MVCProject.WebUI.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            MenuServices service = new MenuServices();

                ViewBag.MenuData = service.GetAll();

           
        }
    }
}