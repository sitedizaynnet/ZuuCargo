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
    public class KamuBorcController : BaseController
    {
        KamuBorcServices KamuBorcServices = new KamuBorcServices();



        [AllowAnonymous]
        public ActionResult PrintInvoice(int Id)
        {

            KamuBorcVM cost = new KamuBorcVM();
            cost = KamuBorcServices.GetById(Id);

            return View(cost);
        }
        public ActionResult SendInvoiceSMS(int id)
        {
            KamuBorcVM cost = new KamuBorcVM();

            cost = KamuBorcServices.GetById(id);
            string phoneNumber = cost.Telephone;
            //SEND SMS
            string html = string.Empty;


            string mesajMetni = "Thank you for choosing with VAR-Shopping. You can download your invoice(" + cost.Id + ") with this link http://www.ZuuCargo.com/Admin/KamuBorc/pdf/" + id;


            string telNo = phoneNumber;

            string url = @"https://gw.cmtelecom.com/gateway.ashx?producttoken=6A10F3EB-0AE0-4114-BD13-902EB0C57A58&body=" + mesajMetni + "&to=" + telNo.Trim() + "&from=ZuuCargo&reference=" + id + phoneNumber;
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
        public ActionResult Pdf(int id)
        {
            string url = "http://www.ZuuCargo.com/Admin/KamuBorc/PrintInvoice/" + id;
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



        // GET: Admin/KamuBorc
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult LoadData()
        {

            //Creating instance of DatabaseContext class  

            IEnumerable<KamuBorcVM> KamuBorcList = KamuBorcServices.GetAll();

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
            var customerData = KamuBorcList;

            ////Sorting    
            //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            //{
            //    customerData = customerData.OrderBy<KamuBorcVM>(sortColumn + " " + sortColumnDir);
            //}
            //Search    
            if (!string.IsNullOrEmpty(searchValue))
            {
                customerData = customerData.Where(m => m.Name == searchValue).ToList();
            }

            //total number of rows count     
            recordsTotal = customerData.Count();
            //Paging     
            var data = customerData.Skip(skip).Take(pageSize).ToList();
            //Returning Json Data    
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });




        }
        // GET: KamuBorc
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult Index()
        {
            KamuBorcServices KamuBorcServices = new KamuBorcServices();
           
            List<KamuBorcVM> KamuBorcLists = new List<KamuBorcVM>();
            KamuBorcLists = KamuBorcServices.GetAll().ToList();



            IEnumerable<KamuBorcVM> KamuBorcList = KamuBorcServices.GetAll();

            return View(KamuBorcList);
        }
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        // GET: KamuBorc/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        // GET: KamuBorc/Create
        public ActionResult Create()
        {
            KamuBorcVM kamuBorcVM = new KamuBorcVM();
            kamuBorcVM.Date = DateTime.Now;
                Random rnd = new Random();
                Random r = new Random();
                int rInt = r.Next(50000, 100000); //for ints

            kamuBorcVM.InvoiceNumber = rInt;
          
          
            return View(kamuBorcVM);
        }

        // POST: KamuBorc/Create
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult Create(KamuBorcVM KamuBorcVM)
        {
            try
            {
                // TODO: Add insert logic here
              
                KamuBorcServices.Insert(KamuBorcVM);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

      
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        // GET: KamuBorc/Edit/5
        public ActionResult Edit(int id)
        {

            KamuBorcVM KamuBorcVM = new KamuBorcVM();
            KamuBorcVM = KamuBorcServices.GetById(id);
          
            return View(KamuBorcVM);
        }

        // POST: KamuBorc/Edit/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult Edit(int id, KamuBorcVM KamuBorcMVM )
        {

            KamuBorcServices.Update(KamuBorcMVM);
            return RedirectToAction("Index");


        }


        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        // GET: KamuBorc/Delete/5
        public ActionResult Delete(int id)
        {
         
            KamuBorcVM KamuBorc = KamuBorcServices.GetById(id);


            return View(KamuBorc);
        }

        // POST: KamuBorc/Delete/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult Delete(int id, KamuBorcVM KamuBorc)
        {

           
            KamuBorcServices.Delete(KamuBorc);
             
           
                return RedirectToAction("Index");
           
        }
    }
}