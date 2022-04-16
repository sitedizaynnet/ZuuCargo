using Microsoft.AspNet.Identity;
using MVCProject.BLL.Services;
using MVCProject.Common.ViewModels;
using MVCProject.WebUI.PTTService;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static MVCProject.WebUI.Controllers.BaseController;

namespace MVCProject.WebUI.Controllers
{
    public class AgentController : Controller
    {
        ShipmentServices shipmentServices = new ShipmentServices();

        // GET: Agent
        [CustomAuthorizeAttribute(Roles = "Admin, Agent")]
        public ActionResult Index()
        {

            string userId = User.Identity.GetUserId();
            List<ShipmentVM> shipmentVMs = shipmentServices.GetAll().Where(x => x.UserId == userId).ToList();

            return View(shipmentVMs);
        }

        // GET: Agent
        [CustomAuthorizeAttribute(Roles = "Admin, Agent")]
  
        public ActionResult Details(int id)
        {
            ShipmentVM shipmentVM = shipmentServices.GetById(id);
            return View(shipmentVM);
        }


        public ActionResult GetCargoLabel(int id)
        {
            UserServices userServices = new UserServices();
            string userId = User.Identity.GetUserId();
            ViewBag.AgentName = userServices.GetByUserId(userId).FirstName + " " + userServices.GetByUserId(userId).LastName;
            ViewBag.AgentTelephone = userServices.GetByUserId(userId).Phone;


            ShipmentVM shipmentVM = shipmentServices.GetById(id);

            return View(shipmentVM);

        }

        public ActionResult Delete(int id)
        {
            ShipmentVM shipmentVM = shipmentServices.GetById(id);
            shipmentServices.Delete(shipmentVM);
            return RedirectToAction("Index");
        }
        [CustomAuthorizeAttribute(Roles = "Admin, Agent")]
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

            return View();
        }

        [CustomAuthorizeAttribute(Roles = "Admin, Agent")]
        public ActionResult CreatePTT()
        {
            return View();
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
            var client = new RestClient("https://pttwssgt.ptt.gov.tr/PostaKargoService/PttTurpexOperations/turpex/harmonycodes");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");

            var body = @"{" + "\n" + @"    ""musteri_id"": 804440354," + "\n" + @"    ""password"": ""S62Exy6V4NNi1iQqNZLVGA""" + "\n" +
            @"}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            return Json(response.Content, JsonRequestBehavior.AllowGet);
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
                shipmentVM.IsDelivered = false;
                shipmentVM.IsPtt = true;
                shipmentVM.IsConfirmed = true;

                shipmentServices.Insert(shipmentVM);
                var lastId = shipmentServices.GetAll().Last().Id;
                var barcode = GetTurpexShipmentBarcode(lastId);
                shipmentVM.Id = lastId;
                shipmentVM.TrackingNo = barcode;
                shipmentServices.Update(shipmentVM);

                bool response = SendTurpexData(lastId, barcode);

                if (response == true)
                {
                    shipmentVM.IsApiSuccess = 1;
                    string userId = User.Identity.GetUserId();
                    shipmentVM.UserId = userId;
                    shipmentServices.Update(shipmentVM);
                    return RedirectToAction("Index");
                }


                if (shipmentVM.MoneyForBuy > 0)
                {
                    AccountingVM accountingVM2 = new AccountingVM();
                    AccountingServices accountingServices = new AccountingServices();
                    accountingVM2.TotalDollar = shipmentVM.MoneyForBuy;
                    accountingVM2.City = shipmentVM.ReceiverCityId;
                    accountingVM2.CustomerName = shipmentVM.ReceiverName;
                    accountingVM2.Date = DateTime.Now;
                    accountingVM2.IsCargo = true;
                    accountingVM2.Quantity = shipmentVM.PackageCount;
                    accountingVM2.Telephone = shipmentVM.ReceiverTelephoneNo;
                    accountingVM2.Notes = "Agent Money For Buy";
                    accountingServices.Insert(accountingVM2);
                }


                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                RedirectToAction("CreateTurpex", "Shipment", shipmentVM);
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
                shipmentVM.IsPtt = true;
                shipmentVM.IsDelivered = false;
                shipmentVM.IsConfirmed = true;

                shipmentServices.Insert(shipmentVM);
                var lastId = shipmentServices.GetAll().Last().Id;
                var barcode = GetShipmentBarcode(lastId);
                shipmentVM.Id = lastId;
                shipmentVM.UserId = User.Identity.GetUserId();
                shipmentVM.TrackingNo = barcode;
                shipmentServices.Update(shipmentVM);
                bool response = SendPttData(lastId, barcode);

                if (response == false)
                {
                    ViewBag.FaultDesc = "Error";
                    RedirectToAction("CreatePTT", "Shipment");
                }



                if (shipmentVM.MoneyForBuy > 0)
                {
                    AccountingVM accountingVM2 = new AccountingVM();
                    AccountingServices accountingServices = new AccountingServices();
                    accountingVM2.TotalDollar = shipmentVM.MoneyForBuy;
                    accountingVM2.City = shipmentVM.ReceiverCityId;
                    accountingVM2.CustomerName = shipmentVM.ReceiverName;
                    accountingVM2.Date = DateTime.Now;
                    accountingVM2.IsCargo = true;
                    accountingVM2.Quantity = shipmentVM.PackageCount;
                    accountingVM2.Telephone = shipmentVM.ReceiverTelephoneNo;
                    accountingVM2.Notes = "Agent Money For Buy";
                    accountingServices.Insert(accountingVM2);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                return View();
            }
        }

        public bool SendTurpexData(int id, string barcodeNumber)
        {
            ShipmentVM shipmentVM = shipmentServices.GetById(id);
            var client = new RestClient("https://pttwssgt.ptt.gov.tr/PostaKargoService/PttTurpexOperations/turpex/savedata");
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
                 + @"    ""boy"": " + shipmentVM.Height + ","
                + "\n"
                 + @"    ""deger_konulmus_ucret"": " + shipmentVM.ValueOfPackage + ","
                + "\n"
                 + @"    ""desi"": " + shipmentVM.Desi + ","
                + "\n"
                 + @"    ""en"": " + shipmentVM.Width + ","
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
                 + @"    ""gonderici_tc_no"": """ + "72685073952" + "\"" + ","
                + "\n"
                 + @"    ""gonderici_telefonu"": """ + shipmentVM.SenderTelephone + "\"" + ","
                + "\n"
                 + @"    ""gonderici_ulke_id"": ""TR""" + ","
                + "\n"
                 + @"    ""gonderici_vergi_no"":  """ + "4590672503" + "\"" + ","
                + "\n"
                 + @"    ""kapsam"": ""COM""" + ","
                + "\n"
                 + @"    ""l_muhteviyat"": ["
                + "\n"
                 + @"{"
                 + "\n"
            + @"    ""adet"": 1" + ","
                + "\n"
                 + @"    ""birim_fiyat"": " + shipmentVM.ValueOfPackage + ","
                + "\n"
                 + @"    ""doviz_cinsi"": ""EUR""" + ","
                + "\n"
                 + @"    ""esya_kodu"":  " + shipmentVM.HarmonyCode + ","
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


            if (response.StatusCode.ToString() != "Created")
            {
                return false;

            }
            return true;
        }

        [CustomAuthorizeAttribute(Roles = "Admin, Shipment")]
        public bool SendPttData(int id, string barcodeNumber)
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


            barcodeInformation.senderTCNo = "72685073952";//
            barcodeInformation.senderTaxNo = "4590672503";

            barcodeInformation.receiverName = shipmentVM.ReceiverName;//
            barcodeInformation.receiverAdres = shipmentVM.ReceiverAddress;//
            barcodeInformation.receiverMail = shipmentVM.ReceiverEmail;//
            barcodeInformation.receiverCountryId = shipmentVM.ReceiverCountryId;//
            barcodeInformation.receiverPostCode = shipmentVM.ReceiverPostalCode;//
            barcodeInformation.receiverCityID = shipmentVM.ReceiverCity;

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
            var content = new InvoceContent();

            if (shipmentVM.Content != null)
            {
                content.productName = shipmentVM.Content;
            }
            if (shipmentVM.ValueOfPackage != 0)
            {
                content.unitPrice = shipmentVM.ValueOfPackage;
            }


            content.unitPriceSpecified = true;
            content.weight = shipmentVM.Weight;
            content.count = 1;
            content.currency = "USD";
            content.origin = "TR";
            content.productHarmonyCode = "620442";

            contexes[0] = content;
            barcodeInformation.context = contexes;

            var barcodes = new BarcodeInformation[1];
            barcodes[0] = barcodeInformation;
            input.barcodeInformation = barcodes;

            LoadPortTypeClient client = new LoadPortTypeClient();
            var response = client.loadData(input);

            if (response.faultDescription != "SUCCESS")
            {
                return false;

            }


            return true;
        }
        public int GetCheckDigit(int newBarcodeDigit)

        {
            int digit1 = Convert.ToInt32(newBarcodeDigit.ToString().Substring(0, 1));
            var digit2 = Convert.ToInt32(newBarcodeDigit.ToString().Substring(1, 1));
            var digit3 = Convert.ToInt32(newBarcodeDigit.ToString().Substring(2, 1));
            var digit4 = Convert.ToInt32(newBarcodeDigit.ToString().Substring(3, 1));
            var digit5 = Convert.ToInt32(newBarcodeDigit.ToString().Substring(4, 1));

            double checkDigit1 = 72 + 54 + 36 + (digit1 * 2) + (digit2 * 3) + (digit3 * 5) + (digit4 * 9) + (digit5 * 7);

            checkDigit1 = checkDigit1 % 11;
            int checkDigit2 = Convert.ToInt32(Math.Floor(checkDigit1));
            checkDigit2 = 11 - Math.Abs(checkDigit2);


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
        [CustomAuthorizeAttribute(Roles = "Admin, Shipment")]

        public int GetCheckDigitTurpex(int newBarcodeDigit)
        {
            int digit1 = Convert.ToInt32(newBarcodeDigit.ToString().Substring(0, 1));
            var digit2 = Convert.ToInt32(newBarcodeDigit.ToString().Substring(1, 1));
            var digit3 = Convert.ToInt32(newBarcodeDigit.ToString().Substring(2, 1));
            var digit4 = Convert.ToInt32(newBarcodeDigit.ToString().Substring(3, 1));
            var digit5 = Convert.ToInt32(newBarcodeDigit.ToString().Substring(4, 1));

            double checkDigit1 = 72 + 54 + 36 + (digit1 * 2) + (digit2 * 3) + (digit3 * 5) + (digit4 * 9) + (digit5 * 7);

            checkDigit1 = checkDigit1 % 11;
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
        [CustomAuthorizeAttribute(Roles = "Admin, Shipment")]

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
        [CustomAuthorizeAttribute(Roles = "Admin, Shipment")]
        [CustomAuthorizeAttribute(Roles = "Admin, Shipment")]

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
    } 
}


 