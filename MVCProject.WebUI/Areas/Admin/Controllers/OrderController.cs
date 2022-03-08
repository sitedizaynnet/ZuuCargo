using MVCProject.BLL.Services;
using MVCProject.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProject.WebUI.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {

        UserServices userServices = new UserServices();
        OrderServices orderServices = new OrderServices();

        // GET: Admin/Order/Index
        public ActionResult Index()
        {

            List<OrderVM> orderList = new List<OrderVM>();
            orderList = orderServices.GetAll().ToList();

            foreach (OrderVM item in orderList)
            {
                item.UserName = userServices.GetAll().Where(x => x.Id == item.UsersId).FirstOrDefault().UserName;
                }
            return View(orderList);
        }
        // GET: Admin/Order/EditOrder

        public ActionResult EditOrder(int id)
        {
            
            OrderVM model = orderServices.GetById(id);
            model.UserName = userServices.GetAll().Where(x => x.Id == model.UsersId).FirstOrDefault().UserName;

            return View(model);
        }
        [HttpPost]
        public ActionResult EditOrder(OrderVM order)
        {
          
            orderServices.Update(order);
            return RedirectToAction("Index");
        }
        // GET: Admin/Order/DeleteOrder

   
        public ActionResult DeleteOrder(int id)
        {

            OrderVM model = orderServices.GetById(id);
            orderServices.Delete(model);
            return RedirectToAction("Index");
        }
    }
}