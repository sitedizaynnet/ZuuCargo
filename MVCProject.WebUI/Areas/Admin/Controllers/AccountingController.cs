using Codaxy.WkHtmlToPdf;
using Microsoft.Ajax.Utilities;
using MVCProject.BLL.Services;
using MVCProject.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using static MVCProject.WebUI.Controllers.BaseController;

namespace MVCProject.WebUI.Areas.Admin.Controllers
{
    public class AccountingController : BaseController
    {
        // GET: Admin, Accounter/Accounting
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]

        public ActionResult Index()
           
        {

          
            return View();
        }
        AccountingServices accountingServices = new AccountingServices();
        AccountingProductsServices accountingProductsServices = new AccountingProductsServices();
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult GetAccounting()
        {

            var employees = accountingServices.GetAll().OrderBy(a => a.CustomerName).ToList();
            var jsonResult = Json(new { data = employees }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
     
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult UpdateAllToAccounting()
        {
            List<AccountingVM> accountingVMs = new List<AccountingVM>();
            AccountingServices accountingServices = new AccountingServices();
            accountingVMs = accountingServices.GetAll().ToList();

            foreach (AccountingVM accountingVM in accountingVMs)
            {

                try
                {
                    AddAccountingFromId(accountingVM.Id);
                }
                catch (Exception)
                {

                 
                }
            }
            return RedirectToAction("Index");


        }


        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult Save(AccountingVM acc)
        {
          

                if (acc.Id > 0)
                {
                    //Edit 
                    AccountingVM account = accountingServices.GetById(acc.Id);
                    if (account != null)
                    {
                    account.Date = acc.Date;
                    account.CustomerName = acc.CustomerName;
                    account.Telephone = acc.Telephone;
                    account.City = acc.City;
                    account.CargoPrice = acc.CargoPrice;
                    account.TotalDollar = acc.TotalDollar;
                    account.TotalLira = acc.TotalLira;
                    account.Exchange = acc.Exchange;
                    account.Expensive = acc.Expensive;
                    account.CargoPrice = acc.CargoPrice;
                    account.Balance = acc.Balance;
                    account.Quantity = acc.Quantity;
                    account.ProductName = acc.ProductName;
                    account.InvoiceNo = acc.InvoiceNo;
                    account.IsPaid = true;
                    account.IsCargo = acc.IsCargo;
                    account.IsDelivered= acc.IsDelivered;
                    account.Notes = acc.Notes;
                    account.Tax = acc.Tax;
                    account.InvoiceLink1 = acc.InvoiceLink1;
                    account.InvoiceLink2 = acc.InvoiceLink2;
                    account.InvoiceLink3 = acc.InvoiceLink3;
                    account.InvoiceLink4 = acc.InvoiceLink4;
                    account.InvoiceLink5 = acc.InvoiceLink5;
                   
                    accountingServices.Update(account);
                    //try
                    //{
                    //    AddAccountingFromId(acc.Id);
                    //}
                    //catch (Exception)
                    //{


                    //}
         
                    }

            }
            else
            {
                acc.IsPaid = true;
                accountingServices.Insert(acc);
                //acc.Id = accountingServices.GetAll().Last().Id;
                //try
                //{
                //    AddAccountingFromId(acc.Id);

                //}
                //catch (Exception)
                //{

                //}
            }

            if (acc.IsSendSMSAfterSave == true)
            {

                try
                {
                   var isSend = SendInvoiceSMSById(accountingServices.GetAll().Last().Id);
                }
                catch (Exception)
                {


                }
            }





            return View("Index");
        }
        [HttpGet]
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult Save(int id)
        {
            ViewBag.Date = DateTime.Now.ToShortDateString();
            AccountingVM account = new AccountingVM();
            if (id != 0)
            {
                account = accountingServices.GetById(id);
            }
            if (id == 0)
            {
                account.Date = DateTime.Now;
                Random rnd = new Random();
                Random r = new Random();
                int rInt = r.Next(50000, 100000); //for ints

                account.InvoiceNo = rInt;
            }
         
          
            return View(account);
        }

        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        [HttpPost]
        public ActionResult UploadFiles()
        {
            string[] filePath = {"","","","","" };

            string path = Server.MapPath("~/Content/Uploads/");
            HttpFileCollectionBase files = Request.Files;
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase file = files[i];
                file.SaveAs(path + file.FileName);
                filePath[i] = "http://www.ZuuCargo.com/Content/Uploads/" + file.FileName;
            }
            return Json( filePath);
        }

        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        [HttpPost]
        public ActionResult Delete(int id)
        {

      
            AccountingVM account = new AccountingVM();
            account = accountingServices.GetById(id);
            accountingServices.Delete(account);
            List<AccountingProductsVM> accountingProductsVMs = accountingProductsServices.GetAll().Where(x => x.AccountingID == id).ToList();
            foreach (AccountingProductsVM product in accountingProductsVMs)

            {
                accountingProductsServices.Delete(product);
            }

          

            return View("Index");

        }

        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]

        [HttpGet]
        public ActionResult DeleteGet(int id)
        {

            AccountingVM account = new AccountingVM();
            account = accountingServices.GetById(id);
            if (account != null)
            {
                return View(account);
            }
            else
            {
                return HttpNotFound();
            }

        }

        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult AddProducts(int id)
        {
            ViewBag.AccountingId = id;
       


            return View();
        }

        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        [HttpPost]
        public ActionResult AddProducts(AccountingProductsVM product)
        {

            accountingProductsServices.Insert(product);
            CalculateAccounting((int)product.AccountingID);
            return View("Index");
        }
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        [HttpGet]
        public ActionResult DeleteProducts(int id)
        {
            AccountingProductsServices accountingProductsServices = new AccountingProductsServices();
            AccountingProductsVM accountingProductsVM = accountingProductsServices.GetById(id);
            accountingProductsServices.Delete(accountingProductsVM);
            CalculateAccounting((int)accountingProductsVM.AccountingID);


            return View("Index");
        }
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult EditProducts(int id)
        {
            AccountingProductsServices accountingProductsServices = new AccountingProductsServices();
            AccountingProductsVM accountingProductsVM = accountingProductsServices.GetById(id);
           
            return View(accountingProductsVM);
        }
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        [HttpPost]
        public ActionResult EditProducts(AccountingProductsVM vm)
        {

            accountingProductsServices.Update(vm);
            ViewBag.AccountingId = vm.AccountingID;
            CalculateAccounting((int)vm.AccountingID);
            return View("AddProducts");
        }

        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult GetProducts(int id)
        {
            var products = accountingProductsServices.GetAll().Where(x=> x.AccountingID == id).ToList();
            return Json(new { data = products }, JsonRequestBehavior.AllowGet);

        }

        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public void CalculateAccounting(int id)
        {
            try
            {
                AccountingVM accountingVM = accountingServices.GetById(id);
                List<AccountingProductsVM> accountingProductsList = accountingProductsServices.GetAll().Where(x => x.AccountingID == id).ToList();

                double priceDollar = 0;
                double priceTL = 0;
                double priceCargo = 0;
                foreach (AccountingProductsVM product in accountingProductsList)
                {
                    if (product.PriceDollar != null)
                    {
                        priceDollar = priceDollar + (double)product.PriceDollar;
                        if (product.Tax !=null)
                        {
                            priceDollar = priceDollar + (double)product.Tax;
                        }
                    }
                    if (product.PriceLira != null)
                    {
                        priceTL = priceTL+ (double)product.PriceLira;
                        if (product.Tax != null)
                        {
                            priceTL = priceTL+ (double)product.Tax;
                        }
                    }
                    if (product.Cargo != null)
                    {
                        priceCargo = priceCargo + (double)product.Cargo;
                       
                    }
                    accountingVM.CargoPrice = priceCargo;
                    accountingVM.Balance = accountingVM.Balance - priceDollar;
                }

                accountingServices.Update(accountingVM);


            }
            catch (Exception)
            {

               
            }
        }

        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult GetReport (string[] function_param) {
            DateTime start = Convert.ToDateTime("01.01.0001");
            DateTime end = Convert.ToDateTime("01.01.3001");

            try
            {
                start = Convert.ToDateTime(function_param[1]);
                end = Convert.ToDateTime(function_param[2]);
            }
            catch (Exception)
            {

              
            }
            string customerName = accountingServices.GetById(Convert.ToInt32(function_param[0])).CustomerName;


            AccountReportVM accountReport = new AccountReportVM();
            List<AccountingVM> accountingVM = accountingServices.GetAll().Where(x => x.CustomerName == customerName && x.Date >= start && x.Date <= end).ToList();
        
               
          
            double totalAccountDollar = 0;
            double totalAccountTL = 0;
            int totalQuantity = 0;
            double totalCargo = 0;
            double totalExpensive = 0;
            double totalExchange = 0;
          


            foreach (AccountingVM account in accountingVM)
                {
                if (account.TotalDollar != null)
                {
                    totalAccountDollar = totalAccountDollar + (double)account.TotalDollar;
                }
                if (account.TotalLira != null)
                {
                    totalAccountTL = totalAccountTL + (double)account.TotalLira;

                }
                if (account.CargoPrice != null)
                {
                    totalCargo = totalCargo + (double)account.CargoPrice;

                }
                if (account.Exchange != null)
                {
                    totalExchange = totalExchange + (double)account.Exchange;

                }
                if (account.Expensive != null)
                {
                    totalExpensive = totalExpensive + (double)account.Expensive;

                }
               
                totalQuantity = totalQuantity + (int)account.Quantity;
                
            }
            //accountReport.TotalCargo = accountingProductsServices.GetAll().Where(x => x.AccountingID == Convert.ToInt32(function_param[0])).ToList();
            
            accountReport.TotalCargo = totalCargo;
            accountReport.TotalSalesDollar = totalAccountDollar;
            accountReport.TotalSalesTL = totalAccountTL;
            accountReport.TotalExchange = totalExchange;
            accountReport.TotalExpensive = totalExpensive;
            accountReport.TotalQuantity = totalQuantity;
          
            accountReport.CustomerName = accountingServices.GetById(Convert.ToInt32(function_param[0])).CustomerName;
           


            return View(accountReport);
        }

        [AllowAnonymous]
        public ActionResult ScanBarcodeArrivalSMS()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ScanBarcodeAddAccounting()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ScanBarcodeInvoiceSMS()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult AddAccountingFromBarcode(int Id)
        {

            AccountingVM accounting = new AccountingVM();
            accounting = accountingServices.GetAll().Where(x=>x.InvoiceNo == Id).FirstOrDefault();
            TotalCargoVM costsVM = new TotalCargoVM();
            costsVM.Date = DateTime.Now;
            costsVM.OrderTotal = accounting.CargoPrice;
            accounting.IsDelivered = true;
            try
            {
                costsVM.Name = accounting.CustomerName;
                costsVM.Category = "Cargo Cost";
                costsVM.Telephone = accounting.Telephone;
                costsVM.Notes = accounting.City + "Is Paid? : " + accounting.IsPaid;
                TotalCargoService costsService = new TotalCargoService();
                costsService.Insert(costsVM);
            }
            catch (Exception)
            {

              
            }

            try
            {
                ArrivedMoneyVM arrivedMoneyVM = new ArrivedMoneyVM();
                arrivedMoneyVM.OrderId = accounting.Id;
                arrivedMoneyVM.Date = DateTime.Now;
                arrivedMoneyVM.Amount = (double)accounting.TotalDollar;

                ArrivedMoneyServices arrivedMoneyServices = new ArrivedMoneyServices();
                arrivedMoneyServices.Insert(arrivedMoneyVM);

            }
            catch (Exception)
            {

               
            }
            //MyBalanceServices myBalanceServices = new MyBalanceServices();
            //MyBalanceVM myBalanceVM = new MyBalanceVM();
            //myBalanceVM.Amount = -(double)accounting.TotalDollar;
            //myBalanceVM.Date = DateTime.Now;
            //myBalanceVM.OrderId = accounting.Id;
            //myBalanceVM.Notes = "Customer :" + accounting.CustomerName + " City : " + accounting.City;
            //myBalanceServices.Insert(myBalanceVM);


            try
            {
                ExpensiveVM expensiveVM = new ExpensiveVM();


                expensiveVM.Name = accounting.CustomerName;
                expensiveVM.DateAdded = DateTime.Now;
                expensiveVM.Total = (double)accounting.Expensive;
                expensiveVM.Phone = accounting.Telephone;
                expensiveVM.Notes = accounting.City;

                ExpensiveServices expensiveServices = new ExpensiveServices();
                expensiveServices.Insert(expensiveVM);

            }
            catch (Exception)
            {

               
            }
            try
            {

                ArrivedExchangeVM arrivedExchangeVM = new ArrivedExchangeVM();

                arrivedExchangeVM.Name = accounting.CustomerName;
                arrivedExchangeVM.DateAdded = DateTime.Now;
                arrivedExchangeVM.Total = (double)accounting.Exchange;
                arrivedExchangeVM.Phone = accounting.Telephone;
                arrivedExchangeVM.Notes = accounting.City;

                ArrivedExchangeServices arrivedExchangeServices = new ArrivedExchangeServices();
                arrivedExchangeServices.Insert(arrivedExchangeVM);

            }
            catch (Exception)
            {

               
            }
            accounting.IsPaid = true;
            accountingServices.Update(accounting);


            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult AddAccountingFromId(int Id)
        {

            AccountingVM accounting = new AccountingVM();
            accounting = accountingServices.GetAll().Where(x => x.Id == Id).FirstOrDefault();
            accounting.IsDelivered = true;
            if (accounting.CargoPrice>0)
            {

                TotalCargoVM costsVM = new TotalCargoVM();
                costsVM.Date = DateTime.Now;
                costsVM.OrderTotal = accounting.CargoPrice;

                costsVM.Name = accounting.CustomerName;
                costsVM.Category = "Cargo Cost";
                costsVM.Telephone = accounting.Telephone;
                costsVM.Notes = accounting.City + "Is Paid? : " + accounting.IsPaid;
                TotalCargoService costsService = new TotalCargoService();
                costsService.Insert(costsVM);
            }

            //ArrivedMoneyVM arrivedMoneyVM = new ArrivedMoneyVM();
            //arrivedMoneyVM.OrderId = accounting.Id;
            //arrivedMoneyVM.Date = DateTime.Now;
            //arrivedMoneyVM.Amount = (double)accounting.TotalDollar;

            MyBalanceServices myBalanceServices = new MyBalanceServices();
            MyBalanceVM myBalanceVM = new MyBalanceVM();
            myBalanceVM.Amount = -(double)accounting.TotalDollar;
            myBalanceVM.Date = DateTime.Now;
            myBalanceVM.OrderId = accounting.Id;
            myBalanceVM.Notes = "Customer :" + accounting.CustomerName + " City : " + accounting.City;
            myBalanceServices.Insert(myBalanceVM);

            //ArrivedMoneyServices arrivedMoneyServices = new ArrivedMoneyServices();
            //arrivedMoneyServices.Insert(arrivedMoneyVM);
            if (accounting.Expensive > 0)
            {

                ExpensiveVM expensiveVM = new ExpensiveVM();
            
                expensiveVM.Name = accounting.CustomerName;
                expensiveVM.Total = (double)accounting.Expensive;
                expensiveVM.Phone = accounting.Telephone;
                expensiveVM.Notes = accounting.City;
                ExpensiveServices expensiveServices = new ExpensiveServices();
                expensiveServices.Insert(expensiveVM);
            }

            accounting.IsPaid = true;
            accountingServices.Update(accounting);


            return RedirectToAction("Index");
        }
        [AllowAnonymous]
        public ActionResult PrintInvoice(int Id)
        {
           
            AccountingVM accounting = new AccountingVM();
            accounting = accountingServices.GetById(Id);
          
            return View(accounting);
        }
        public ActionResult InvoiceWithEx(int Id)
        {

            AccountingVM accounting = new AccountingVM();
            accounting = accountingServices.GetById(Id);

            return View(accounting);
        }
        public ActionResult Pdf(int id)
        {
            string url = "http://www.ZuuCargo.com/Admin/Accounting/PrintInvoice/" + id;
            string outputfile = id + ".pdf";
            SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();
            SelectPdf.PdfDocument doc = converter.ConvertUrl(url);
            // save pdf document
            byte[] pdf = doc.Save();

            // close pdf document
            doc.Close();
            // return resulted pdf document
            FileResult fileResult = new FileContentResult(pdf, "application/pdf");
            fileResult.FileDownloadName = "Invoice.pdf";
            return fileResult;
           
        }
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]


        public bool SendInvoiceSMSById(int id)
        {
            AccountingVM accountingVM = new AccountingVM();
            AccountingServices accountingServices = new AccountingServices();
            accountingVM = accountingServices.GetById(id);
            string phoneNumber = accountingVM.Telephone;
            //SEND SMS
            string html = string.Empty;


            string mesajMetni = "Thank you for Shopping with VARShopping. You can download your invoice(" + accountingVM.InvoiceNo + ") with this link http://www.ZuuCargo.com/Admin/Accounting/pdf/" + id;


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



            //SEND INVOICELINK
            if (accountingVM.InvoiceLink1 != null)
            {

                string html2 = string.Empty;


                string mesajMetni2 = "You can see your invoice with this link " + accountingVM.InvoiceLink1;


                string telNo2 = phoneNumber;

                string url2 = @"https://gw.cmtelecom.com/gateway.ashx?producttoken=6A10F3EB-0AE0-4114-BD13-902EB0C57A58&body=" + mesajMetni2 + "&to=" + telNo2.Trim() + "&from=ZuuCargo&reference=" + id + 2;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(url2);
                request2.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response2 = (HttpWebResponse)request2.GetResponse())
                using (Stream stream2 = response2.GetResponseStream())
                using (StreamReader reader2 = new StreamReader(stream2))
                {
                    html2 = reader2.ReadToEnd();
                }

            }

            //SEND INVOICELINK
            if (accountingVM.InvoiceLink2 != null)
            {

                string html3 = string.Empty;


                string mesajMetni3 = "You can see your invoice with this link " + accountingVM.InvoiceLink2;


                string telNo3 = phoneNumber;

                string url3 = @"https://gw.cmtelecom.com/gateway.ashx?producttoken=6A10F3EB-0AE0-4114-BD13-902EB0C57A58&body=" + mesajMetni3 + "&to=" + telNo3.Trim() + "&from=ZuuCargo&reference=" + id + 3;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                HttpWebRequest request3 = (HttpWebRequest)WebRequest.Create(url3);
                request3.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response3 = (HttpWebResponse)request3.GetResponse())
                using (Stream stream3 = response3.GetResponseStream())
                using (StreamReader reader3 = new StreamReader(stream3))
                {
                    html3 = reader3.ReadToEnd();
                }

            }


            //SEND INVOICELINK
            if (accountingVM.InvoiceLink3 != null)
            {

                string html4 = string.Empty;


                string mesajMetni4 = "You can see your invoice with this link " + accountingVM.InvoiceLink3;


                string telNo4 = phoneNumber;

                string url4 = @"https://gw.cmtelecom.com/gateway.ashx?producttoken=6A10F3EB-0AE0-4114-BD13-902EB0C57A58&body=" + mesajMetni4 + "&to=" + telNo4.Trim() + "&from=ZuuCargo&reference=" + id + 4;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                HttpWebRequest request4 = (HttpWebRequest)WebRequest.Create(url4);
                request4.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response4 = (HttpWebResponse)request4.GetResponse())
                using (Stream stream4 = response4.GetResponseStream())
                using (StreamReader reader4 = new StreamReader(stream4))
                {
                    html4 = reader4.ReadToEnd();
                }

            }

            //SEND INVOICELINK
            if (accountingVM.InvoiceLink4 != null)
            {

                string html5 = string.Empty;


                string mesajMetni5 = "You can see your invoice with this link " + accountingVM.InvoiceLink4;


                string telNo5 = phoneNumber;

                string url5 = @"https://gw.cmtelecom.com/gateway.ashx?producttoken=6A10F3EB-0AE0-4114-BD13-902EB0C57A58&body=" + mesajMetni5 + "&to=" + telNo5.Trim() + "&from=ZuuCargo&reference=" + id + 5;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                HttpWebRequest request5 = (HttpWebRequest)WebRequest.Create(url5);
                request5.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response5 = (HttpWebResponse)request5.GetResponse())
                using (Stream stream5 = response5.GetResponseStream())
                using (StreamReader reader5 = new StreamReader(stream5))
                {
                    html5 = reader5.ReadToEnd();
                }

            }

            //SEND INVOICELINK
            if (accountingVM.InvoiceLink5 != null)
            {

                string html6 = string.Empty;


                string mesajMetni6 = "You can see your invoice with this link " + accountingVM.InvoiceLink5;


                string telNo6 = phoneNumber;

                string url6 = @"https://gw.cmtelecom.com/gateway.ashx?producttoken=6A10F3EB-0AE0-4114-BD13-902EB0C57A58&body=" + mesajMetni6 + "&to=" + telNo6.Trim() + "&from=ZuuCargo&reference=" + id + 6;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                HttpWebRequest request6 = (HttpWebRequest)WebRequest.Create(url6);
                request6.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response6 = (HttpWebResponse)request6.GetResponse())
                using (Stream stream6 = response6.GetResponseStream())
                using (StreamReader reader6 = new StreamReader(stream6))
                {
                    html6 = reader6.ReadToEnd();
                }

            }
            return true;
        }
        public ActionResult SendInvoiceSMS(int id)
        {
            AccountingVM accountingVM = new AccountingVM();
            AccountingServices accountingServices = new AccountingServices();
            accountingVM = accountingServices.GetById(id);
            string phoneNumber = accountingVM.Telephone;
            //SEND SMS
            string html = string.Empty;


            string mesajMetni = "Thank you for Shopping with VARShopping. You can download your invoice("+ accountingVM.InvoiceNo +") with this link http://www.ZuuCargo.com/Admin/Accounting/pdf/"+id;


                string telNo = phoneNumber;

            string url = @"https://gw.cmtelecom.com/gateway.ashx?producttoken=6A10F3EB-0AE0-4114-BD13-902EB0C57A58&body=" + mesajMetni + "&to=" + telNo.Trim() + "&from=ZuuCargo&reference=" + id+1;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }



            //SEND INVOICELINK
            if (accountingVM.InvoiceLink1 != null)
            {

                string html2 = string.Empty;


                string mesajMetni2 = "You can see your invoice with this link " + accountingVM.InvoiceLink1;


                string telNo2 = phoneNumber;

                string url2 = @"https://gw.cmtelecom.com/gateway.ashx?producttoken=6A10F3EB-0AE0-4114-BD13-902EB0C57A58&body=" + mesajMetni2 + "&to=" + telNo2.Trim() + "&from=ZuuCargo&reference=" + id+2;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(url2);
                request2.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response2 = (HttpWebResponse)request2.GetResponse())
                using (Stream stream2 = response2.GetResponseStream())
                using (StreamReader reader2 = new StreamReader(stream2))
                {
                    html2 = reader2.ReadToEnd();
                }
               
            }

            //SEND INVOICELINK
            if (accountingVM.InvoiceLink2 != null)
            {

                string html3 = string.Empty;


                string mesajMetni3 = "You can see your invoice with this link " + accountingVM.InvoiceLink2;


                string telNo3 = phoneNumber;

                string url3 = @"https://gw.cmtelecom.com/gateway.ashx?producttoken=6A10F3EB-0AE0-4114-BD13-902EB0C57A58&body=" + mesajMetni3 + "&to=" + telNo3.Trim() + "&from=ZuuCargo&reference=" + id+3;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                HttpWebRequest request3 = (HttpWebRequest)WebRequest.Create(url3);
                request3.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response3 = (HttpWebResponse)request3.GetResponse())
                using (Stream stream3 = response3.GetResponseStream())
                using (StreamReader reader3 = new StreamReader(stream3))
                {
                    html3 = reader3.ReadToEnd();
                }

            }


            //SEND INVOICELINK
            if (accountingVM.InvoiceLink3 != null)
            {

                string html4 = string.Empty;


                string mesajMetni4 = "You can see your invoice with this link " + accountingVM.InvoiceLink3;


                string telNo4 = phoneNumber;

                string url4 = @"https://gw.cmtelecom.com/gateway.ashx?producttoken=6A10F3EB-0AE0-4114-BD13-902EB0C57A58&body=" + mesajMetni4 + "&to=" + telNo4.Trim() + "&from=ZuuCargo&reference=" + id+4;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                HttpWebRequest request4 = (HttpWebRequest)WebRequest.Create(url4);
                request4.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response4 = (HttpWebResponse)request4.GetResponse())
                using (Stream stream4 = response4.GetResponseStream())
                using (StreamReader reader4 = new StreamReader(stream4))
                {
                    html4 = reader4.ReadToEnd();
                }

            }

            //SEND INVOICELINK
            if (accountingVM.InvoiceLink4 != null)
            {

                string html5 = string.Empty;


                string mesajMetni5 = "You can see your invoice with this link " + accountingVM.InvoiceLink4;


                string telNo5 = phoneNumber;

                string url5 = @"https://gw.cmtelecom.com/gateway.ashx?producttoken=6A10F3EB-0AE0-4114-BD13-902EB0C57A58&body=" + mesajMetni5 + "&to=" + telNo5.Trim() + "&from=ZuuCargo&reference=" + id+5;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                HttpWebRequest request5 = (HttpWebRequest)WebRequest.Create(url5);
                request5.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response5 = (HttpWebResponse)request5.GetResponse())
                using (Stream stream5 = response5.GetResponseStream())
                using (StreamReader reader5 = new StreamReader(stream5))
                {
                    html5 = reader5.ReadToEnd();
                }

            }

            //SEND INVOICELINK
            if (accountingVM.InvoiceLink5 != null)
            {

                string html6 = string.Empty;


                string mesajMetni6 = "You can see your invoice with this link " + accountingVM.InvoiceLink5;


                string telNo6 = phoneNumber;

                string url6 = @"https://gw.cmtelecom.com/gateway.ashx?producttoken=6A10F3EB-0AE0-4114-BD13-902EB0C57A58&body=" + mesajMetni6 + "&to=" + telNo6.Trim() + "&from=ZuuCargo&reference=" + id+6;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                HttpWebRequest request6 = (HttpWebRequest)WebRequest.Create(url6);
                request6.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response6 = (HttpWebResponse)request6.GetResponse())
                using (Stream stream6 = response6.GetResponseStream())
                using (StreamReader reader6 = new StreamReader(stream6))
                {
                    html6 = reader6.ReadToEnd();
                }

            }
            return View();
        }
        public ActionResult SendInvoiceSMSBarcode(int id)
        {
            AccountingVM accountingVM = new AccountingVM();
            AccountingServices accountingServices = new AccountingServices();
            accountingVM = accountingServices.GetAll().Where(x=>x.InvoiceNo == id).FirstOrDefault();
            id = accountingVM.Id;
            string phoneNumber = accountingVM.Telephone;
            //SEND SMS
            string html = string.Empty;


            string mesajMetni = "Thank you for Shopping with VARShopping. You can download your invoice(" + accountingVM.InvoiceNo + ") with this link http://www.ZuuCargo.com/Admin/Accounting/pdf/" + id;


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



            //SEND INVOICELINK
            if (accountingVM.InvoiceLink1 != null)
            {

                string html2 = string.Empty;


                string mesajMetni2 = "You can see your invoice with this link " + accountingVM.InvoiceLink1;


                string telNo2 = phoneNumber;

                string url2 = @"https://gw.cmtelecom.com/gateway.ashx?producttoken=6A10F3EB-0AE0-4114-BD13-902EB0C57A58&body=" + mesajMetni2 + "&to=" + telNo2.Trim() + "&from=ZuuCargo&reference=" + id + 2;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(url2);
                request2.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response2 = (HttpWebResponse)request2.GetResponse())
                using (Stream stream2 = response2.GetResponseStream())
                using (StreamReader reader2 = new StreamReader(stream2))
                {
                    html2 = reader2.ReadToEnd();
                }

            }

            //SEND INVOICELINK
            if (accountingVM.InvoiceLink2 != null)
            {

                string html3 = string.Empty;


                string mesajMetni3 = "You can see your invoice with this link " + accountingVM.InvoiceLink2;


                string telNo3 = phoneNumber;

                string url3 = @"https://gw.cmtelecom.com/gateway.ashx?producttoken=6A10F3EB-0AE0-4114-BD13-902EB0C57A58&body=" + mesajMetni3 + "&to=" + telNo3.Trim() + "&from=ZuuCargo&reference=" + id + 3;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                HttpWebRequest request3 = (HttpWebRequest)WebRequest.Create(url3);
                request3.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response3 = (HttpWebResponse)request3.GetResponse())
                using (Stream stream3 = response3.GetResponseStream())
                using (StreamReader reader3 = new StreamReader(stream3))
                {
                    html3 = reader3.ReadToEnd();
                }

            }


            //SEND INVOICELINK
            if (accountingVM.InvoiceLink3 != null)
            {

                string html4 = string.Empty;


                string mesajMetni4 = "You can see your invoice with this link " + accountingVM.InvoiceLink3;


                string telNo4 = phoneNumber;

                string url4 = @"https://gw.cmtelecom.com/gateway.ashx?producttoken=6A10F3EB-0AE0-4114-BD13-902EB0C57A58&body=" + mesajMetni4 + "&to=" + telNo4.Trim() + "&from=ZuuCargo&reference=" + id + 4;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                HttpWebRequest request4 = (HttpWebRequest)WebRequest.Create(url4);
                request4.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response4 = (HttpWebResponse)request4.GetResponse())
                using (Stream stream4 = response4.GetResponseStream())
                using (StreamReader reader4 = new StreamReader(stream4))
                {
                    html4 = reader4.ReadToEnd();
                }

            }

            //SEND INVOICELINK
            if (accountingVM.InvoiceLink4 != null)
            {

                string html5 = string.Empty;


                string mesajMetni5 = "You can see your invoice with this link " + accountingVM.InvoiceLink4;


                string telNo5 = phoneNumber;

                string url5 = @"https://gw.cmtelecom.com/gateway.ashx?producttoken=6A10F3EB-0AE0-4114-BD13-902EB0C57A58&body=" + mesajMetni5 + "&to=" + telNo5.Trim() + "&from=ZuuCargo&reference=" + id + 5;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                HttpWebRequest request5 = (HttpWebRequest)WebRequest.Create(url5);
                request5.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response5 = (HttpWebResponse)request5.GetResponse())
                using (Stream stream5 = response5.GetResponseStream())
                using (StreamReader reader5 = new StreamReader(stream5))
                {
                    html5 = reader5.ReadToEnd();
                }

            }

            //SEND INVOICELINK
            if (accountingVM.InvoiceLink5 != null)
            {

                string html6 = string.Empty;


                string mesajMetni6 = "You can see your invoice with this link " + accountingVM.InvoiceLink5;


                string telNo6 = phoneNumber;

                string url6 = @"https://gw.cmtelecom.com/gateway.ashx?producttoken=6A10F3EB-0AE0-4114-BD13-902EB0C57A58&body=" + mesajMetni6 + "&to=" + telNo6.Trim() + "&from=ZuuCargo&reference=" + id + 6;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                HttpWebRequest request6 = (HttpWebRequest)WebRequest.Create(url6);
                request6.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response6 = (HttpWebResponse)request6.GetResponse())
                using (Stream stream6 = response6.GetResponseStream())
                using (StreamReader reader6 = new StreamReader(stream6))
                {
                    html6 = reader6.ReadToEnd();
                }

            }
            return new HttpStatusCodeResult(200);
        }

        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]

        public ActionResult SendPackageArrivedSMS(int id)
        {
            AccountingVM accountingVM = new AccountingVM();
            AccountingServices accountingServices = new AccountingServices();
            accountingVM = accountingServices.GetById(id);
            accountingVM.IsDelivered = true;
            accountingServices.Update(accountingVM);
            string phoneNumber = accountingVM.Telephone;
            //SEND SMS
            string html = string.Empty;
            string mesajMetni = "Your package (" + accountingVM.InvoiceNo + ") has arrived to the Zaxo and is ready to be picked up.";
            string telNo = phoneNumber;

            string url = @"https://gw.cmtelecom.com/gateway.ashx?producttoken=6A10F3EB-0AE0-4114-BD13-902EB0C57A58&body=" + mesajMetni + "&to=" + telNo.Trim() + "&from=ZuuCargo&reference=" + id;
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


        public ActionResult SendPackageArrivedSMSBarcode(int id)
        {
            AccountingVM accountingVM = new AccountingVM();
            AccountingServices accountingServices = new AccountingServices();
            accountingVM = accountingServices.GetAll().Where(x=>x.InvoiceNo == id).FirstOrDefault();
            id = accountingVM.Id;
            accountingVM.IsDelivered = true;
            accountingServices.Update(accountingVM);
            string phoneNumber = accountingVM.Telephone;
            //SEND SMS
            string html = string.Empty;
            string mesajMetni = "Your package (" + accountingVM.InvoiceNo + ") has arrived to the Zaxo and is ready to be picked up.";
            string telNo = phoneNumber;

            string url = @"https://gw.cmtelecom.com/gateway.ashx?producttoken=6A10F3EB-0AE0-4114-BD13-902EB0C57A58&body=" + mesajMetni + "&to=" + telNo.Trim() + "&from=ZuuCargo&reference=" + id;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }
            return new HttpStatusCodeResult(200);
        }

        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult Report()
        {
            List<CustomerListVM> customerList = new List<CustomerListVM>();
            List<AccountingVM> accountingVMs = new List<AccountingVM>();
            accountingVMs = accountingServices.GetAll().ToList();
            accountingVMs = accountingVMs.DistinctBy(x => x.CustomerName).ToList();

            IList<SelectListItem> items = new List<SelectListItem>();
            foreach (AccountingVM item in accountingVMs)
            {
                CustomerListVM customerListVM = new CustomerListVM();
                customerListVM.CustomerName = item.CustomerName;
                customerListVM.AcccountId = item.Id;
                customerList.Add(customerListVM);
                items.Add(
                     new SelectListItem { Text = item.CustomerName , Value = item.Id.ToString() }
                    );
            }

        
            return View(items);
        }
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult AllReport(string[] function_param)
        {
            DateTime start = Convert.ToDateTime("01.01.0001");
            DateTime end = Convert.ToDateTime("01.01.3001");

            try
            {
                start = Convert.ToDateTime(function_param[0]);
                end = Convert.ToDateTime(function_param[1]);
            }
            catch (Exception)
            {


            }


            AccountReportVM accountReport = new AccountReportVM();
            List<AccountingVM> accountingVM = accountingServices.GetAll().Where(x=> x.Date >= start && x.Date <= end).ToList();



            double totalAccountDollar = 0;
            double totalAccountTL = 0;
            int totalQuantity = 0;
            double totalCargo = 0;
            double totalExpensive = 0;
            double totalExchange = 0;
            double totalDelivered = 0;

            foreach (AccountingVM account in accountingVM)
            {
                if (account.IsPaid == true)
                {
                    if (account.TotalDollar != null)
                    {
                        totalAccountDollar = totalAccountDollar + (double)account.TotalDollar;
                    }
                    if (account.TotalLira != null)
                    {
                        totalAccountTL = totalAccountTL + (double)account.TotalLira;

                    }
                    if (account.CargoPrice != null)
                    {
                        totalCargo = totalCargo + (double)account.CargoPrice;

                    }
                    if (account.Exchange != null)
                    {
                        totalExchange = totalExchange + (double)account.Exchange;

                    }
                    if (account.Expensive != null)
                    {
                        totalExpensive = totalExpensive + (double)account.Expensive;

                    }
                    if (account.Quantity != null)
                    {
                        totalQuantity = totalQuantity + (int)account.Quantity;

                    }
                    if (account.IsDelivered != null)
                    {
                        if ((bool)account.IsDelivered == true)
                        {
                            try
                            {
                                totalDelivered = (double)account.TotalDollar + totalDelivered;
                            }
                            catch (Exception)
                            {


                            }

                        }
                    }

                }
            }
            //accountReport.TotalCargo = accountingProductsServices.GetAll().Where(x => x.AccountingID == Convert.ToInt32(function_param[0])).ToList();

            accountReport.TotalCargo = totalCargo;
            accountReport.TotalSalesDollar = totalAccountDollar;
            accountReport.TotalSalesTL = totalAccountTL;
            accountReport.TotalExchange = totalExchange;
            accountReport.TotalExpensive = totalExpensive;
            accountReport.TotalQuantity = totalQuantity;
            accountReport.TotalDelivered = totalDelivered;


            return View(accountReport);
        }
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult AllRefund(string[] function_param)
        {
            DateTime start = Convert.ToDateTime("01.01.0001");
            DateTime end = Convert.ToDateTime("01.01.3001");

            try
            {
                start = Convert.ToDateTime(function_param[0]);
                end = Convert.ToDateTime(function_param[1]);
            }
            catch (Exception)
            {


            }
            RefundService refundService = new RefundService();

            AccountReportVM accountReport = new AccountReportVM();
            List<RefundVM> refundList = refundService.GetAll().ToList();



            double totalAccountDollar = 0;
            double totalAccountTL = 0;
            int totalQuantity = 0;
         
            foreach (RefundVM refund in refundList)
            {

                    totalAccountDollar = totalAccountDollar + (double)refund.TotalDollar;

                    totalAccountTL = totalAccountTL + (double)refund.TotalLira;

                    totalQuantity = totalQuantity + (int)refund.Adet;



            }
            //accountReport.TotalCargo = accountingProductsServices.GetAll().Where(x => x.AccountingID == Convert.ToInt32(function_param[0])).ToList();

            accountReport.TotalRefundDolar = totalAccountDollar;
            accountReport.TotalRefundLira = totalAccountTL;
            accountReport.TotalRefundAdet = totalQuantity;
           


            return View(accountReport);
        }


        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult AllLost(string[] function_param)
        {
            DateTime start = Convert.ToDateTime("01.01.0001");
            DateTime end = Convert.ToDateTime("01.01.3001");

            try
            {
                start = Convert.ToDateTime(function_param[0]);
                end = Convert.ToDateTime(function_param[1]);
            }
            catch (Exception)
            {


            }
            LostOrderServices lostOrderServices = new LostOrderServices();

            AccountReportVM accountReport = new AccountReportVM();
            List<LostOrderVM> lostList = lostOrderServices.GetAll().ToList();



            double totalAccountDollar = 0;
            double totalAccountTL = 0;
            int totalQuantity = 0;
           
            foreach (LostOrderVM lost in lostList)
            {

                totalAccountDollar = totalAccountDollar + (double)lost.TotalDollar;

                totalAccountTL = totalAccountTL + (double)lost.TotalLira;

                totalQuantity = totalQuantity + (int)lost.Adet;



            }
            //accountReport.TotalCargo = accountingProductsServices.GetAll().Where(x => x.AccountingID == Convert.ToInt32(function_param[0])).ToList();

            accountReport.TotalLostDolar = totalAccountDollar;
            accountReport.TotalLostLira = totalAccountTL;
            accountReport.TotalLostAdet = totalQuantity;



            return View(accountReport);
        }
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult CargoReport(string[] function_param)
        {
            DateTime start = Convert.ToDateTime("01.01.0001");
            DateTime end = Convert.ToDateTime("01.01.3001");

            try
            {
                start = Convert.ToDateTime(function_param[0]);
                end = Convert.ToDateTime(function_param[1]);
            }
            catch (Exception)
            {


            }


            AccountReportVM accountReport = new AccountReportVM();
            CargosService cargosService = new CargosService();
            List<CargosVM> cargosVMList = cargosService.GetAll().Where(x => x.Date >= start && x.Date <= end).ToList();



            int totalQuantity = 0;
            double totalCargoDollar = 0;
            double totalDollar = 0;



            foreach (CargosVM cargos in cargosVMList)
            {
                if (cargos.Quantity != null)
                {
                    totalQuantity = totalQuantity + (int)cargos.Quantity;
                }
                if (cargos.TotalCargo != null)
                {
                    totalCargoDollar = totalCargoDollar + (double)cargos.TotalCargo;

                }
                if (cargos.TotalDollar != null)
                {
                    totalDollar = totalDollar + (double)cargos.TotalDollar;
                }


            }
            //accountReport.TotalCargo = accountingProductsServices.GetAll().Where(x => x.AccountingID == Convert.ToInt32(function_param[0])).ToList();

            accountReport.TotalCargo = totalCargoDollar;
           
            accountReport.TotalQuantity = totalQuantity;



            return View(accountReport);
        }
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult CostsReport(string categoryid)
        {


            TotalCargoService totalCargoService = new TotalCargoService();

            CostsReportVM costReport = new CostsReportVM();
            List<TotalCargoVM> costList = totalCargoService.GetAll().Where(x => x.Category.Trim() == categoryid.Trim()).ToList();



            double totalOrderTotal = 0;
            double totalExpensive = 0;
           

            foreach (TotalCargoVM cost in costList)
            {
                if (cost.OrderTotal != null)
                {
                    totalOrderTotal = totalOrderTotal + (double)cost.OrderTotal;
                }
                if (cost.Expensive != null)
                {
                    totalExpensive = totalExpensive + (double)cost.Expensive;
                }


            }
            //accountReport.TotalCargo = accountingProductsServices.GetAll().Where(x => x.AccountingID == Convert.ToInt32(function_param[0])).ToList();

            costReport.TotalExpensive = totalExpensive;
            costReport.TotalOrderTotal = totalOrderTotal;
            costReport.Category = categoryid;


            return View(costReport);
        }


        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult AllCostsReport(string categoryid)
        {


            TotalCargoService totalCargoService = new TotalCargoService();

            CostsReportVM costReport = new CostsReportVM();
            List<TotalCargoVM> costList = totalCargoService.GetAll().ToList();



            double OrderTotalTurkey = 0;
            double OrderTotalChina = 0;
            double OrderTotalUSA = 0;
            double OrderTotalPost = 0;
            double OrderTotalCargo = 0;

            double OrderTotalTicket = 0;
            double OrderTotalExpensive = 0;
            double OrderTotalExchange = 0;





            foreach (TotalCargoVM cost in costList)
            {
                if (cost.OrderTotal !=null)
                {
                    if (cost.Category.Trim() == "Cost Online Turkey")
                    {
                        OrderTotalTurkey = OrderTotalTurkey + (double)cost.OrderTotal;
                    }
                    if (cost.Category.Trim() == "Cost Online China")
                    {
                        OrderTotalChina = OrderTotalChina + (double)cost.OrderTotal;
                    }
                    if (cost.Category.Trim() == "Cost Online USA")
                    {
                        OrderTotalUSA = OrderTotalUSA + (double)cost.OrderTotal;
                    }
                    if (cost.Category.Trim() == "Cost Post")
                    {
                        OrderTotalPost = OrderTotalPost + (double)cost.OrderTotal;
                    }
                    if (cost.Category.Trim() == "Cost ZuuCargo")
                    {
                        OrderTotalCargo = OrderTotalCargo + (double)cost.OrderTotal;
                    }
                    if (cost.Category.Trim() == "Ticket & visa")
                    {
                        OrderTotalTicket = OrderTotalTicket + (double)cost.OrderTotal;
                    }
                    if (cost.Category.Trim() == "Expensive")
                    {
                        OrderTotalExpensive = OrderTotalExpensive + (double)cost.OrderTotal;
                    }
                    if (cost.Category.Trim() == "Exchange")
                    {
                        OrderTotalExchange = OrderTotalExchange + (double)cost.OrderTotal;
                    }
                }

            }
            //accountReport.TotalCargo = accountingProductsServices.GetAll().Where(x => x.AccountingID == Convert.ToInt32(function_param[0])).ToList();

            costReport.OrderTotalTurkey = OrderTotalTurkey;
            costReport.OrderTotalChina = OrderTotalChina;
            costReport.OrderTotalUSA = OrderTotalUSA;
            costReport.OrderTotalPost = OrderTotalPost;

            costReport.OrderTotalCargo = OrderTotalCargo;
            costReport.OrderTotalTicket = OrderTotalTicket;
            costReport.OrderTotalExpensive = OrderTotalExpensive;
            costReport.OrderTotalExchange = OrderTotalExchange;

            return View(costReport);
        }

        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult KamuBorcReport()
        {

            KamuBorcServices kamuBorcServices = new KamuBorcServices();

            AccountReportVM kamuBorcReport = new AccountReportVM();
            List<KamuBorcVM> kamuBorcList = kamuBorcServices.GetAll().ToList();

            double kamuBorcTurkey = 0;
            double kamuBorcUK = 0;
            double kamuBorcPost = 0;
            double kamuBorcCargo = 0;

            foreach (KamuBorcVM kamuBorc in kamuBorcList)
            {
                if (kamuBorc.TotalDollar != null)
                {
                    if (kamuBorc.Category.Trim() == "Kamu Borc Turkey")
                    {
                        kamuBorcTurkey = kamuBorcTurkey + (double)kamuBorc.TotalDollar;
                    }
                    if (kamuBorc.Category.Trim() == "Kamu Borc UK")
                    {
                        kamuBorcUK = kamuBorcUK + (double)kamuBorc.TotalDollar;
                    }
                    if (kamuBorc.Category.Trim() == "Post")
                    {
                        kamuBorcPost = kamuBorcPost + (double)kamuBorc.TotalDollar;
                    }
                    if (kamuBorc.Category.Trim() == "ZuuCargo")
                    {
                        kamuBorcCargo = kamuBorcCargo + (double)kamuBorc.TotalDollar;
                    }
                  
                 
                }

            }
            //accountReport.TotalCargo = accountingProductsServices.GetAll().Where(x => x.AccountingID == Convert.ToInt32(function_param[0])).ToList();

            kamuBorcReport.KamuBorcTurkey = kamuBorcTurkey;
            kamuBorcReport.KamuBorcUK = kamuBorcUK;
            kamuBorcReport.KamuBorcPost = kamuBorcPost;
            kamuBorcReport.KamuBorcCargo = kamuBorcCargo;


            return View(kamuBorcReport);
        }

        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult GetCargoReportByCustomer(string[] function_param)
        {
            DateTime start = Convert.ToDateTime("01.01.0001");
            DateTime end = Convert.ToDateTime("01.01.3001");

            try
            {
                start = Convert.ToDateTime(function_param[1]);
                end = Convert.ToDateTime(function_param[2]);
            }
            catch (Exception)
            {


            }
            string customerName = accountingServices.GetById(Convert.ToInt32(function_param[0])).CustomerName;


            AccountReportVM accountReport = new AccountReportVM();
            List<AccountingVM> accountingVM = accountingServices.GetAll().Where(x => x.CustomerName == customerName && x.Date >= start && x.Date <= end && x.IsCargo == true).ToList();



            double totalAccountDollar = 0;
            double totalAccountTL = 0;
            int totalQuantity = 0;
            double totalCargo = 0;
            double totalExpensive = 0;
            double totalExchange = 0;

            foreach (AccountingVM account in accountingVM)
            {
                if (account.TotalDollar != null)
                {
                    totalAccountDollar = totalAccountDollar + (double)account.TotalDollar;
                }
                if (account.TotalLira != null)
                {
                    totalAccountTL = totalAccountTL + (double)account.TotalLira;

                }
                if (account.CargoPrice != null)
                {
                    totalCargo = totalCargo + (double)account.CargoPrice;

                }
                if (account.Exchange != null)
                {
                    totalExchange = totalExchange + (double)account.Exchange;

                }
                if (account.Expensive != null)
                {
                    totalExpensive = totalExpensive + (double)account.Expensive;

                }
                totalQuantity = totalQuantity + (int)account.Quantity;

            }
            //accountReport.TotalCargo = accountingProductsServices.GetAll().Where(x => x.AccountingID == Convert.ToInt32(function_param[0])).ToList();

            accountReport.TotalCargo = totalCargo;
            accountReport.TotalSalesDollar = totalAccountDollar;
            accountReport.TotalSalesTL = totalAccountTL;
            accountReport.TotalExchange = totalExchange;
            accountReport.TotalExpensive = totalExpensive;
            accountReport.TotalQuantity = totalQuantity;
            accountReport.CustomerName = accountingServices.GetById(Convert.ToInt32(function_param[0])).CustomerName;



            return View(accountReport);
        }

        public JsonResult  MakeDelivered(int Id)
        {
            var success = false;
            AccountingVM accounting = new AccountingVM();
            try
            {
                accounting = accountingServices.GetById(Id);

                accounting.IsDelivered = true;
                accountingServices.Update(accounting);
                 success = true;


            }
            catch (Exception)
            {

                 success = false;

            }
            return Json(success, JsonRequestBehavior.AllowGet);
        }

    }
}
