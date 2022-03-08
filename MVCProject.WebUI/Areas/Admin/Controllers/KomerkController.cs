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
    public class KomerkController : BaseController
    {
        KomerkServices KomerkServices = new KomerkServices();

        // GET: Admin/Komerk
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult LoadData()
        {

            //Creating instance of DatabaseContext class  

            IEnumerable<KomerkVM> KomerkList = KomerkServices.GetAll();

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
            var customerData = KomerkList;

            ////Sorting    
            //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            //{
            //    customerData = customerData.OrderBy<KomerkVM>(sortColumn + " " + sortColumnDir);
            //}
            //Search    
            if (!string.IsNullOrEmpty(searchValue))
            {
                customerData = customerData.Where(m => m.Price.ToString() == searchValue).ToList();
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
            KomerkServices KomerkServices = new KomerkServices();
            KomerkVM accounting = new KomerkVM();
            accounting = KomerkServices.GetById(Id);

            return View(accounting);
        }



        // GET: Komerk
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult Index()
        { 
            List<KomerkVM> KomerkLists = new List<KomerkVM>();

            IEnumerable<KomerkVM> KomerkList = KomerkServices.GetAll();

            return View(KomerkList);
        }
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        // GET: Komerk/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        // GET: Komerk/Create
        public ActionResult Create()
        {
            KomerkVM KomerkVM = new KomerkVM();
         
            return View(KomerkVM);
        }

        // POST: Komerk/Create
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult Create(KomerkVM KomerkVM)
        {
            //try
            //{
                // TODO: Add insert logic here
                KomerkServices KomerkServices = new KomerkServices();
                KomerkServices.Insert(KomerkVM);
                return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
        }


        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        // GET: Komerk/Edit/5
        public ActionResult Edit(int id)
        {

            KomerkVM KomerkVM = new KomerkVM();
            KomerkVM = KomerkServices.GetById(id);

            return View(KomerkVM);
        }

        // POST: Komerk/Edit/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult Edit(int id, KomerkVM KomerkMVM)
        {

            KomerkServices.Update(KomerkMVM);
            return RedirectToAction("Index");


        }


        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        // GET: Komerk/Delete/5
        public ActionResult Delete(int id)
        {
            KomerkServices KomerkServices = new KomerkServices();
            KomerkVM Komerk = KomerkServices.GetById(id);


            return View(Komerk);
        }

        // POST: Komerk/Delete/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult Delete(int id, KomerkVM Komerk)
        {

            KomerkServices KomerkServices = new KomerkServices();
            KomerkServices.Delete(Komerk);


            return RedirectToAction("Index");

        }
    }
}