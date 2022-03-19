using MVCProject.BLL.Services;
using MVCProject.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MVCProject.WebUI.Controllers
{
    public class HomeController : BaseController
    {

        OrderServices orderServices = new OrderServices();
        SehirlerServices sehirlerServices = new SehirlerServices();
        IlcelerServices ilcelerServices = new IlcelerServices();
        SemtServices semtServices = new SemtServices();
        ZoneServices zoneServices = new ZoneServices();

        
        public HomeController()
        {


        }
        public ActionResult DoJobs()
        {


            return null;

        }
        public ActionResult Track()
        {
          
            return View();
        }

        public ActionResult Zones()
        {

            List<ZoneVM> zonesList = new List<ZoneVM>();
            zonesList = zoneServices.GetAll().ToList();
            return View(zonesList);
        }
        public ActionResult SiteMap()
        {

            //Response.Clear();
            ////Response.ContentTpye ile bu Action'ın View'ını XML tabanlı olarak ayarlıyoruz.
            //Response.ContentType = "text/xml";
            //XmlTextWriter xr = new XmlTextWriter(Response.OutputStream, Encoding.UTF8);
            //xr.WriteStartDocument();
            //xr.WriteStartElement("urlset");//urlset etiketi açıyoruz
            //xr.WriteAttributeString("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");
            //xr.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            //xr.WriteAttributeString("xsi:schemaLocation", "http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/siteindex.xsd");
            ///* sitemap dosyamızın olmazsa olmazını ekledik. Şeması bu dedik buraya kadar.  */

            //xr.WriteStartElement("url");
            //xr.WriteElementString("loc", "https://www.ZuuCargo.net/");
            //xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
            //xr.WriteElementString("changefreq", "daily");
            //xr.WriteElementString("priority", "1");
            //xr.WriteEndElement();

            ////Burada veritabanımızdaki Firmaları SiteMap'e ekliyoruz.
            ////var s = providerServices.GetAll();
            //foreach (var a in s)
            //{
            //    xr.WriteStartElement("url");
            //    xr.WriteElementString("loc", "https://www.ZuuCargo.net/Provider/ProviderDetailId/" + a.Id);
            //    xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
            //    xr.WriteElementString("priority", "0.5");
            //    xr.WriteElementString("changefreq", "monthly");
            //    xr.WriteEndElement();
            //}



            //xr.WriteEndDocument();
            ////urlset etiketini kapattık
            //xr.Flush();
            //xr.Close();
            //Response.End();
            return View();

        }
        ////[RequireHttps]
        public JsonResult RemoteDataSource_KategoriGetir(string text)
        {

            CategoryServices kategoriServices = new CategoryServices();

            var kategoriler = kategoriServices.GetAll().Select(x => new CategoryVM
            {
                Title = x.Title,
                Id = x.Id

            });

            return Json(kategoriler, JsonRequestBehavior.AllowGet);
        }

        // GET: Home
        //[RequireHttps]
        public ActionResult Index()
        {

            ReklamServices reklamServices = new ReklamServices();
            ViewBag.Reklam = reklamServices.GetAll().ToList();
          
            return View();
        }
        public ActionResult CalculateShipping()
        {
            List<ZoneVM> zoneList = zoneServices.GetAll().ToList();
            ViewData["ZoneList"] = zoneList;

            return View();
        }


        public JsonResult CalculatePriceJson(JsonAnswer[] data)
        {

            bool success;

            string country = data[0].value;

            string weight = data[1].value;
            string lenght = data[2].value;
            string width = data[3].value;
            string height = data[4].value;

            string postType = data[5].value;

            short? zoneId = zoneServices.GetAll().Where(x => x.Country.Trim() == country.Trim()).FirstOrDefault().ZoneNo;

            PriceServices priceServices = new PriceServices();
            List<PriceVM> priceList3 = new List<PriceVM>();
            try
            {
                List<PriceVM> priceList1 = priceServices.GetAll().Where(x => Convert.ToInt32(zoneId) == Convert.ToInt32(x.Zone)).ToList();

                //List<PriceVM> priceList2 = priceList1.Where(x => Convert.ToBoolean(isExpress) == x.IsExpress).ToList();
                List<PriceVM> priceList2 = priceList1.Where(x => postType.Trim() == x.Type.Trim()).ToList();

                priceList3 = priceList2.Where(x => Convert.ToDouble(weight) <= x.ToDesi && Convert.ToDouble(weight) >= x.FromDesi).ToList();



                //List<PriceVM> priceList5 = priceList4.Where(x => countryZone == x.Zone).ToList();

                double weight2 = Convert.ToDouble(weight);
                weight2 = weight2 + 1;
                success = true;

            }
            catch (Exception ex)
            {
                return Json(ex, JsonRequestBehavior.AllowGet);

            }
           

            return Json(priceList3, JsonRequestBehavior.AllowGet);

        }
        public JsonResult TrackStatus(JsonAnswer[] data)
        {
            bool success;

            string trackNo = data[0].value;

            ShipmentVM shipment = new ShipmentVM();
            StatusUpdatesServices statusUpdatesServices = new StatusUpdatesServices();



            ShipmentServices shipmentServices = new ShipmentServices();
            List<StatusUpdatesVM> shipmentStatuses = new List<StatusUpdatesVM>();

            try
            {
                shipment = shipmentServices.GetAll().Where(x => x.TrackingNo.Trim() == trackNo.Trim()).FirstOrDefault();
                shipmentStatuses = statusUpdatesServices.GetAll().Where(x => x.ShipmentId == shipment.Id).ToList();
                success = true;

            }
            catch (Exception ex)
            {
                return Json(ex, JsonRequestBehavior.AllowGet);

            }


            return Json(shipmentStatuses, JsonRequestBehavior.AllowGet);
        }

            
        public JsonResult TrackOrder(JsonAnswer[] data)
        {

            bool success;

            string trackNo = data[0].value;

            ShipmentVM shipment = new ShipmentVM();
            StatusUpdatesServices statusUpdatesServices = new StatusUpdatesServices();

      

            ShipmentServices shipmentServices = new ShipmentServices();
           
            try
            {
                 shipment = shipmentServices.GetAll().Where(x => x.TrackingNo.Trim() == trackNo.Trim()).FirstOrDefault();
                //shipment.StatusUpdates = statusUpdatesServices.GetAll().Where(x => x.ShipmentId == shipment.Id).ToList();
                success = true;

            }
            catch (Exception ex)
            {
                return Json(ex, JsonRequestBehavior.AllowGet);

            }
            
            
            return Json(shipment, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public PartialViewResult TestAjax(string Name)
        {
            ViewBag.Name = Name;
            return PartialView();
        }
        public ActionResult PrivacyPolicy()
        {
            return View();
        }

        public ActionResult HowItWorks()
        {
            return View();
        }

        public ActionResult HowFindCustomers()
        {
            return View();
        }
        public ActionResult Blog()
        {
            return View();
        }
        public ActionResult Terms()
        {
            return View();
        }

        // GET: Home
        public ActionResult Cars()
        {
            return View();

        }

        public ActionResult Suppliers()
        {
            return View();

        }
        public ActionResult Locations()
        {
            return View();

        }
        public ActionResult Comments()
        {
            return View();

        }
        public ActionResult Contact()
        {
            ViewBag.ToId = "8d2fb683-9720-4371-af68-539bc8069d8b";
            ViewBag.MessageVM = new MessageReplyVM();
            return View();

        }
        public ActionResult Services()
        {

            return View();

        }
    }
}