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
    public class RefundController : BaseController
    {
        RefundService RefundServices = new RefundService();

        // GET: Admin/Refund
        [CustomAuthorizeAttribute(Roles = "Admin")]
        public ActionResult LoadData()
        {

            //Creating instance of DatabaseContext class  

            IEnumerable<RefundVM> refundList = RefundServices.GetAll();

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
            var customerData = refundList;

            ////Sorting    
            //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            //{
            //    customerData = customerData.OrderBy<RefundVM>(sortColumn + " " + sortColumnDir);
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
        // GET: refund
        [CustomAuthorizeAttribute(Roles = "Admin")]
        public ActionResult Index()
        {
            RefundService RefundServices = new RefundService();
           
            List<RefundVM> refundLists = new List<RefundVM>();
            refundLists = RefundServices.GetAll().ToList();



            IEnumerable<RefundVM> refundList = RefundServices.GetAll();

            return View(refundList);
        }
        [CustomAuthorizeAttribute(Roles = "Admin")]
        // GET: refund/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }
        [CustomAuthorizeAttribute(Roles = "Admin")]
        // GET: refund/Create
        public ActionResult Create()
        {
            RefundVM RefundVM = new RefundVM();
            return View(RefundVM);
        }

        // POST: refund/Create
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin")]
        public ActionResult Create(RefundVM RefundVM)
        {
            try
            {
                // TODO: Add insert logic here
                RefundService RefundServices = new RefundService();
                RefundServices.Insert(RefundVM);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

      
        [CustomAuthorizeAttribute(Roles = "Admin")]
        // GET: refund/Edit/5
        public ActionResult Edit(int id)
        {

            RefundVM RefundVM = new RefundVM();
            RefundVM = RefundServices.GetById(id);
          
            return View(RefundVM);
        }

        // POST: refund/Edit/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin")]
        public ActionResult Edit(int id, RefundVM refundMVM )
        {

            RefundServices.Update(refundMVM);
            return RedirectToAction("Index");


        }


        [CustomAuthorizeAttribute(Roles = "Admin")]
        // GET: refund/Delete/5
        public ActionResult Delete(int id)
        {
            RefundService RefundServices = new RefundService();
            RefundVM refund = RefundServices.GetById(id);


            return View(refund);
        }

        // POST: refund/Delete/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin")]
        public ActionResult Delete(int id, RefundVM refund)
        {

            RefundService refundService = new RefundService();
            refundService.Delete(refund);
             
           
                return RedirectToAction("Index");
           
        }
    }
}