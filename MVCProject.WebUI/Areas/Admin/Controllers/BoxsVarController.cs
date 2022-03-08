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
    public class BoxsVarController : BaseController
    {
        BoxsVarService BoxsVarServices = new BoxsVarService();



        [AllowAnonymous]
        public ActionResult PrintInvoice(int Id)
        {

            BoxsVarVM cost = new BoxsVarVM();
            cost = BoxsVarServices.GetById(Id);

            return View(cost);
        }
        [CustomAuthorizeAttribute(Roles = "Admin, BoxsVar")]
        public ActionResult SendInvoiceSMS(int id)
        {
            BoxsVarVM cost = new BoxsVarVM();

            cost = BoxsVarServices.GetById(id);
            string phoneNumber = cost.Telephone;
            //SEND SMS
            string html = string.Empty;


            string mesajMetni = "Thank you for choosing with VAR-Shopping. You can download your invoice(" + cost.Id + ") with this link http://www.ZuuCargo.com/Admin/BoxsVar/pdf/" + id;


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
            string url = "http://www.ZuuCargo.com/Admin/BoxsVar/PrintInvoice/" + id;
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



        // GET: Admin/BoxsVar
        [CustomAuthorizeAttribute(Roles = "Admin, BoxsVar")]
        public ActionResult LoadData()
        {

            //Creating instance of DatabaseContext class  

            IEnumerable<BoxsVarVM> BoxsVarList = BoxsVarServices.GetAll();

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
            var customerData = BoxsVarList;

            ////Sorting    
            //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            //{
            //    customerData = customerData.OrderBy<BoxsVarVM>(sortColumn + " " + sortColumnDir);
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
        // GET: BoxsVar
        [CustomAuthorizeAttribute(Roles = "Admin, BoxsVar")]
        public ActionResult Index()
        {
            BoxsVarService BoxsVarServices = new BoxsVarService();
           
            List<BoxsVarVM> BoxsVarLists = new List<BoxsVarVM>();
            BoxsVarLists = BoxsVarServices.GetAll().ToList();



            IEnumerable<BoxsVarVM> BoxsVarList = BoxsVarServices.GetAll();

            return View(BoxsVarList);
        }
        [CustomAuthorizeAttribute(Roles = "Admin, BoxsVar")]
        // GET: BoxsVar/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }
        [CustomAuthorizeAttribute(Roles = "Admin, BoxsVar")]
        // GET: BoxsVar/Create
        public ActionResult Create()
        {
            BoxsVarVM BoxsVarVM = new BoxsVarVM();
            return View(BoxsVarVM);
        }

        // POST: BoxsVar/Create
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, BoxsVar")]
        public ActionResult Create(BoxsVarVM BoxsVarVM)
        {
            try
            {
                // TODO: Add insert logic here
                BoxsVarService BoxsVarServices = new BoxsVarService();
                BoxsVarServices.Insert(BoxsVarVM);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

      
        [CustomAuthorizeAttribute(Roles = "Admin, BoxsVar")]
        // GET: BoxsVar/Edit/5
        public ActionResult Edit(int id)
        {

            BoxsVarVM BoxsVarVM = new BoxsVarVM();
            BoxsVarVM = BoxsVarServices.GetById(id);
          
            return View(BoxsVarVM);
        }

        // POST: BoxsVar/Edit/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, BoxsVar")]
        public ActionResult Edit(int id, BoxsVarVM BoxsVarMVM )
        {

            BoxsVarServices.Update(BoxsVarMVM);
            return RedirectToAction("Index");


        }


        [CustomAuthorizeAttribute(Roles = "Admin, BoxsVar")]
        // GET: BoxsVar/Delete/5
        public ActionResult Delete(int id)
        {
            BoxsVarService BoxsVarServices = new BoxsVarService();
            BoxsVarVM BoxsVar = BoxsVarServices.GetById(id);


            return View(BoxsVar);
        }

        // POST: BoxsVar/Delete/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, BoxsVar")]
        public ActionResult Delete(int id, BoxsVarVM BoxsVar)
        {

            BoxsVarService BoxsVarService = new BoxsVarService();
            BoxsVarService.Delete(BoxsVar);
             
           
                return RedirectToAction("Index");
           
        }
    }
}