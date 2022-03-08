using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using MVCProject.BLL.Services;
using MVCProject.Common;
using MVCProject.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static MVCProject.WebUI.Areas.Admin.Controllers.AdminAccountController;
using static MVCProject.WebUI.Controllers.BaseController;

namespace MVCProject.WebUI.Areas.Admin.Controllers
{
    public class MenusController : BaseController
    {
        [CustomAuthorizeAttribute(Roles = "Admin")]
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }

        MenuServices service;

        public ActionResult KendoMenu_Read([DataSourceRequest] DataSourceRequest request)
        {
            service = new MenuServices();
            return Json(service.GetAll().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult KendoMenu_Create([DataSourceRequest] DataSourceRequest request, MenusVM menu)
        {
            if (menu != null && ModelState.IsValid)
            {
                service = new MenuServices();
                service.Insert(menu);
            }

            return Json(new[] { menu }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult KendoMenu_Update([DataSourceRequest] DataSourceRequest request, MenusVM menu)
        {
            if (menu != null && ModelState.IsValid)
            {
                service = new MenuServices();
                service.Update(menu);
            }

            return Json(new[] { menu }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult KendoMenu_Destroy([DataSourceRequest] DataSourceRequest request, MenusVM menu)
        {
            if (menu != null)
            {
                service = new MenuServices();
                service.Delete(menu);
            }

            return Json(new[] { menu }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }



    }
}