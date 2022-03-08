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
    public class ArrivedExchangeController : BaseController
    {
        ArrivedExchangeServices ArrivedExchangeServices = new ArrivedExchangeServices();

        // GET: Admin/ArrivedExchange
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult LoadData()
        {

            //Creating instance of DatabaseContext class  

            IEnumerable<ArrivedExchangeVM> ArrivedExchangeList = ArrivedExchangeServices.GetAll();

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
            var customerData = ArrivedExchangeList;

            ////Sorting    
            //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            //{
            //    customerData = customerData.OrderBy<ArrivedExchangeVM>(sortColumn + " " + sortColumnDir);
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
            ArrivedExchangeServices ArrivedExchangeServices = new ArrivedExchangeServices();
            ArrivedExchangeVM accounting = new ArrivedExchangeVM();
            accounting = ArrivedExchangeServices.GetById(Id);

            return View(accounting);
        }



        // GET: ArrivedExchange
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult Index()
        { 
            List<ArrivedExchangeVM> ArrivedExchangeLists = new List<ArrivedExchangeVM>();

            IEnumerable<ArrivedExchangeVM> ArrivedExchangeList = ArrivedExchangeServices.GetAll();

            return View(ArrivedExchangeList);
        }
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        // GET: ArrivedExchange/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        // GET: ArrivedExchange/Create
        public ActionResult Create()
        {
            ArrivedExchangeVM ArrivedExchangeVM = new ArrivedExchangeVM();
         
            return View(ArrivedExchangeVM);
        }

        // POST: ArrivedExchange/Create
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult Create(ArrivedExchangeVM ArrivedExchangeVM)
        {
            //try
            //{
                // TODO: Add insert logic here
                ArrivedExchangeServices ArrivedExchangeServices = new ArrivedExchangeServices();
                ArrivedExchangeServices.Insert(ArrivedExchangeVM);
                return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
        }


        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        // GET: ArrivedExchange/Edit/5
        public ActionResult Edit(int id)
        {

            ArrivedExchangeVM ArrivedExchangeVM = new ArrivedExchangeVM();
            ArrivedExchangeVM = ArrivedExchangeServices.GetById(id);

            return View(ArrivedExchangeVM);
        }

        // POST: ArrivedExchange/Edit/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult Edit(int id, ArrivedExchangeVM ArrivedExchangeMVM)
        {

            ArrivedExchangeServices.Update(ArrivedExchangeMVM);
            return RedirectToAction("Index");


        }


        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        // GET: ArrivedExchange/Delete/5
        public ActionResult Delete(int id)
        {
            ArrivedExchangeServices ArrivedExchangeServices = new ArrivedExchangeServices();
            ArrivedExchangeVM ArrivedExchange = ArrivedExchangeServices.GetById(id);


            return View(ArrivedExchange);
        }

        // POST: ArrivedExchange/Delete/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult Delete(int id, ArrivedExchangeVM ArrivedExchange)
        {

            ArrivedExchangeServices ArrivedExchangeServices = new ArrivedExchangeServices();
            ArrivedExchangeServices.Delete(ArrivedExchange);


            return RedirectToAction("Index");

        }
    }
}