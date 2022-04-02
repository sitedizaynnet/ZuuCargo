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
    public class RemainingCostController : BaseController
    {
        RemainingCostServices RemainingCostServicess = new RemainingCostServices();

        // GET: Admin/RemainingCost
        [CustomAuthorizeAttribute(Roles = "Admin")]
        public ActionResult LoadData()
        {

            //Creating instance of DatabaseContext class  

            IEnumerable<RemainingCostVM> RemainingCostList = RemainingCostServicess.GetAll();

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
            var customerData = RemainingCostList;

            ////Sorting    
            //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            //{
            //    customerData = customerData.OrderBy<RemainingCostVM>(sortColumn + " " + sortColumnDir);
            //}
            //Search    
            if (!string.IsNullOrEmpty(searchValue))
            {
                customerData = customerData.Where(m => m.TotalAccounting.ToString() == searchValue).ToList();
            }

            //total number of rows count     
            recordsTotal = customerData.Count();
            //Paging     
            var data = customerData.Skip(skip).Take(pageSize).ToList();
            //Returning Json Data    
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });




        }
        // GET: RemainingCost
        [CustomAuthorizeAttribute(Roles = "Admin")]
        public ActionResult Index()
        {
            RemainingCostServices RemainingCostServicess = new RemainingCostServices();
           
            List<RemainingCostVM> remainingCostLists = new List<RemainingCostVM>();
            remainingCostLists = RemainingCostServicess.GetAll().ToList();



            IEnumerable<RemainingCostVM> remainingCostList = RemainingCostServicess.GetAll();

            return View(remainingCostList);
        }
        [CustomAuthorizeAttribute(Roles = "Admin")]
        // GET: RemainingCost/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }
        [CustomAuthorizeAttribute(Roles = "Admin")]
        // GET: RemainingCost/Create
        public ActionResult Create()
        {
            RemainingCostVM remainingCostVM = new RemainingCostVM();
            return View(remainingCostVM);
        }

        // POST: RemainingCost/Create
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin")]
        public ActionResult Create(RemainingCostVM remainingCostVM)
        {
            try
            {
                // TODO: Add insert logic here
                RemainingCostServices remainingCostServicess = new RemainingCostServices();
                remainingCostVM.RemainingCosts = remainingCostVM.TotalAccounting - remainingCostVM.Taxi - remainingCostVM.Komerk - remainingCostVM.TurkishCargo - remainingCostVM.PTTCosts;

                RemainingCostServicess.Insert(remainingCostVM);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

      
        [CustomAuthorizeAttribute(Roles = "Admin")]
        // GET: RemainingCost/Edit/5
        public ActionResult Edit(int id)
        {

            RemainingCostVM remainingCostVM = new RemainingCostVM();
            remainingCostVM = RemainingCostServicess.GetById(id);
          
            return View(remainingCostVM);
        }

        // POST: RemainingCost/Edit/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin")]
        public ActionResult Edit(int id, RemainingCostVM remainingCostVM )
        {
            remainingCostVM.RemainingCosts = remainingCostVM.TotalAccounting - remainingCostVM.Taxi - remainingCostVM.Komerk - remainingCostVM.TurkishCargo - remainingCostVM.PTTCosts;

            RemainingCostServicess.Update(remainingCostVM);
            return RedirectToAction("Index");


        }


        [CustomAuthorizeAttribute(Roles = "Admin")]
        // GET: RemainingCost/Delete/5
        public ActionResult Delete(int id)
        {
            RemainingCostServices RemainingCostServices = new RemainingCostServices();
            RemainingCostVM RemainingCost = RemainingCostServices.GetById(id);
            RemainingCostServices.Delete(RemainingCost);


            return RedirectToAction("Index");
        }

       
    }
}