using MVCProject.BLL.Services;
using MVCProject.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static MVCProject.WebUI.Controllers.BaseController;

namespace MVCProject.WebUI.Areas.Admin.Controllers
{
    public class ReklamController : BaseController
    {
        ReklamServices ReklamServices = new ReklamServices();

        // GET: Admin/Reklam
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult LoadData()
        {

            //Creating instance of DatabaseContext class  

            IEnumerable<ReklamVM> ReklamList = ReklamServices.GetAll();

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
            var customerData = ReklamList;

            ////Sorting    
            //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            //{
            //    customerData = customerData.OrderBy<ReklamVM>(sortColumn + " " + sortColumnDir);
            //}
            //Search    
            if (!string.IsNullOrEmpty(searchValue))
            {
                customerData = customerData.Where(m => m.Id.ToString() == searchValue).ToList();
            }

            //total number of rows count     
            recordsTotal = customerData.Count();
            //Paging     
            var data = customerData.Skip(skip).Take(pageSize).ToList();
            //Returning Json Data    
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });




        }


        [AllowAnonymous]
        public ActionResult PrintInvoice(int Id)
        {
            ReklamServices ReklamServices = new ReklamServices();
            ReklamVM accounting = new ReklamVM();
            accounting = ReklamServices.GetById(Id);

            return View(accounting);
        }



        // GET: Reklam
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult Index()
        { 
            List<ReklamVM> ReklamLists = new List<ReklamVM>();

            IEnumerable<ReklamVM> ReklamList = ReklamServices.GetAll();

            return View(ReklamList);
        }
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        // GET: Reklam/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        // GET: Reklam/Create
        public ActionResult Create()
        {
            ReklamVM ReklamVM = new ReklamVM();
         
            return View(ReklamVM);
        }

        [CustomAuthorizeAttribute(Roles = "Admin")]
        public JsonResult UploadImage()
        {
            string fileName = "";
            if (Request.Files.Count > 0)
            {

                HttpPostedFileBase file = Request.Files[0]; //Uploaded file
                                                            //Use the following properties to get file's name, size and MIMEType
                int fileSize = file.ContentLength;
                fileName = file.FileName;
                string mimeType = file.ContentType;
                System.IO.Stream fileContent = file.InputStream;


                //To save file, use SaveAs method
                file.SaveAs(Server.MapPath("~/Content/Uploads/") + fileName); //File will be saved in application root
                ReklamServices reklamServices = new ReklamServices();
                ReklamVM reklamVM = reklamServices.GetById(1);
                reklamVM.Image = "https://zuucargo.com/Content/Uploads/" + fileName;
                reklamServices.Update(reklamVM);
            }
            return Json("https://zuucargo.com/Content/Uploads/" + fileName, JsonRequestBehavior.AllowGet);
        }

    


    // POST: Reklam/Create
    [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        [ValidateInput(false)]
        public ActionResult Create(ReklamVM ReklamVM)
        {
            //try
            //{
                // TODO: Add insert logic here
                ReklamServices ReklamServices = new ReklamServices();
                ReklamServices.Insert(ReklamVM);
                return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
        }


        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        // GET: Reklam/Edit/5
        public ActionResult Edit(int id)
        {

            ReklamVM ReklamVM = new ReklamVM();
            ReklamVM = ReklamServices.GetById(id);

            return View(ReklamVM);
        }

        // POST: Reklam/Edit/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult Edit(int id, ReklamVM ReklamMVM)
        {

            ReklamServices.Update(ReklamMVM);
            return RedirectToAction("Index");


        }


        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        // GET: Reklam/Delete/5
        public ActionResult Delete(int id)
        {
            ReklamServices ReklamServices = new ReklamServices();
            ReklamVM Reklam = ReklamServices.GetById(id);


            return View(Reklam);
        }

        // POST: Reklam/Delete/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult Delete(int id, ReklamVM Reklam)
        {

            ReklamServices ReklamServices = new ReklamServices();
            ReklamServices.Delete(Reklam);


            return RedirectToAction("Index");

        }
    }
}