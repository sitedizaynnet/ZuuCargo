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
    public class ArrivedMoneyController : BaseController
    {
        ArrivedMoneyServices ArrivedMoneyServices = new ArrivedMoneyServices();

        // GET: Admin/ArrivedMoney
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult LoadData()
        {

            //Creating instance of DatabaseContext class  

            IEnumerable<ArrivedMoneyVM> ArrivedMoneyList = ArrivedMoneyServices.GetAll();

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
            var customerData = ArrivedMoneyList;

            ////Sorting    
            //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            //{
            //    customerData = customerData.OrderBy<ArrivedMoneyVM>(sortColumn + " " + sortColumnDir);
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
        // GET: ArrivedMoney
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult Index()
        { 
            List<ArrivedMoneyVM> ArrivedMoneyLists = new List<ArrivedMoneyVM>();

            IEnumerable<ArrivedMoneyVM> ArrivedMoneyList = ArrivedMoneyServices.GetAll();

            return View(ArrivedMoneyList);
        }
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        // GET: ArrivedMoney/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        // GET: ArrivedMoney/Create
        public ActionResult Create()
        {
            ArrivedMoneyVM ArrivedMoneyVM = new ArrivedMoneyVM();
            ArrivedMoneyVM.Date = DateTime.Now;
            return View(ArrivedMoneyVM);
        }

        // POST: ArrivedMoney/Create
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult Create(ArrivedMoneyVM ArrivedMoneyVM)
        {
            //try
            //{
                // TODO: Add insert logic here
                ArrivedMoneyServices ArrivedMoneyServices = new ArrivedMoneyServices();
                ArrivedMoneyServices.Insert(ArrivedMoneyVM);
                return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
        }


        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        // GET: ArrivedMoney/Edit/5
        public ActionResult Edit(int id)
        {

            ArrivedMoneyVM ArrivedMoneyVM = new ArrivedMoneyVM();
            ArrivedMoneyVM = ArrivedMoneyServices.GetById(id);

            return View(ArrivedMoneyVM);
        }

        // POST: ArrivedMoney/Edit/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult Edit(int id, ArrivedMoneyVM ArrivedMoneyMVM)
        {

            ArrivedMoneyServices.Update(ArrivedMoneyMVM);
            return RedirectToAction("Index");


        }


        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        // GET: ArrivedMoney/Delete/5
        public ActionResult Delete(int id)
        {
            ArrivedMoneyServices ArrivedMoneyServices = new ArrivedMoneyServices();
            ArrivedMoneyVM ArrivedMoney = ArrivedMoneyServices.GetById(id);


            return View(ArrivedMoney);
        }

        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
   
        //ARRIVED >TO MY BALANCE TRANSFER
        public ActionResult TransferToMyBalance(int id)
        {
        
            ArrivedMoneyServices ArrivedMoneyServices = new ArrivedMoneyServices();
            ArrivedMoneyVM arrivedMoney = ArrivedMoneyServices.GetById(id);
            MyBalanceVM myBalanceVM = new MyBalanceVM();
            myBalanceVM.Amount = arrivedMoney.Amount;
            myBalanceVM.Notes = arrivedMoney.Notes;
            myBalanceVM.OrderId = arrivedMoney.OrderId;
            myBalanceVM.Date = DateTime.Now;
            MyBalanceServices myBalanceServices = new MyBalanceServices();
            myBalanceServices.Insert(myBalanceVM);

            ArrivedMoneyServices arrivedMoneyServices = new ArrivedMoneyServices();
           
            arrivedMoneyServices.Delete(arrivedMoney);

            return RedirectToAction("Index");
        }

        // POST: ArrivedMoney/Delete/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult Delete(int id, ArrivedMoneyVM ArrivedMoney)
        {

            ArrivedMoneyServices ArrivedMoneyServices = new ArrivedMoneyServices();
            ArrivedMoneyServices.Delete(ArrivedMoney);


            return RedirectToAction("Index");

        }
    }
}