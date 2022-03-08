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
    public class TurkishCargoController : BaseController
    {
        TurkishCargoServices TurkishCargoServices = new TurkishCargoServices();

        // GET: Admin/TurkishCargo
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult LoadData()
        {

            //Creating instance of DatabaseContext class  

            IEnumerable<TurkishCargoVM> TurkishCargoList = TurkishCargoServices.GetAll();

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
            var customerData = TurkishCargoList;

            ////Sorting    
            //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            //{
            //    customerData = customerData.OrderBy<TurkishCargoVM>(sortColumn + " " + sortColumnDir);
            //}
            //Search    
            if (!string.IsNullOrEmpty(searchValue))
            {
                customerData = customerData.Where(m => m.AirBill.ToString() == searchValue).ToList();
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
            TurkishCargoServices TurkishCargoServices = new TurkishCargoServices();
            TurkishCargoVM accounting = new TurkishCargoVM();
            accounting = TurkishCargoServices.GetById(Id);

            return View(accounting);
        }



        // GET: TurkishCargo
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult Index()
        {
            List<TurkishCargoVM> TurkishCargoLists = new List<TurkishCargoVM>();

            IEnumerable<TurkishCargoVM> TurkishCargoList = TurkishCargoServices.GetAll();

            return View(TurkishCargoList);
        }
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        // GET: TurkishCargo/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        // GET: TurkishCargo/Create
        public ActionResult Create()
        {
            TurkishCargoVM TurkishCargoVM = new TurkishCargoVM();

            return View(TurkishCargoVM);
        }

        // POST: TurkishCargo/Create
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult Create(TurkishCargoVM TurkishCargoVM)
        {
            //try
            //{
            // TODO: Add insert logic here
            TurkishCargoServices TurkishCargoServices = new TurkishCargoServices();
            TurkishCargoServices.Insert(TurkishCargoVM);
            return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
        }


        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        // GET: TurkishCargo/Edit/5
        public ActionResult Edit(int id)
        {

            TurkishCargoVM TurkishCargoVM = new TurkishCargoVM();
            TurkishCargoVM = TurkishCargoServices.GetById(id);

            return View(TurkishCargoVM);
        }

        // POST: TurkishCargo/Edit/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult Edit(int id, TurkishCargoVM TurkishCargoMVM)
        {

            TurkishCargoServices.Update(TurkishCargoMVM);
            return RedirectToAction("Index");


        }


        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        // GET: TurkishCargo/Delete/5
        public ActionResult Delete(int id)
        {
            TurkishCargoServices TurkishCargoServices = new TurkishCargoServices();
            TurkishCargoVM TurkishCargo = TurkishCargoServices.GetById(id);


            return View(TurkishCargo);
        }

        // POST: TurkishCargo/Delete/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult Delete(int id, TurkishCargoVM TurkishCargo)
        {

            TurkishCargoServices TurkishCargoServices = new TurkishCargoServices();
            TurkishCargoServices.Delete(TurkishCargo);


            return RedirectToAction("Index");

        }
    }
}