using MVCProject.BLL.Services;
using MVCProject.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProject.WebUI.Areas.Admin.Controllers
{
    public class ZonesController : BaseController
       

    {
        ZoneServices zoneservices = new ZoneServices();
        // GET: Admin/Zones
        public ActionResult Index()
        {
            IEnumerable<ZoneVM> zoneList = new List<ZoneVM>();
            zoneList = zoneservices.GetAll();
            return View(zoneList);
        }

        // GET: Admin/Zones/Details/5
        public ActionResult Details(int id)
        {
            ZoneVM zonevm = new ZoneVM();
            zonevm = zoneservices.GetById(id);
            return View(zonevm);
        }

        // GET: Admin/Zones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Zones/Create
        [HttpPost]
        public ActionResult Create(ZoneVM zoneVM)
        {
            try
            {
                // TODO: Add insert logic here
                zoneservices.Insert(zoneVM);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Zones/Edit/5
        public ActionResult Edit(int id)
        {
            ZoneVM zoneVM = new ZoneVM();
            zoneVM = zoneservices.GetById(id);
            return View(zoneVM);
        }

        // POST: Admin/Zones/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ZoneVM zoneVM)
        {
            try
            {
                // TODO: Add update logic here
              
                zoneservices.Update(zoneVM);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



        // POST: Admin/Zones/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                ZoneVM zoneVM = new ZoneVM();
                zoneVM = zoneservices.GetById(id);
                zoneservices.Delete(zoneVM);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
