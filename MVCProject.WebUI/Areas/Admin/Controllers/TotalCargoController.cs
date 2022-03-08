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
    public class TotalCargoController : BaseController
    {
       TotalCargoService totalCargoServices = new TotalCargoService();



        [AllowAnonymous]
        public ActionResult PrintInvoice(int Id)
        {

            TotalCargoVM cost = new TotalCargoVM();
            cost = totalCargoServices.GetById(Id);

            return View(cost);
        }
        [CustomAuthorizeAttribute(Roles = "Admin, Cost")]
        public ActionResult SendInvoiceSMS(int id)
        {
            TotalCargoVM cost = new TotalCargoVM();

            cost = totalCargoServices.GetById(id);
            string phoneNumber = cost.Telephone;
            //SEND SMS
            string html = string.Empty;


            string mesajMetni = "Thank you for choosing with VAR-Shopping. You can download your invoice(" + cost.Id + ") with this link http://www.ZuuCargo.com/Admin/Costs/pdf/" + id;


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
            string url = "http://www.ZuuCargo.com/Admin/Costs/PrintInvoice/" + id;
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
        [CustomAuthorizeAttribute(Roles = "Admin, Cost")]

        public ActionResult GetTotalCargo()
        {

            var employees = totalCargoServices.GetAll().OrderBy(a => a.Date).ToList();
            return Json(new { data = employees }, JsonRequestBehavior.AllowGet);

        }

        // GET: Admin/Costs
        [CustomAuthorizeAttribute(Roles = "Admin, Cost")]
        public ActionResult LoadData()
        {

            //Creating instance of DatabaseContext class  

            IEnumerable<TotalCargoVM> CostsList = totalCargoServices.GetAll();

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
            var customerData = CostsList;

            ////Sorting    
            //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            //{
            //    customerData = customerData.OrderBy<CostsVM>(sortColumn + " " + sortColumnDir);
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
        // GET: Costs
        [CustomAuthorizeAttribute(Roles = "Admin, Cost")]
        public ActionResult Index()
        {
            TotalCargoService totalCargoServices = new TotalCargoService();
           
            List<TotalCargoVM> CostsLists = new List<TotalCargoVM>();
            CostsLists = totalCargoServices.GetAll().ToList();



            IEnumerable<TotalCargoVM> CostsList = totalCargoServices.GetAll();

            return View(CostsList);
        }
        [CustomAuthorizeAttribute(Roles = "Admin, Cost")]
        // GET: Costs/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }
        [CustomAuthorizeAttribute(Roles = "Admin, Cost")]
        // GET: Costs/Create
        public ActionResult Create()
        {
            TotalCargoVM CostsVM = new TotalCargoVM();
            return View(CostsVM);
        }

        // POST: Costs/Create
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Cost")]
        public ActionResult Create(TotalCargoVM CostsVM)
        {
            try
            {
                // TODO: Add insert logic here
                TotalCargoService totalCargoServices = new TotalCargoService();
                totalCargoServices.Insert(CostsVM);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

      
        [CustomAuthorizeAttribute(Roles = "Admin, Cost")]
        // GET: Costs/Edit/5
        public ActionResult Edit(int id)
        {

            TotalCargoVM CostsVM = new TotalCargoVM();
            CostsVM = totalCargoServices.GetById(id);
          
            return View(CostsVM);
        }

        // POST: Costs/Edit/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Cost")]
        public ActionResult Edit(int id, TotalCargoVM CostsMVM )
        {

            totalCargoServices.Update(CostsMVM);
            return RedirectToAction("Index");


        }


        [CustomAuthorizeAttribute(Roles = "Admin, Cost")]
        // GET: Costs/Delete/5
        public ActionResult Delete(int id)
        {
            TotalCargoService totalCargoServices = new TotalCargoService();
            TotalCargoVM Costs = totalCargoServices.GetById(id);


            return View(Costs);
        }

        // POST: Costs/Delete/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Cost")]
        public ActionResult Delete(int id, TotalCargoVM Costs)
        {

            TotalCargoService CostsService = new TotalCargoService();
            CostsService.Delete(Costs);
             
           
                return RedirectToAction("Index");
           
        }
    }
}