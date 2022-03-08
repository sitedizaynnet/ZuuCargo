using Microsoft.AspNet.Identity;
using MVCProject.BLL.Services;
using MVCProject.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProject.WebUI.Controllers
{
    public class CommentsController : Controller
    {
        // GET: Comments
        public ActionResult Comments()
        {

            return View();
        }


        public ActionResult CreateComment(/*int rentorderid*/)
        {
            return View();
        }



        //public ActionResult RateProduct(int id, int rate)
        //{

        //    SupplierCommentsVM suppliercomment = new SupplierCommentsVM();

        //    suppliercomment.CommentsId = id;
        //    suppliercomment.CommentScore = rate;
        //    suppliercomment.UserId = User.Identity.GetUserId();
        //    int RentOrderId = (int)TempData["RentOrderId"];
        //    TempData["RentOrderId"] = RentOrderId;
        //    TempData.Keep();
        //    suppliercomment.RentOrderId = RentOrderId;
        //    RentOrdersServices rentorderservice = new RentOrdersServices();

        //    int SupplierId = rentorderservice.GetAll().Where(x => x.Id == RentOrderId).SingleOrDefault().SupplierId;
        //    suppliercomment.SuppliersId = SupplierId;

        //    bool success = false;
        //    string error = "";

        //    try
        //    {
        //        if (suppliercomment.Id > 0)
        //        {
        //            SupplierCommentsServices commentservice = new SupplierCommentsServices();
        //            success = commentservice.Update(suppliercomment);

        //        }
        //        else
        //        {
        //            SupplierCommentsServices commentservice = new SupplierCommentsServices();
        //            success = commentservice.Insert(suppliercomment);
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        // get last error
        //        if (ex.InnerException != null)
        //            while (ex.InnerException != null)
        //                ex = ex.InnerException;

        //        error = ex.Message;
        //    }



        //    return Json(new { error = error, success = success, pid = id }, JsonRequestBehavior.AllowGet);
        //}

    }
}