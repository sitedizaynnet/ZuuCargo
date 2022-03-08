using MVCProject.BLL.Services;
using MVCProject.Common.ViewModels;
using MVCProject.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using static MVCProject.Entities.ZuuCargoEntities;
using static MVCProject.WebUI.Controllers.BaseController;

namespace MVCProject.WebUI.Areas.Admin.Controllers
{
    public class DepositController : BaseController
    {
        DepositServices DepositServices = new DepositServices();



        [AllowAnonymous]
        public ActionResult PrintInvoice(int Id)
        {

            DepositVM cost = new DepositVM();
            cost = DepositServices.GetById(Id);

            return View(cost);
        }
        [CustomAuthorizeAttribute(Roles = "Admin, Deposit")]
        public ActionResult SendInvoiceSMS(int id)
        {
            DepositVM Deposit = new DepositVM();

            Deposit = DepositServices.GetById(id);
            UserServices userServices = new UserServices();
            ApplicationUser user = new ApplicationUser();
            user.Phone = userServices.GetByUserId(Deposit.CustomerId).Phone;
            string phoneNumber = user.Phone;
          
            var charsToRemove = new string[] { "(", ")", "+", "-", " "};
            foreach (var c in charsToRemove)
            {
                phoneNumber = phoneNumber.Replace(c, string.Empty);
            }
            phoneNumber = "00" + phoneNumber;
            //SEND SMS
            string html = string.Empty;


            string mesajMetni = "A package has been delivered to your ZuuCargo suite and all items have been added to your inbox!";


            string telNo = phoneNumber;

            string url = @"https://gw.cmtelecom.com/gateway.ashx?producttoken=6A10F3EB-0AE0-4114-BD13-902EB0C57A58&body=" + mesajMetni + "&to=" + telNo.Trim() + "&from=Deposit&reference=" + id + phoneNumber;
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
            string url = "http://www.ZuuCargo.com/Admin/Deposit/PrintInvoice/" + id;
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



        // GET: Admin/Deposit
        [CustomAuthorizeAttribute(Roles = "Admin, Deposit")]
        public ActionResult LoadData()
        {

            //Creating instance of DatabaseContext class  

            IEnumerable<DepositVM> DepositList = DepositServices.GetAll();

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
            var customerData = DepositList;

            ////Sorting    
            //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            //{
            //    customerData = customerData.OrderBy<DepositVM>(sortColumn + " " + sortColumnDir);
            //}
            //Search    
            if (!string.IsNullOrEmpty(searchValue))
            {
                customerData = customerData.Where(m => m.CustomerId == searchValue).ToList();
            }

            //total number of rows count     
            recordsTotal = customerData.Count();
            //Paging     
            var data = customerData.Skip(skip).Take(pageSize).ToList();
            //Returning Json Data    
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

        }
        // GET: Deposit
        [CustomAuthorizeAttribute(Roles = "Admin, Deposit")]
        public ActionResult PrintKargo(int id)
        {
            DepositServices DepositServices = new DepositServices();
            DepositVM DepositVM = new DepositVM();
            DepositVM = DepositServices.GetById(id);
            return View(DepositVM);
        
        }
        // GET: Deposit
        [CustomAuthorizeAttribute(Roles = "Admin, Deposit")]
        public ActionResult Index()
        {
            DepositServices DepositServices = new DepositServices();
            UserServices userServices = new UserServices();
            List<DepositVM> DepositLists = new List<DepositVM>();
            DepositLists = DepositServices.GetAll().ToList();
            ViewBag.UsersList = userServices.GetAll().ToList();


            IEnumerable<DepositVM> DepositList = DepositServices.GetAll();

            return View(DepositList);
        }
        [CustomAuthorizeAttribute(Roles = "Admin, Deposit")]
        // GET: Deposit/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }
        [CustomAuthorizeAttribute(Roles = "Admin, Deposit")]
        // GET: Deposit/Create
        public ActionResult Create()
        {
            UserServices userServices = new UserServices();
            ViewData["UserList"] = userServices.GetAll().Where(x=>x.FirstName !=null).ToList();
           
            DepositVM DepositVM = new DepositVM();

        

            return View(DepositVM);
        }

        // POST: Deposit/Create
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Deposit")]
        public ActionResult Create(DepositVM DepositVM)
        {
           
            try
            {
                // TODO: Add insert logic here
                DepositServices DepositServices = new DepositServices();
                DepositServices.Insert(DepositVM);
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

      
        [CustomAuthorizeAttribute(Roles = "Admin, Deposit")]
        // GET: Deposit/Edit/5
        public ActionResult Edit(int id)
        {

            UserServices userServices = new UserServices();
            ViewData["UserList"] = userServices.GetAll().Where(x => x.FirstName != null).ToList();
            DepositVM depositVM = new DepositVM();
           depositVM = DepositServices.GetById(id);
          
            return View(depositVM);
        }

        // POST: Deposit/Edit/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Deposit")]
        public ActionResult Edit(int id, DepositVM DepositMVM )
        {
            UserServices userServices = new UserServices();
            ViewData["UserList"] = userServices.GetAll().Where(x => x.FirstName != null).ToList();
            DepositServices.Update(DepositMVM);
            return RedirectToAction("Index");


        }


        [CustomAuthorizeAttribute(Roles = "Admin, Deposit")]
        // GET: Deposit/Delete/5
        public ActionResult Delete(int id)
        {
            DepositServices DepositServices = new DepositServices();
            DepositVM Deposit = DepositServices.GetById(id);


            return View(Deposit);
        }

        // POST: Deposit/Delete/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Deposit")]
        public ActionResult Delete(int id, DepositVM Deposit)
        {

            DepositServices DepositService = new DepositServices();
            DepositService.Delete(Deposit);
             
           
                return RedirectToAction("Index");
           
        }
    }
}