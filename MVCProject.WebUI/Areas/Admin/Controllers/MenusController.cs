
using MVCProject.BLL.Services;
using System.Web.Mvc;
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



    }
}