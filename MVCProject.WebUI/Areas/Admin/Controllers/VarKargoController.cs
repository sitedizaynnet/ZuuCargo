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
    public class ZuuCargoController : BaseController
    {
        ZuuCargoServices ZuuCargoServices = new ZuuCargoServices();



        [AllowAnonymous]
        public ActionResult PrintInvoice(int Id)
        {

            ZuuCargoVM cost = new ZuuCargoVM();
            cost = ZuuCargoServices.GetById(Id);

            return View(cost);
        }
        [CustomAuthorizeAttribute(Roles = "Admin, ZuuCargo")]
        public ActionResult SendInvoiceSMS(int id)
        {
            ZuuCargoVM ZuuCargo = new ZuuCargoVM();

            ZuuCargo = ZuuCargoServices.GetById(id);
            
            UserServices userServices = new UserServices();
            ApplicationUser user = new ApplicationUser();
            user.Phone = userServices.GetByUserId(ZuuCargo.CustomerAccountId).Phone;
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
            string url = "http://www.ZuuCargo.com/Admin/ZuuCargo/PrintInvoice/" + id;
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



        // GET: Admin/ZuuCargo
        [CustomAuthorizeAttribute(Roles = "Admin, ZuuCargo")]
        public ActionResult LoadData()
        {

            //Creating instance of DatabaseContext class  

            IEnumerable<ZuuCargoVM> ZuuCargoList = ZuuCargoServices.GetAll();

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
            var customerData = ZuuCargoList;

            ////Sorting    
            //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            //{
            //    customerData = customerData.OrderBy<ZuuCargoVM>(sortColumn + " " + sortColumnDir);
            //}
            //Search    
            if (!string.IsNullOrEmpty(searchValue))
            {
                customerData = customerData.Where(m => m.CustomersName == searchValue).ToList();
            }

            //total number of rows count     
            recordsTotal = customerData.Count();
            //Paging     
            var data = customerData.Skip(skip).Take(pageSize).ToList();
            //Returning Json Data    
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

        }
        // GET: ZuuCargo
        [CustomAuthorizeAttribute(Roles = "Admin, ZuuCargo")]
        public ActionResult PrintKargo(int id)
        {
            ZuuCargoServices ZuuCargoServices = new ZuuCargoServices();
            ZuuCargoVM ZuuCargoVM = new ZuuCargoVM();
            ZuuCargoVM = ZuuCargoServices.GetById(id);
            return View(ZuuCargoVM);
        
        }
        // GET: ZuuCargo
        [CustomAuthorizeAttribute(Roles = "Admin, ZuuCargo")]
        public ActionResult Index()
        {
            ZuuCargoServices ZuuCargoServices = new ZuuCargoServices();
            UserServices userServices = new UserServices();
            List<ZuuCargoVM> ZuuCargoLists = new List<ZuuCargoVM>();
            ZuuCargoLists = ZuuCargoServices.GetAll().ToList();
           


            IEnumerable<ZuuCargoVM> ZuuCargoList = ZuuCargoServices.GetAll();

            return View(ZuuCargoList);
        }
        [CustomAuthorizeAttribute(Roles = "Admin, ZuuCargo")]
        // GET: ZuuCargo/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }
        [CustomAuthorizeAttribute(Roles = "Admin, ZuuCargo")]
        // GET: ZuuCargo/Create
        public ActionResult Create()
        {
            UserServices userServices = new UserServices();
            ViewData["UserList"] = userServices.GetAll().Where(x=>x.FirstName !=null).ToList();
           
            ZuuCargoVM ZuuCargoVM = new ZuuCargoVM();

        

            return View(ZuuCargoVM);
        }

        // POST: ZuuCargo/Create
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, ZuuCargo")]
        public ActionResult Create(ZuuCargoVM ZuuCargoVM)
        {
            UserServices userServices = new UserServices();
            ViewData["UserList"] = userServices.GetAll().Where(x => x.FirstName != null).ToList();
            try
            {
                // TODO: Add insert logic here
                ZuuCargoServices ZuuCargoServices = new ZuuCargoServices();
                ApplicationUser user = new ApplicationUser();
             
                user = userServices.GetByUserId(ZuuCargoVM.CustomerAccountId);
                ZuuCargoVM.CustomersName = user.FirstName + " " + user.LastName;
                ZuuCargoServices.Insert(ZuuCargoVM);
                var Db = new ZuuCargoEntities();
                var usertoUpdate = Db.Users.First(u => u.Id == ZuuCargoVM.CustomerAccountId.Trim());
                usertoUpdate.Balance = usertoUpdate.Balance - ZuuCargoVM.Price;
                Db.Entry(usertoUpdate).State = System.Data.Entity.EntityState.Modified;
                Db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

      
        [CustomAuthorizeAttribute(Roles = "Admin, ZuuCargo")]
        // GET: ZuuCargo/Edit/5
        public ActionResult Edit(int id)
        {

            UserServices userServices = new UserServices();
            ViewData["UserList"] = userServices.GetAll().Where(x => x.FirstName != null).ToList();
            ZuuCargoVM ZuuCargoVM = new ZuuCargoVM();
            ZuuCargoVM = ZuuCargoServices.GetById(id);
          
            return View(ZuuCargoVM);
        }

        // POST: ZuuCargo/Edit/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, ZuuCargo")]
        public ActionResult Edit(int id, ZuuCargoVM ZuuCargoMVM )
        {
            UserServices userServices = new UserServices();
            ViewData["UserList"] = userServices.GetAll().Where(x => x.FirstName != null).ToList();
            ZuuCargoServices.Update(ZuuCargoMVM);
            return RedirectToAction("Index");


        }


        [CustomAuthorizeAttribute(Roles = "Admin, ZuuCargo")]
        // GET: ZuuCargo/Delete/5
        public ActionResult Delete(int id)
        {
            ZuuCargoServices ZuuCargoServices = new ZuuCargoServices();
            ZuuCargoVM ZuuCargo = ZuuCargoServices.GetById(id);


            return View(ZuuCargo);
        }

        // POST: ZuuCargo/Delete/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, ZuuCargo")]
        public ActionResult Delete(int id, ZuuCargoVM ZuuCargo)
        {

            ZuuCargoServices ZuuCargoService = new ZuuCargoServices();
            ZuuCargoService.Delete(ZuuCargo);
             
           
                return RedirectToAction("Index");
           
        }
    }
}