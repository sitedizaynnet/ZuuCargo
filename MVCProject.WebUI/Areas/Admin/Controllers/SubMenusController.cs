using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
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

        public ActionResult KendoSubMenu_Read([DataSourceRequest] DataSourceRequest request)
        {
            service = new SubMenuServices();
            return Json(service.GetAll().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult KendoSubMenu_Create([DataSourceRequest] DataSourceRequest request, SubMenusVM submenu)
        {
            if (submenu != null && ModelState.IsValid)
            {
                service = new SubMenuServices();
                service.Insert(submenu);
            }

            return Json(new[] { submenu }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult KendoSubMenu_Update([DataSourceRequest] DataSourceRequest request, SubMenusVM submenu)
        {
            if (submenu != null && ModelState.IsValid)
            {
                service = new SubMenuServices();
                service.Update(submenu);
            }

            return Json(new[] { submenu }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult KendoSubMenu_Destroy([DataSourceRequest] DataSourceRequest request, SubMenusVM submenu)
        {
            if (submenu != null)
            {
                service = new SubMenuServices();
                service.Delete(submenu);

            }

            return Json(new[] { submenu }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

    }
}