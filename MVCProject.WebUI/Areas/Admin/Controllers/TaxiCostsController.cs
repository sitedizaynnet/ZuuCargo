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
    public class TaxiCostsController : BaseController
    {
        TaxiCostsServices TaxiCostsServices = new TaxiCostsServices();

        // GET: Admin/TaxiCosts
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult LoadData()
        {

            //Creating instance of DatabaseContext class  

            IEnumerable<TaxiCostsVM> TaxiCostsList = TaxiCostsServices.GetAll();

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
            var customerData = TaxiCostsList;

            ////Sorting    
            //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            //{
            //    customerData = customerData.OrderBy<TaxiCostsVM>(sortColumn + " " + sortColumnDir);
            //}
            //Search    
            if (!string.IsNullOrEmpty(searchValue))
            {
                customerData = customerData.Where(m => m.Name.ToString() == searchValue).ToList();
            }

            //total number of rows count     
            recordsTotal = customerData.Count();
            //Paging     
            var data = customerData.Skip(skip).Take(pageSize).ToList();
            //Returning Json Data    
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });




        }
        // GET: TaxiCosts
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult Index()
        { 
            List<TaxiCostsVM> TaxiCostsLists = new List<TaxiCostsVM>();

            IEnumerable<TaxiCostsVM> TaxiCostsList = TaxiCostsServices.GetAll();

            return View(TaxiCostsList);
        }
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        // GET: TaxiCosts/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        // GET: TaxiCosts/Create
        public ActionResult Create()
        {
            TaxiCostsVM TaxiCostsVM = new TaxiCostsVM();
            TaxiCostsVM.Date = DateTime.Now;
            return View(TaxiCostsVM);
        }

        // POST: TaxiCosts/Create
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult Create(TaxiCostsVM TaxiCostsVM)
        {
            //try
            //{
                // TODO: Add insert logic here
                TaxiCostsServices TaxiCostsServices = new TaxiCostsServices();
                TaxiCostsServices.Insert(TaxiCostsVM);
                return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
        }


        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        // GET: TaxiCosts/Edit/5
        public ActionResult Edit(int id)
        {

            TaxiCostsVM TaxiCostsVM = new TaxiCostsVM();
            TaxiCostsVM = TaxiCostsServices.GetById(id);

            return View(TaxiCostsVM);
        }

        // POST: TaxiCosts/Edit/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult Edit(int id, TaxiCostsVM TaxiCostsMVM)
        {

            TaxiCostsServices.Update(TaxiCostsMVM);
            return RedirectToAction("Index");


        }


        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        // GET: TaxiCosts/Delete/5
        public ActionResult Delete(int id)
        {
            TaxiCostsServices TaxiCostsServices = new TaxiCostsServices();
            TaxiCostsVM TaxiCosts = TaxiCostsServices.GetById(id);


            return View(TaxiCosts);
        }

        // POST: TaxiCosts/Delete/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult Delete(int id, TaxiCostsVM TaxiCosts)
        {

            TaxiCostsServices TaxiCostsServices = new TaxiCostsServices();
            TaxiCostsServices.Delete(TaxiCosts);


            return RedirectToAction("Index");

        }
    }
}