using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCProject.BLL.Services;
using MVCProject.Common.ViewModels;
using MVCProject.Entities;

namespace MVCProject.WebUI.Areas.Admin.Controllers
{
    public class DeliveryController : BaseController
    {
        AccountingServices accountingServices = new AccountingServices();
        CustomerLocationServices customerLocationServices = new CustomerLocationServices();

        // GET: Admin/DeliveryVM
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetLastCoordByAllDevice()
        {

            GpsDataServices gpsdataservice = new GpsDataServices();
            List<string> devicesList = gpsdataservice.GetAll()
                .Select(a => a.DeviceIMEI).Distinct().ToList(); ;
            List<GpsDataVM> dataList = new List<GpsDataVM>();
            foreach (var item in devicesList)
            {
                var itemByDevImei = gpsdataservice.GetAll().Where(x => x.DeviceIMEI == item.ToString()).Last();
                dataList.Add(itemByDevImei);
            }

            var resultList = dataList.Select(b => new
            {
                Id = b.Id,
                lat = b.Latitude,
                lng = b.Longitude,
                direction = b.Direction,
                Date = b.Date,
                deviceId = b.DeviceId,
                deviceImei = b.DeviceIMEI,
                state = b.State,
                speed = b.Speed
            });

            return Json(resultList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDeliveryList()
        {
            List<AccountingVM> DeliveryList = new List<AccountingVM>();
            DeliveryList = accountingServices.GetAll().Where(x => x.IsDelivered == false).ToList();

            return View(DeliveryList);
        }

        public ActionResult DeliveryRoute()
        {
            return View();

        }
        public ActionResult SaveLocation(int id)
        {
            string customerName = accountingServices.GetById(id).CustomerName;
            ViewBag.CustomerName = customerName;
            return View();

        }
        public ActionResult DriveLocation(int id)
        {
            string customerName = accountingServices.GetById(id).CustomerName;
            ViewBag.CustomerName = customerName.Trim();
            CustomerLocationVM customerLocationVM = new CustomerLocationVM();
            customerLocationVM = customerLocationServices.GetAll().Where(x => x.CustomerName == customerName).Last();

            return View(customerLocationVM);

        }
        public JsonResult SaveLocationAjax(string[] function_param)
        {
            var success = false;
            try
            {
                CustomerLocationVM customerLocationVM = new CustomerLocationVM();
                customerLocationVM.CustomerName = function_param[0];
                customerLocationVM.Latitude = function_param[1];
                customerLocationVM.Longitude = function_param[2];
                customerLocationServices.Insert(customerLocationVM);
                success = true;
            }
            catch (Exception)
            {

                throw;
            }
           

            return Json(success, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetDeliveryListAjax()
        {
            
            var deliveryList = accountingServices.GetAll().Where(x=>x.IsDelivered == false).OrderBy(a => a.CustomerName).ToList();
            return Json(new { data = deliveryList }, JsonRequestBehavior.AllowGet);

        }

    }
}
