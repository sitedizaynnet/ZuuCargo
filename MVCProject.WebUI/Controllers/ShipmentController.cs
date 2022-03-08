using MVCProject.BLL.Services;
using MVCProject.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProject.WebUI.Controllers
{
    public class ShipmentController : BaseController
    {
        // GET: Shipment
        public ActionResult Index()
        {
            ShipmentServices shipmentServices = new ShipmentServices();
            IEnumerable<ShipmentVM> shipmentList = shipmentServices.GetAll();
            return View(shipmentList);
        }

        // GET: Shipment/Details/5
        public ActionResult Details(int id)
        {
            ShipmentServices shipmentServices = new ShipmentServices();
            ShipmentVM shipmentVM = new ShipmentVM();
            shipmentVM = shipmentServices.GetById(id);

            return View(shipmentVM);
        }

        // GET: Shipment/Create
        public ActionResult Create()
        {
            ShipmentVM shipmentVM = new ShipmentVM();
            return View(shipmentVM);
        }

        // POST: Shipment/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Shipment/Edit/5
        [CustomAuthorizeAttribute(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            ShipmentServices shipmentServices = new ShipmentServices();
            ShipmentVM shipment = new ShipmentVM();
            shipment = shipmentServices.GetById(id);
            return View(shipment);
        }

        // POST: Shipment/Edit/5
        [CustomAuthorizeAttribute(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Shipment/Delete/5
        [CustomAuthorizeAttribute(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            ShipmentServices shipmentServices = new ShipmentServices();
            ShipmentVM shipment = shipmentServices.GetById(id);
         
            return View(shipment);
        }

        // POST: Shipment/Delete/5
        [CustomAuthorizeAttribute(Roles = "Admin")]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult TrackOrder()
        {
            return View();

        }
    }
}
