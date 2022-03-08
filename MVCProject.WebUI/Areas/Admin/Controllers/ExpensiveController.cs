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
    public class ExpensiveController : BaseController
    {
        ExpensiveServices ExpensiveServices = new ExpensiveServices();

        // GET: Admin/Expensive
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult LoadData()
        {

            //Creating instance of DatabaseContext class  

            IEnumerable<ExpensiveVM> ExpensiveList = ExpensiveServices.GetAll();

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
            var customerData = ExpensiveList;

            ////Sorting    
            //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            //{
            //    customerData = customerData.OrderBy<ExpensiveVM>(sortColumn + " " + sortColumnDir);
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


        [AllowAnonymous]
        public ActionResult PrintInvoice(int Id)
        {
            ExpensiveServices expensiveServices = new ExpensiveServices();
            ExpensiveVM accounting = new ExpensiveVM();
            accounting = expensiveServices.GetById(Id);

            return View(accounting);
        }



        // GET: Expensive
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult Index()
        { 
            List<ExpensiveVM> ExpensiveLists = new List<ExpensiveVM>();

            IEnumerable<ExpensiveVM> ExpensiveList = ExpensiveServices.GetAll();

            return View(ExpensiveList);
        }
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        // GET: Expensive/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        // GET: Expensive/Create
        public ActionResult Create()
        {
            ExpensiveVM ExpensiveVM = new ExpensiveVM();
         
            return View(ExpensiveVM);
        }

        // POST: Expensive/Create
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult Create(ExpensiveVM ExpensiveVM)
        {
            //try
            //{
                // TODO: Add insert logic here
                ExpensiveServices ExpensiveServices = new ExpensiveServices();
                ExpensiveServices.Insert(ExpensiveVM);
                return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
        }


        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        // GET: Expensive/Edit/5
        public ActionResult Edit(int id)
        {

            ExpensiveVM ExpensiveVM = new ExpensiveVM();
            ExpensiveVM = ExpensiveServices.GetById(id);

            return View(ExpensiveVM);
        }

        // POST: Expensive/Edit/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult Edit(int id, ExpensiveVM ExpensiveMVM)
        {

            ExpensiveServices.Update(ExpensiveMVM);
            return RedirectToAction("Index");


        }


        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        // GET: Expensive/Delete/5
        public ActionResult Delete(int id)
        {
            ExpensiveServices ExpensiveServices = new ExpensiveServices();
            ExpensiveVM Expensive = ExpensiveServices.GetById(id);


            return View(Expensive);
        }

        // POST: Expensive/Delete/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult Delete(int id, ExpensiveVM Expensive)
        {

            ExpensiveServices ExpensiveServices = new ExpensiveServices();
            ExpensiveServices.Delete(Expensive);


            return RedirectToAction("Index");

        }
    }
}