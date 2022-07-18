using Microsoft.AspNet.Identity;
using MVCProject.BLL.Services;
using MVCProject.Common.ViewModels;
using MVCProject.Entities;
using MVCProject.WebUI.PTTTrack;
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

        public TrackingInfo TrackPTT(string id)
        {
            TrackingRequest trackingRequest = new TrackingRequest();
            trackingRequest.inp = new Input();
            trackingRequest.inp.barcode = id;
            trackingRequest.inp.userName = "804440354";
            trackingRequest.inp.password = "S62Exy6V4NNi1iQqNZLVGA";
            PttTrackPortTypeClient pttTrackPortTypeClient = new PttTrackPortTypeClient();
            PTTTrack.Output output = pttTrackPortTypeClient.Tracking(trackingRequest.inp);

            return output.trackingInf.Last();
        }

        public TrackingInfo[] TrackPTTGetList(string id)
        {
            TrackingRequest trackingRequest = new TrackingRequest();
            trackingRequest.inp = new Input();
            trackingRequest.inp.barcode = id;
            trackingRequest.inp.userName = "804440354";
            trackingRequest.inp.password = "S62Exy6V4NNi1iQqNZLVGA";
            PttTrackPortTypeClient pttTrackPortTypeClient = new PttTrackPortTypeClient();
            PTTTrack.Output output = pttTrackPortTypeClient.Tracking(trackingRequest.inp);

            return output.trackingInf;
        }

        public ActionResult DoJobs()
        {
            ShipmentServices shipmentServices = new ShipmentServices();

            List<ShipmentVM> shipments = shipmentServices.GetAll().Where(x=>x.IsDelivered == false ).ToList();
            UserServices userServices = new UserServices();
            foreach (ShipmentVM item in shipments)
            {
                try
                {
                    TrackingInfo[] info = TrackPTTGetList(item.TrackingNo);
                    ShipmentVM shipmentVM = shipmentServices.GetById(item.Id);

                    if (info != null)
                    {

                        try
                        {
                            if (info.Count() > item.StatusCounter )
                            {
                                try
                                {
                                    var senderUserToken = userServices.GetAll().Where(x => x.UserName == shipmentVM.SenderEmail).FirstOrDefault().Token;

                                    if (info.Last().gonderi_durum_aciklama.ToString().Contains("Accepted") || info.Last().gonderi_durum_aciklama.ToString().Contains("Kabul"))
                                    {
                                        shipmentServices.ExecutePushNotification("Status Updated", "Your Package is Accepted.", senderUserToken, null);

                                    }
                                    if (info.Last().gonderi_durum_aciklama.ToString().Contains("forwarded") || info.Last().gonderi_durum_aciklama.ToString().Contains("yönlendirildi"))
                                    {
                                        shipmentServices.ExecutePushNotification("Status Updated", "Your Package is In Transit.", senderUserToken, null);

                                    }
                                    if (info.Last().gonderi_durum_aciklama.ToString().Contains("delivery office") || info.Last().gonderi_durum_aciklama.ToString().Contains("teslimat"))
                                    {
                                        shipmentServices.ExecutePushNotification("Status Updated", "Your Package is In Delivery.", senderUserToken, null);

                                    }
                                    if (info.Last().gonderi_durum_aciklama.ToString().Contains("Deliver ") || info.Last().gonderi_durum_aciklama.ToString().Contains("Teslim edildi "))
                                    {
                                        shipmentServices.ExecutePushNotification("Status Updated", "Your Package is Delivered.", senderUserToken, null);

                                    }
                                    if (info.Last().gonderi_durum_aciklama.ToString().Contains("DHL REQUEST "))
                                    {
                                      var DHLNo =   string.Concat(info.Last().gonderi_durum_aciklama.ToString().Where(char.IsNumber));

                                        item.Konsimento = DHLNo;
                                        shipmentServices.Update(item);

                                    }
                                }
                                catch (Exception)
                                {


                                }
                                try
                                {
                                    var receiverUserToken = userServices.GetAll().Where(x => x.UserName == shipmentVM.ReceiverEmail).FirstOrDefault().Token;
                                    if (info.Last().gonderi_durum_aciklama.ToString().Contains("Accepted") || info.Last().gonderi_durum_aciklama.ToString().Contains("Kabul"))
                                    {
                                        shipmentServices.ExecutePushNotification("Status Updated", "Your Package is Accepted.", receiverUserToken, null);

                                    }
                                    if (info.Last().gonderi_durum_aciklama.ToString().Contains("forwarded") || info.Last().gonderi_durum_aciklama.ToString().Contains("yönlendirildi"))
                                    {
                                        shipmentServices.ExecutePushNotification("Status Updated", "Your Package is In Transit.", receiverUserToken, null);

                                    }
                                    if (info.Last().gonderi_durum_aciklama.ToString().Contains("delivery office") || info.Last().gonderi_durum_aciklama.ToString().Contains("teslimat"))
                                    {
                                        shipmentServices.ExecutePushNotification("Status Updated", "Your Package is In Delivery.", receiverUserToken, null);

                                    }
                                    if (info.Last().gonderi_durum_aciklama.ToString().Contains("Deliver ") || info.Last().gonderi_durum_aciklama.ToString().Contains("Teslim edildi "))

                                    {
                                        shipmentServices.ExecutePushNotification("Status Updated", "Your Package is Delivered.", receiverUserToken, null);

                                    }
                                }
                                catch (Exception)
                                {


                                }


                                item.StatusCounter++;
                                shipmentServices.Update(item);
                            }
                        }
                        catch (Exception)
                        {

                           
                        }

                        if (info.Last().gonderi_durum_aciklama.Contains("Deliver item"))
                        {
                            item.IsDelivered = true;
                            shipmentServices.Update(item);
                        }
                    }
                }
                catch (Exception)
                {
                  

                }
               
               
            }
            return null;

        }
        public JsonResult SaveToken(string currentToken)
        {
           
            var userId = User.Identity.GetUserId();
            var Db = new ZuuCargoEntities();
            var usertoUpdate = Db.Users.First(u => u.Id == userId);
            usertoUpdate.Token = currentToken;
            Db.Entry(usertoUpdate).State = System.Data.Entity.EntityState.Modified;
            Db.SaveChangesAsync();
            return Json(true);
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
        //[RequireHttps]
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
        // GET: Agent
    
        public ActionResult CalculateShipping()
        {
            List<ZoneVM> zoneList = zoneServices.GetAll().ToList();
            ViewData["ZoneList"] = zoneList;

            return View();
        }


        public JsonResult CalculatePriceJson(JsonAnswer[] data)
        {

            bool success;
            var weight = 0.0;
          
            var postType = "";

            string country = data[0].value;

            if (data[1].ToString() != "")
            {
                 weight = Convert.ToDouble(data[1].value);
               
            }
           
            if (data[2].ToString() != "")
            {
                if (data[2].name == "rdClothes")
                {
                    postType = "Clothes";
                }
                else
                {
                    postType = "Dox";
                }
                
            }
            

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