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
    public class CargosController : BaseController
    {
        CargosService CargosServices = new CargosService();
        [CustomAuthorizeAttribute(Roles = "Admin, Cargo")]

        public ActionResult GetCargos()
        {

            var cargos = CargosServices.GetAll().OrderBy(a => a.CustomerName).ToList();
            return Json(new { data = cargos }, JsonRequestBehavior.AllowGet);

        }
        // GET: Admin/Cargos
        [CustomAuthorizeAttribute(Roles = "Admin, Cargo")]
        public ActionResult LoadData()
        {

            //Creating instance of DatabaseContext class  

            IEnumerable<CargosVM> CargosList = CargosServices.GetAll();

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
            var customerData = CargosList;

            ////Sorting    
            //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            //{
            //    customerData = customerData.OrderBy<CargosVM>(sortColumn + " " + sortColumnDir);
            //}
            //Search    
            if (!string.IsNullOrEmpty(searchValue))
            {
                customerData = customerData.Where(m => m.CustomerName == searchValue).ToList();
            }

            //total number of rows count     
            recordsTotal = customerData.Count();
            //Paging     
            var data = customerData.Skip(skip).Take(pageSize).ToList();
            //Returning Json Data    
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });




        }
        // GET: Cargos
        [CustomAuthorizeAttribute(Roles = "Admin, Cargo")]
        public ActionResult Index()
        {
            CargosService CargosServices = new CargosService();

            List<CargosVM> CargosLists = new List<CargosVM>();
            CargosLists = CargosServices.GetAll().ToList();



            IEnumerable<CargosVM> CargosList = CargosServices.GetAll();

            return View(CargosList);
        }
        [CustomAuthorizeAttribute(Roles = "Admin, Cargo")]
        // GET: Cargos/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }
        [CustomAuthorizeAttribute(Roles = "Admin, Cargo")]
        // GET: Cargos/Create
        public ActionResult Create()
        {
            CargosVM CargosVM = new CargosVM();
            return View(CargosVM);
        }

        // POST: Cargos/Create
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Cargo")]
        public ActionResult Create(CargosVM CargosVM)
        {
            try
            {
                // TODO: Add insert logic here
                CargosService CargosServices = new CargosService();
                CargosServices.Insert(CargosVM);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        [CustomAuthorizeAttribute(Roles = "Admin, Cargo")]
        // GET: Cargos/Edit/5
        public ActionResult Edit(int id)
        {

            CargosVM CargosVM = new CargosVM();
            CargosVM = CargosServices.GetById(id);

            return View(CargosVM);
        }

        // POST: Cargos/Edit/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Cargo")]
        public ActionResult Edit(int id, CargosVM CargosMVM)
        {

            CargosServices.Update(CargosMVM);
            return RedirectToAction("Index");


        }


        [CustomAuthorizeAttribute(Roles = "Admin, Cargo")]
        // GET: Cargos/Delete/5
        public ActionResult Delete(int id)
        {
            CargosService CargosServices = new CargosService();
            CargosVM Cargos = CargosServices.GetById(id);


            return View(Cargos);
        }

        // POST: Cargos/Delete/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Cargo")]
        public ActionResult Delete(int id, CargosVM Cargos)
        {

            CargosService CargosService = new CargosService();
            CargosService.Delete(Cargos);


            return RedirectToAction("Index");

        }


        [AllowAnonymous]
        public ActionResult PrintInvoice(int Id)
        {

            CargosVM cargo = new CargosVM();
            cargo = CargosServices.GetById(Id);

            return View(cargo);
        }

        public ActionResult Pdf(int id)
        {
            string url = "http://www.ZuuCargo.com/Admin/Cargos/PrintInvoice/" + id;
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


        [CustomAuthorizeAttribute(Roles = "Admin, Cargo")]

        public ActionResult SendInvoiceSMS(int id)
        {
            CargosVM cargoVM = new CargosVM();
            CargosService accountingServices = new CargosService();
            cargoVM = accountingServices.GetById(id);
            string phoneNumber = cargoVM.Telephone;
            //SEND SMS
            string html = string.Empty;


            string mesajMetni = "Thank you for choosing with ZuuCargo. You can download your invoice(" + cargoVM.Id + ") with this link http://www.ZuuCargo.com/Admin/Cargos/pdf/" + id;


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

    }
}