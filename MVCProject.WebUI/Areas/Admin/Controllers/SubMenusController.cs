
using MVCProject.BLL;
using MVCProject.BLL.Services;
using MVCProject.Common;
using MVCProject.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using static MVCProject.WebUI.Controllers.BaseController;

namespace MVCProject.WebUI.Areas.Admin.Controllers
{
    public class SubMenusController : BaseController
    {
        [CustomAuthorizeAttribute(Roles = "Admin")]
        // GET: SubMenus
        public ActionResult Index()
        {
            return View();
        }

        SubMenuServices service;

    }
}