﻿using MVCProject.BLL.Services;
using MVCProject.Common.ViewModels;
using MVCProject.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using static MVCProject.WebUI.Controllers.BaseController;
using MVCProject.WebUI.PTTService;
using RestSharp;
using Newtonsoft.Json;
using MVCProject.WebUI.PTTTrack;

namespace MVCProject.WebUI.Areas.Admin.Controllers
{
    public class ShipmentController : BaseController
    {
        ShipmentServices shipmentServices = new ShipmentServices();
        StatusUpdatesServices statusUpdatesServices = new StatusUpdatesServices();

        public ActionResult GetCargoLabel(int id)
        {


          
             ShipmentVM shipmentVM = shipmentServices.GetById(id);

            return View(shipmentVM);

        }


        public int GetCheckDigit(int newBarcodeDigit)
        {
            int digit1 = Convert.ToInt32(newBarcodeDigit.ToString().Substring(0, 1));
            var digit2 = Convert.ToInt32(newBarcodeDigit.ToString().Substring(1, 1));
            var digit3 = Convert.ToInt32(newBarcodeDigit.ToString().Substring(2, 1));
            var digit4 = Convert.ToInt32(newBarcodeDigit.ToString().Substring(3, 1));
            var digit5 = Convert.ToInt32(newBarcodeDigit.ToString().Substring(4, 1));

            double checkDigit1 = 72 + 54 + 36 + (digit1 * 2) + (digit2 * 3) + (digit3 * 5) + (digit4 * 9) + (digit5 * 7);

            checkDigit1 = checkDigit1 / 11;
            int checkDigit2 = Convert.ToInt32(Math.Floor(checkDigit1));
            checkDigit2 = checkDigit2 - 11;

            if (checkDigit2 == 10)
            {
                checkDigit2 = 0;
            }
            if (checkDigit2 == 11)
            {
                checkDigit2 = 5;
            }

            return checkDigit2;

        } 
        public string GetShipmentBarcode(int id)
        {
            ShipmentBarcodesServices shipmentBarcodesServices = new ShipmentBarcodesServices();
            ShipmentBarcodesVM shipmentBarcodesVM = new ShipmentBarcodesVM();
            //RE999700000TR- RE999799999TR

            string lastBarcode = shipmentBarcodesServices.GetAll().Last().Barcode;
            int barcodeDigit = Convert.ToInt32(lastBarcode.Substring(5, 5));
            int newBarcodeDigit = barcodeDigit + 1;
            int checkDigit = GetCheckDigit(newBarcodeDigit);
            shipmentBarcodesVM.ShipmentId = id;
            ShipmentVM shipmentVM = shipmentServices.GetById(id);
            if (shipmentVM.PostType.Trim() == "KOLİ")
            {

                shipmentBarcodesVM.Barcode = "CE999" + newBarcodeDigit + checkDigit + "TR";
            }
            else
            {
                shipmentBarcodesVM.Barcode = "RE999" + newBarcodeDigit + checkDigit + "TR";
            }
            shipmentBarcodesServices.Insert(shipmentBarcodesVM);
            return shipmentBarcodesVM.Barcode;
        }
        public string GetTurpexShipmentBarcode(int id)
        {
            ShipmentTurpexBarcodesServices shipmentTurpexBarcodesServices = new ShipmentTurpexBarcodesServices();
            ShipmentTurpexBarcodesVM shipmentTurpexBarcodesVM = new ShipmentTurpexBarcodesVM();
            //TE999800000TR-TE999899999TR

            string lastBarcode = shipmentTurpexBarcodesServices.GetAll().Last().Barcode;
            int barcodeDigit = Convert.ToInt32(lastBarcode.Substring(5, 5));
            int newBarcodeDigit = barcodeDigit + 1;
            int checkDigit = GetCheckDigit(newBarcodeDigit);
            shipmentTurpexBarcodesVM.ShipmentId = id;
            ShipmentVM shipmentVM = shipmentServices.GetById(id);
           
                shipmentTurpexBarcodesVM.Barcode = "TE999" + newBarcodeDigit + checkDigit + "TR";
           
            shipmentTurpexBarcodesServices.Insert(shipmentTurpexBarcodesVM);
            return shipmentTurpexBarcodesVM.Barcode;
        }

        public PTTService.Output SendPttData(int id, string barcodeNumber)
        {
            ShipmentVM shipmentVM = shipmentServices.GetById(id);

            var input = new PTTService.Input();
            
            input.userName = 804440354;
            input.folderName = shipmentVM.Id.ToString();
            input.shipmentKind = "YURTDISI";
            input.shipmentType = shipmentVM.PostType;
            input.password = "S62Exy6V4NNi1iQqNZLVGA";
            input.userNameSpecified = true;

            var barcodeInformation = new PTTService.BarcodeInformation();
            barcodeInformation.senderName = shipmentVM.SenderName; //
            barcodeInformation.senderNumber = shipmentVM.SenderTelephone;//
            barcodeInformation.senderPhone = shipmentVM.SenderTelephone;//
            barcodeInformation.senderSms = shipmentVM.SenderTelephone;//
            barcodeInformation.senderAdres = shipmentVM.SenderAddress;//
            barcodeInformation.senderPostCode = shipmentVM.SenderPostalCode.ToString();//
            barcodeInformation.senderMail = shipmentVM.SenderEmail;//
            barcodeInformation.senderTaxNo = shipmentVM.SenderIdVATNumber.ToString();
            barcodeInformation.senderTCNo = shipmentVM.SenderTCNo;//

            barcodeInformation.receiverName = shipmentVM.ReceiverName;//
            barcodeInformation.receiverAdres = shipmentVM.ReceiverAddress;//
            barcodeInformation.receiverMail = shipmentVM.ReceiverEmail;//
            barcodeInformation.receiverCountryId = shipmentVM.ReceiverCountryId;//
            barcodeInformation.receiverPostCode = shipmentVM.ReceiverPostalCode;//

            barcodeInformation.barcode = barcodeNumber;
            barcodeInformation.price = 0;
            barcodeInformation.paymentmethod = "N"; //N olacak
            barcodeInformation.senderUpperNumber = "804440354";
            barcodeInformation.priceSpecified = true;
            barcodeInformation.paymentConditionalPrice = 0;
            barcodeInformation.paymentConditionalPriceSpecified = true;
  
            barcodeInformation.height = shipmentVM.Height;
            barcodeInformation.heightSpecified = true;
            barcodeInformation.width = shipmentVM.Width;
            barcodeInformation.widthSpecified = true;
            barcodeInformation.length = shipmentVM.Lenght;
            barcodeInformation.lengthSpecified = true;
            barcodeInformation.weight = shipmentVM.Weight;
            barcodeInformation.weightSpecified = true;
            barcodeInformation.desi = shipmentVM.Desi;
            barcodeInformation.desiSpecified = true;
            barcodeInformation.postalAccountNumberSpecified = false;
           
            barcodeInformation.valuePriceSpecified = true;
            barcodeInformation.financialAdvisorNo = "0";

            var contexes = new InvoceContent[1];
            contexes[0].productName = shipmentVM.Content;
            contexes[0].unitPrice = shipmentVM.ValueOfPackage;
            contexes[0].unitPriceSpecified = true;
            contexes[0].weight = shipmentVM.Weight;
            contexes[0].count = 1;
            contexes[0].currency = "USD";
            contexes[0].origin = "TR";
            contexes[0].productHarmonyCode = "620442";

            barcodeInformation.context = contexes;

            var barcodes = new BarcodeInformation[1];
            barcodes[0] = barcodeInformation;
            input.barcodeInformation = barcodes;

            LoadPortTypeClient client = new LoadPortTypeClient();
            var response = client.loadData(input);



            return response;
        }


        [CustomAuthorizeAttribute(Roles = "Admin, Shipment")]
        public ActionResult LoadData()
        {

            //Creating instance of DatabaseContext class  
           
            IEnumerable<ShipmentVM> shipmentList = shipmentServices.GetAll();

            var draw = Request.Form.GetValues("draw").FirstOrDefault();
                    var start = Request.Form.GetValues("start").FirstOrDefault();
                    var length = Request.Form.GetValues("length").FirstOrDefault();
                    var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                    var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                    var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();


                    //Paging Size (10,20,50,100)    
                    int pageSize = length != null ? Convert.ToInt32(length) : 0;
                    int skip = start != null ? Convert.ToInt32(start) : 0;
                    int recordsTotal = 0;

                    // Getting all Customer data    
                    var customerData = shipmentList;

                    ////Sorting    
                    //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                    //{
                    //    customerData = customerData.OrderBy<ShipmentVM>(sortColumn + " " + sortColumnDir);
                    //}
                    //Search    
                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        customerData = customerData.Where(m => m.SenderName == searchValue).ToList();
                    }

                    //total number of rows count     
                    recordsTotal = customerData.Count();
                    //Paging     
                    var data = customerData.Skip(skip).Take(pageSize).ToList();
                    //Returning Json Data    
                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
                
            


        }
        // GET: Shipment
        [CustomAuthorizeAttribute(Roles = "Admin, Shipment")]
        public ActionResult Index()
        {
            ShipmentServices shipmentServices = new ShipmentServices();
            List<ShipmentMultiVM> shipmentMultiVMs = new List<ShipmentMultiVM>();
            List<ShipmentVM> shipmentLists = new List<ShipmentVM>();
            shipmentLists = shipmentServices.GetAll().ToList();



            IEnumerable<ShipmentVM> shipmentList = shipmentServices.GetAll();

            return View(shipmentList);
        }
        [CustomAuthorizeAttribute(Roles = "Admin, Shipment")]
        // GET: Shipment/Details/5
        public ActionResult Details(int id)
        {
            ShipmentServices shipmentServices = new ShipmentServices();
            StatusUpdatesServices statusUpdatesServices = new StatusUpdatesServices();

            ShipmentMultiVM shipmentMultiVM = new ShipmentMultiVM();
            shipmentMultiVM.ShipmentVM = shipmentServices.GetById(id);
            shipmentMultiVM.StatusUpdatesVMList = statusUpdatesServices.GetAll().Where(x => x.ShipmentId == id).ToList();

            return View(shipmentMultiVM);
        }

        [CustomAuthorizeAttribute(Roles = "Admin, Shipment")]
        // GET: Shipment/Create
        public ActionResult Create()
        {
            Random rnd = new Random();
            Random r = new Random();
            int rInt = r.Next(5000000, 100000000); //for ints

            ShipmentVM shipmentVM = new ShipmentVM();
            shipmentVM.TrackingNo = rInt.ToString();
            shipmentVM.ShipmentDate = DateTime.Now;
            return View(shipmentVM);
        }

        // POST: Shipment/Create
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Shipment")]
        public ActionResult Create(ShipmentVM shipmentVM)
        {
            try
            {
                // TODO: Add insert logic here
                ShipmentServices shipmentServices = new ShipmentServices();
                shipmentVM.ShipmentDate = DateTime.Now;
                shipmentServices.Insert(shipmentVM);

                AccountingVM accountingVM = new AccountingVM();
                AccountingServices accountingServices = new AccountingServices();
                accountingVM.TotalDollar = shipmentVM.TotalDolar;
                accountingVM.City = shipmentVM.SenderCountry;
                accountingVM.CustomerName = shipmentVM.SenderName;
                accountingVM.Date = DateTime.Now;
                accountingVM.IsCargo = true;
                accountingVM.Quantity = shipmentVM.PackageCount;
                accountingVM.Telephone = shipmentVM.SenderTelephone;
                accountingVM.Notes = shipmentVM.Notes;
                accountingServices.Insert(accountingVM);


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [CustomAuthorizeAttribute(Roles = "Admin, Shipment")]
        // GET: Shipment/Create
        public ActionResult CreatePTT()
        {
           
            ShipmentVM shipmentVM = new ShipmentVM();
            
            shipmentVM.ShipmentDate = DateTime.Now;
            return View(shipmentVM);
        }

        [CustomAuthorizeAttribute(Roles = "Admin, Shipment")]
        public JsonResult UlkedenSehirGetir(string ulkeId)
        {
            var client = new RestClient("https://pttwssgt.ptt.gov.tr/PostaKargoService/PttTurpexOperationsTest/turpex/ulkesehirler/" + ulkeId);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");

            var body = @"{" + "\n" + @"    ""musteri_id"": 804440354," + "\n" + @"    ""password"": ""S62Exy6V4NNi1iQqNZLVGA""" + "\n" +
            @"}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            ViewBag.Sehirler = response.Content;
            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorizeAttribute(Roles = "Admin, Shipment")]
        public JsonResult HarmonyCodeGetir()
        {
            var client = new RestClient("https://pttwssgt.ptt.gov.tr/PostaKargoService/PttTurpexOperationsTest/turpex/harmonycodes");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");

            var body = @"{" + "\n" + @"    ""musteri_id"": 804440354," + "\n" + @"    ""password"": ""S62Exy6V4NNi1iQqNZLVGA""" + "\n" +
            @"}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
           
            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }

        public bool SendTurpexData(int id, string barcodeNumber)
        {
            ShipmentVM shipmentVM = shipmentServices.GetById(id);
            var client = new RestClient("https://pttwssgt.ptt.gov.tr/PostaKargoService/PttTurpexOperations/savedata");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");

            var body = @"{" + "\n"
                + @"    ""agirlik"": " + shipmentVM.Weight + "," + "\n"
                + @"    ""alici_adi"": """ + shipmentVM.ReceiverName + "\"" + ","
                + "\n"
                 + @"    ""alici_adresi"": """ + shipmentVM.ReceiverAddress + "\"" + ","
                + "\n"
                 + @"    ""alici_email"": """ + shipmentVM.ReceiverEmail + "\"" + ","
                + "\n"
                 + @"    ""alici_iletisim_kisi"": """ + shipmentVM.ReceiverName + "\"" + ","
                + "\n"
                 + @"    ""alici_posta_kodu"": """ + shipmentVM.ReceiverPostalCode + "\"" + ","
                + "\n"
                 + @"    ""alici_sehir_id"": """ + shipmentVM.ReceiverCityId + "\"" + ","
                + "\n"
                 + @"    ""alici_telefonu"": """ + shipmentVM.ReceiverTelephoneNo + "\"" + ","
                + "\n"
                 + @"    ""alici_ulke_id"": """ + shipmentVM.ReceiverCountryId + "\"" + ","
                + "\n"
                 + @"    ""barkod_no"": """ + barcodeNumber + "\"" + ","
                + "\n"
                 + @"    ""boy"": " + shipmentVM.Height  + ","
                + "\n"
                 + @"    ""deger_konulmus_ucret"": " + shipmentVM.ValueOfPackage + ","
                + "\n"
                 + @"    ""desi"": " + shipmentVM.Desi + ","
                + "\n"
                 + @"    ""en"": " + shipmentVM.Width  + ","
                + "\n"
                 + @"    ""fatura_no"":  """ + shipmentVM.Id + "\"" + ","
                + "\n"
                 + @"    ""gonderici_adi"": """ + shipmentVM.SenderName + "\"" + ","
                + "\n"
                 + @"    ""gonderici_adresi"":  """ + shipmentVM.SenderAddress + "\"" + ","
                + "\n"
                 + @"    ""gonderici_email"":  """ + shipmentVM.SenderEmail + "\"" + ","
                + "\n"
                 + @"    ""gonderici_il_id"": ""73""" + ","
                + "\n"
                 + @"    ""gonderici_posta_kodu"": """ + shipmentVM.SenderPostalCode + "\"" + ","
                + "\n"
                 + @"    ""gonderici_tc_no"": """ + shipmentVM.SenderTCNo + "\"" + ","
                + "\n"
                 + @"    ""gonderici_telefonu"": """ + shipmentVM.SenderTelephone + "\"" + ","
                + "\n"
                 + @"    ""gonderici_ulke_id"": ""TR""" + ","
                + "\n"
                 + @"    ""gonderici_vergi_no"":  """ + shipmentVM.SenderIdVATNumber + "\"" + ","
                + "\n"
                 + @"    ""kapsam"": ""COM""" + ","
                + "\n"
                 + @"    ""l_muhteviyat"": ["
                + "\n"
                 + @"{"
                 + "\n"
            + @"    ""adet"": 1" + ","
                + "\n"
                 + @"    ""birim_fiyat"": " + shipmentVM.ValueOfPackage +  ","
                + "\n"
                 + @"    ""doviz_cinsi"": ""EUR""" + ","
                + "\n"
                 + @"    ""esya_kodu"": ""620442""" + ","
                + "\n"
                 + @"    ""harmony_code_id"": " + shipmentVM.HarmonyCode + ","
                + "\n"
                 + @"    ""icerik"": """ + shipmentVM.Content + "\"" + ","
                + "\n"
                 + @"    ""islem_saat"": 0" + ","
                + "\n"
                 + @"    ""islem_tarih"": 0" + ","
                + "\n"
                 + @"    ""mensei"": ""TR""" 
                + "\n"
                 + @"}"
                 + @"]" + ","
                 + @"    ""musteri_referans_no"":  """ + shipmentVM.Id + "\"" + ","
                + "\n"
                 + @"    ""navlun_bedeli"": 0" + ","
                + "\n"
                + @"    ""odeme_sartli_ucret"": 0" + ","
                + "\n"
                 + @"    ""odeme_sekli"": ""N""" + ","
                + "\n" 
               
                 + @"    ""posta_ceki_no"": 0" + ","
                + "\n"
                + @"    ""teslim_edilememe_durum"": ""I""" + ","
                + "\n"
                 + @"    ""ucret"": 0" + ","
                + "\n" +
                  @"""yetki"": {" + "\n" + @"    ""musteri_id"": 804440354," + "\n" + @"    ""password"": ""S62Exy6V4NNi1iQqNZLVGA""" + "\n" +
            @"}" + "," +
                 "\n"
                  + @"    ""yukseklik"": " + shipmentVM.Height 
                + "\n"
            + @"}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);


            shipmentVM.ShipmentDate = DateTime.Now;

            return true;
        }

            // GET: Shipment/Create
            [CustomAuthorizeAttribute(Roles = "Admin, Shipment")]
        public ActionResult CreateTurpex()
        {
            var client = new RestClient("https://pttwssgt.ptt.gov.tr/PostaKargoService/PttTurpexOperationsTest/turpex/ulkeler");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
          
            var body = @"{" + "\n" + @"    ""musteri_id"": 804440354," + "\n" + @"    ""password"": ""S62Exy6V4NNi1iQqNZLVGA""" + "\n" +
            @"}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            ViewBag.Ulkeler = response.Content; 


            ShipmentVM shipmentVM = new ShipmentVM();

            shipmentVM.ShipmentDate = DateTime.Now;
            return View(shipmentVM);
        }


        [CustomAuthorizeAttribute(Roles = "Admin, Shipment")]
        [HttpPost]
        public ActionResult CreateTurpex(ShipmentVM shipmentVM)
        {
            try
            {
                // TODO: Add insert logic here
                ShipmentServices shipmentServices = new ShipmentServices();
                shipmentVM.ShipmentDate = DateTime.Now;
                shipmentVM.Desi = (shipmentVM.Height * shipmentVM.Width * shipmentVM.Lenght) / 3000;

                shipmentServices.Insert(shipmentVM);
                var lastId = shipmentServices.GetAll().Last().Id;
                var barcode = GetTurpexShipmentBarcode(lastId);
                shipmentVM.Id = lastId;
                shipmentVM.TrackingNo = barcode;
                shipmentServices.Update(shipmentVM);

                bool response = SendTurpexData(lastId, barcode);

               

                AccountingVM accountingVM = new AccountingVM();
                AccountingServices accountingServices = new AccountingServices();
                accountingVM.TotalDollar = shipmentVM.TotalDolar;
                accountingVM.City = shipmentVM.SenderCountry;
                accountingVM.CustomerName = shipmentVM.SenderName;
                accountingVM.Date = DateTime.Now;
                accountingVM.IsCargo = true;
                accountingVM.Quantity = shipmentVM.PackageCount;
                accountingVM.Telephone = shipmentVM.SenderTelephone;
                accountingVM.Notes = shipmentVM.Notes;
                accountingServices.Insert(accountingVM);
                return View();
            }
            catch (Exception ex)
            {
                RedirectToAction("CreateTurpex", "Shipment",shipmentVM);
            }
            return RedirectToAction("Index", "Shipment");
            }

        // POST: Shipment/Create
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Shipment")]
        public ActionResult CreatePTT(ShipmentVM shipmentVM)
        {
            try
            {
                // TODO: Add insert logic here
                ShipmentServices shipmentServices = new ShipmentServices();
                shipmentVM.ShipmentDate = DateTime.Now;
                shipmentVM.Desi = (shipmentVM.Height * shipmentVM.Width * shipmentVM.Lenght) / 3000;

                shipmentServices.Insert(shipmentVM);
                var lastId = shipmentServices.GetAll().Last().Id;
                var barcode = GetShipmentBarcode(lastId);
                shipmentVM.Id = lastId;
                shipmentVM.TrackingNo = barcode;
                shipmentServices.Update(shipmentVM);
               PTTService.Output response = SendPttData(lastId, barcode);

                if (response.barcodeInformation[0].success == false)
                {
                    ViewBag.FaultDesc = response.barcodeInformation[0].faultDescription;
                    RedirectToAction("CreatePTT", "Shipment");
                }

                AccountingVM accountingVM = new AccountingVM();
                AccountingServices accountingServices = new AccountingServices();
                accountingVM.TotalDollar = shipmentVM.TotalDolar;
                accountingVM.City = shipmentVM.SenderCountry;
                accountingVM.CustomerName = shipmentVM.SenderName;
                accountingVM.Date = DateTime.Now;
                accountingVM.IsCargo = true;
                accountingVM.Quantity = shipmentVM.PackageCount;
                accountingVM.Telephone = shipmentVM.SenderTelephone;
                accountingVM.Notes = shipmentVM.Notes;
                accountingServices.Insert(accountingVM);


                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                return View();
            }
        }

      

        public ActionResult EditStatus(int id)
        {
            ShipmentServices shipmentServices = new ShipmentServices();
            StatusUpdatesServices statusUpdatesServices = new StatusUpdatesServices();
            List<StatusUpdatesVM> StatusUpdatesVM = new List<StatusUpdatesVM>();
            StatusUpdatesVM = statusUpdatesServices.GetAll().Where(x => x.ShipmentId == id).ToList();
            return View(StatusUpdatesVM);
        }
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Shipment")]
        public ActionResult EditStatus(StatusUpdatesVM status)
        {
            ShipmentServices shipmentServices = new ShipmentServices();
            StatusUpdatesServices statusUpdatesServices = new StatusUpdatesServices();
            statusUpdatesServices.Update(status);
            return View();
        }
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Shipment")]
        public ActionResult AddStatus(StatusUpdatesVM StatusUpdatesVM)
        {

            ViewData["Statuses"] = null;
            ViewData["Statuses"] = statusUpdatesServices.GetAll().Where(x => x.ShipmentId == StatusUpdatesVM.Id).ToList();
            ShipmentVM shipmentVM = shipmentServices.GetAll().Where(x => x.Id == StatusUpdatesVM.ShipmentId).FirstOrDefault();
            StatusUpdatesVM.Id = 0;
            statusUpdatesServices.Insert(StatusUpdatesVM);

            if (StatusUpdatesVM.IsSendSMS)
            {
                //SEND SMS
                string html = string.Empty;
                string mesajMetni = "Your shipment " + shipmentVM.TrackingNo + " status is updated. Track it using http://ZuuCargo.com/Track/Shipment/" + shipmentVM.TrackingNo;
                string telNo = shipmentVM.SenderTelephone;

                string url = @"https://gw.cmtelecom.com/gateway.ashx?producttoken=6A10F3EB-0AE0-4114-BD13-902EB0C57A58&body=" + mesajMetni + "&to=" + telNo + "&from=ZuuCargo&reference=your_reference";
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                }
            }



            return View();

        }

        [CustomAuthorizeAttribute(Roles = "Admin, Shipment")]
        public ActionResult InvoiceSMS(int  id)
        {
            ShipmentVM shipmentVM = new ShipmentVM();
            shipmentVM = shipmentServices.GetById(id);
            
            //SEND SMS
            string html = string.Empty;
        string mesajMetni = "Thank you for Shipping with ZuuCargo , your Waybill Number is " + shipmentVM.TrackingNo + ". Track it using http://ZuuCargo.com/Track/Shipment/" + shipmentVM.TrackingNo;

        string telNo = shipmentVM.SenderTelephone;

        string url = @"https://gw.cmtelecom.com/gateway.ashx?producttoken=6A10F3EB-0AE0-4114-BD13-902EB0C57A58&body=" + mesajMetni + "&to=" + telNo + "&from=ZuuCargo&reference=your_reference";
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse) request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                }


               return View();

}
        [CustomAuthorizeAttribute(Roles = "Admin, Shipment")]
        public ActionResult TrackSMS(int id)
        {
            ShipmentVM shipmentVM = new ShipmentVM();
            shipmentVM = shipmentServices.GetById(id);

            //SEND SMS
            string html = string.Empty;
            string mesajMetni = "Thank you for choosing with ZuuCargo , your tracking link is https://zuucargo.com/Track/TrackShipment/ " + shipmentVM.TrackingNo;

            string telNo = shipmentVM.ReceiverTelephoneNo;

            string url = @"https://gw.cmtelecom.com/gateway.ashx?producttoken=6A10F3EB-0AE0-4114-BD13-902EB0C57A58&body=" + mesajMetni + "&to=" + telNo + "&from=ZuuCargo&reference=your_reference";
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }


            return View();

        }


        [CustomAuthorizeAttribute(Roles = "Admin, Shipment")]
        public ActionResult TrackSMSAcente(int id)
        {
            ShipmentVM shipmentVM = new ShipmentVM();
            shipmentVM = shipmentServices.GetById(id);

            //SEND SMS
            string html = string.Empty;
            string mesajMetni = "Thank you for choosing with ZuuCargo , your tracking link is https://zuucargo.com/Track/TrackShipment/ " + shipmentVM.TrackingNo;

            string telNo = shipmentVM.Acente;

            string url = @"https://gw.cmtelecom.com/gateway.ashx?producttoken=6A10F3EB-0AE0-4114-BD13-902EB0C57A58&body=" + mesajMetni + "&to=" + telNo + "&from=ZuuCargo&reference=your_reference";
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }


            return View();

        }

        //[CustomAuthorizeAttribute(Roles = "Admin, Shipment")]
        //public ActionResult TNTSMS(int id)
        //{
        //    ShipmentVM shipmentVM = new ShipmentVM();
        //    shipmentVM = shipmentServices.GetById(id);

        //    //SEND SMS
        //    string html = string.Empty;
        //    string mesajMetni = "Thank you for Shipping with ZuuCargo , your TNT tracking link is  " + shipmentVM.TNTLink;

        //    string telNo = shipmentVM.SenderTelephone;

        //    string url = @"https://gw.cmtelecom.com/gateway.ashx?producttoken=6A10F3EB-0AE0-4114-BD13-902EB0C57A58&body=" + mesajMetni + "&to=" + telNo + "&from=ZuuCargo&reference=your_reference";
        //    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        //    request.AutomaticDecompression = DecompressionMethods.GZip;

        //    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        //    using (Stream stream = response.GetResponseStream())
        //    using (StreamReader reader = new StreamReader(stream))
        //    {
        //        html = reader.ReadToEnd();
        //    }


        //    return View();

        //}

        string mesajMetni;
        [CustomAuthorizeAttribute(Roles = "Admin, Shipment")]
        public ActionResult AddStatus(int id)
        {
          
            StatusUpdatesVM statusUpdatesVM = new StatusUpdatesVM();
            ShipmentVM shipmentVM = shipmentServices.GetById(id);
            
            statusUpdatesVM.UpdatedDateTime = DateTime.Now;
            statusUpdatesVM.ShipmentId = id;
            statusUpdatesVM.StatusId = shipmentVM.StatusId;
            ViewData["Statuses"] = statusUpdatesServices.GetAll().Where(x => x.ShipmentId == id).ToList();
           
            return View(statusUpdatesVM);
  
          
        }

        [CustomAuthorizeAttribute(Roles = "Admin, Shipment")]
        // GET: Shipment/Edit/5
        public ActionResult Edit(int id)
        {
            
            ShipmentVM shipmentVM = new ShipmentVM();
            shipmentVM = shipmentServices.GetById(id);
            try
            {
                shipmentVM.Status = statusUpdatesServices.GetAll().Where(x => x.ShipmentId == id).Last().Status;
            }
            catch (Exception)
            {


            }
            return View(shipmentVM);
        }

        // POST: Shipment/Edit/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Shipment")]
        public ActionResult Edit(int id, ShipmentVM shipmentVM)
        {
           
            try
            {
                shipmentServices.Update(shipmentVM);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [CustomAuthorizeAttribute(Roles = "Admin, Shipment")]
        // GET: Shipment/Delete/5
        public ActionResult Delete(int id)
        {
            ShipmentServices shipmentServices = new ShipmentServices();
            ShipmentVM shipment = shipmentServices.GetById(id);
            shipmentServices.Delete(shipment);


            return RedirectToAction("Index","Shipment");
        }

       
        [CustomAuthorizeAttribute(Roles = "Admin, Shipment")]
        public ActionResult TrackOrder()
        {
            return View();

        }
        [CustomAuthorizeAttribute(Roles = "Admin, Shipment")]
        public ActionResult ScanShipmentBarcode()
        {
            
            return View();
        }
        [CustomAuthorizeAttribute(Roles = "Admin, Shipment")]
        public ActionResult ScanShipmentBarcodeAjax(int id)
        {
            ShipmentVM shipmentVM = shipmentServices.GetById(id);
            string phoneNumber = shipmentVM.ReceiverTelephoneNo;
            //SEND SMS
            string html = string.Empty;
            string trackingNo = "";
                if (shipmentVM.IsDhl)
            {
                trackingNo = shipmentVM.ExternalTrackingCode;
            }
            if (shipmentVM.IsPtt)
            {
                trackingNo = shipmentVM.ExternalTrackingCode;
            }

            string mesajMetni = "Thank you for choosing ZuuCargo. You can track your shipment #(" + trackingNo + ") with this link http://zuucargo.com/Track/TrackShipment/" + trackingNo;


            string telNo = phoneNumber;

            string url = @"https://gw.cmtelecom.com/gateway.ashx?producttoken=6A10F3EB-0AE0-4114-BD13-902EB0C57A58&body=" + mesajMetni + "&to=" + telNo.Trim() + "&from=ZuuCargo&reference=" + id + 1;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            return View();
        }

        [CustomAuthorizeAttribute(Roles = "Admin, Shipment")]
        public ActionResult PrintShipment(int id)
        {
            ShipmentVM shipmentVM = new ShipmentVM();
            shipmentVM = shipmentServices.GetById(id);
            return View(shipmentVM);
        }

    }
}