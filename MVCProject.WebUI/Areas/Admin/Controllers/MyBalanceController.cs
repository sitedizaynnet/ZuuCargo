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
    public class MyBalanceController : BaseController
    {
        MyBalanceServices MyBalanceServices = new MyBalanceServices();

        // GET: Admin/MyBalance
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult LoadData()
        {

            //Creating instance of DatabaseContext class  

            IEnumerable<MyBalanceVM> MyBalanceList = MyBalanceServices.GetAll();

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
            var customerData = MyBalanceList;

            ////Sorting    
            //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            //{
            //    customerData = customerData.OrderBy<MyBalanceVM>(sortColumn + " " + sortColumnDir);
            //}
            //Search    
            if (!string.IsNullOrEmpty(searchValue))
            {
                customerData = customerData.Where(m => m.OrderId.ToString() == searchValue).ToList();
            }

            //total number of rows count     
            recordsTotal = customerData.Count();
            //Paging     
            var data = customerData.Skip(skip).Take(pageSize).ToList();
            //Returning Json Data    
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });




        }
        // GET: MyBalance
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult Index()
        { 
            List<MyBalanceVM> MyBalanceLists = new List<MyBalanceVM>();

            IEnumerable<MyBalanceVM> MyBalanceList = MyBalanceServices.GetAll();

            return View(MyBalanceList);
        }
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        // GET: MyBalance/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        // GET: MyBalance/Create
        public ActionResult Create()
        {
            MyBalanceVM MyBalanceVM = new MyBalanceVM();
            MyBalanceVM.Date = DateTime.Now;
            return View(MyBalanceVM);
        }

        // POST: MyBalance/Create
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult Create(MyBalanceVM MyBalanceVM)
        {
            //try
            //{
                // TODO: Add insert logic here
                MyBalanceServices MyBalanceServices = new MyBalanceServices();
                MyBalanceServices.Insert(MyBalanceVM);
                return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
        }


        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        // GET: MyBalance/Edit/5
        public ActionResult Edit(int id)
        {

            MyBalanceVM MyBalanceVM = new MyBalanceVM();
            MyBalanceVM = MyBalanceServices.GetById(id);

            return View(MyBalanceVM);
        }

        // POST: MyBalance/Edit/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult Edit(int id, MyBalanceVM MyBalanceMVM)
        {

            MyBalanceServices.Update(MyBalanceMVM);
            return RedirectToAction("Index");


        }


        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        // GET: MyBalance/Delete/5
        public ActionResult Delete(int id)
        {
            MyBalanceServices MyBalanceServices = new MyBalanceServices();
            MyBalanceVM MyBalance = MyBalanceServices.GetById(id);


            return View(MyBalance);
        }

        // POST: MyBalance/Delete/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult Delete(int id, MyBalanceVM MyBalance)
        {

            MyBalanceServices MyBalanceServices = new MyBalanceServices();
            MyBalanceServices.Delete(MyBalance);


            return RedirectToAction("Index");

        }
    }
}