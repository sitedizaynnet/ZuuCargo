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
    public class LostController : BaseController
    {
        LostOrderServices lostOrderServices = new LostOrderServices();

        // GET: Admin/Lost
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult LoadData()
        {

            //Creating instance of DatabaseContext class  

            IEnumerable<LostOrderVM> LostList = lostOrderServices.GetAll();

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
            var customerData = LostList;

            ////Sorting    
            //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            //{
            //    customerData = customerData.OrderBy<LostOrderVM>(sortColumn + " " + sortColumnDir);
            //}
            //Search    
            if (!string.IsNullOrEmpty(searchValue))
            {
                customerData = customerData.Where(m => m.Marak == searchValue).ToList();
            }

            //total number of rows count     
            recordsTotal = customerData.Count();
            //Paging     
            var data = customerData.Skip(skip).Take(pageSize).ToList();
            //Returning Json Data    
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });




        }
        // GET: Lost
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult Index()
        { 
            List<LostOrderVM> LostLists = new List<LostOrderVM>();

            IEnumerable<LostOrderVM> LostList = lostOrderServices.GetAll();

            return View(LostList);
        }
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        // GET: Lost/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        // GET: Lost/Create
        public ActionResult Create()
        {
            LostOrderVM LostOrderVM = new LostOrderVM();
            return View(LostOrderVM);
        }

        // POST: Lost/Create
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult Create(LostOrderVM LostOrderVM)
        {
            try
            {
                // TODO: Add insert logic here
                LostOrderServices lostOrderServices = new LostOrderServices();
                lostOrderServices.Insert(LostOrderVM);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        // GET: Lost/Edit/5
        public ActionResult Edit(int id)
        {

            LostOrderVM LostOrderVM = new LostOrderVM();
            LostOrderVM = lostOrderServices.GetById(id);

            return View(LostOrderVM);
        }

        // POST: Lost/Edit/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult Edit(int id, LostOrderVM LostMVM)
        {

            lostOrderServices.Update(LostMVM);
            return RedirectToAction("Index");


        }


        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        // GET: Lost/Delete/5
        public ActionResult Delete(int id)
        {
            LostOrderServices lostOrderServices = new LostOrderServices();
            LostOrderVM Lost = lostOrderServices.GetById(id);


            return View(Lost);
        }

        // POST: Lost/Delete/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult Delete(int id, LostOrderVM Lost)
        {

            LostOrderServices LostOrderServices = new LostOrderServices();
            LostOrderServices.Delete(Lost);


            return RedirectToAction("Index");

        }
    }
}