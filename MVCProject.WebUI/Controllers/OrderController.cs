using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MVCProject.BLL.Services;
using MVCProject.Common.ViewModels;
using MVCProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static MVCProject.Entities.ZuuCargoEntities;

namespace MVCProject.WebUI.Controllers
{
    public class OrderController : BaseController
    {
        
      
        OrderServices orderServices = new OrderServices();
 
        SehirlerServices sehirlerServices = new SehirlerServices();
        IlcelerServices ilcelerServices = new IlcelerServices();
        SemtServices semtServices = new SemtServices();
       
        CommentServices commentServices = new CommentServices();


        // POST: GetQuestionsBySubCategoryId
    
       
        [CustomAuthorizeAttribute]

        public ActionResult Category(string id)
        {
           
          
            return View();
        }
        //[CustomAuthorize]
        public ActionResult CreateOrder(int id)
        {
            ViewData["ReturnUrl"] = "https://ZuuCargo.net/Order/CreateOrder/" + id;



            ViewData["CatId"] = id;
          
            return View();
        }

        [HttpPost]
        [CustomAuthorize]

        public ActionResult CreateOrder(JsonAnswer[] data)
        {

            OrderVM orderVM = new OrderVM();
           
            orderVM.OrderTime = DateTime.Now;
            orderVM.BitisTarihi = DateTime.Now.AddDays(2);
            
             orderVM.UsersId = User.Identity.GetUserId();
            orderVM.CityId = Convert.ToInt16(data[0].value);
            orderVM.IlceId = Convert.ToInt16(data[1].value);
            if (data[2].value!=null)
            {
                orderVM.SemtId = Convert.ToInt32(data[2].value);
            }
            



            string answers = null;
            for (int i = 5; i < data.Count(); i++)
            {

                answers += (data[i].value != "on") ? data[i].value : data[i].name; ;
                //answers += (data[i].value == "on") ? "" : data[i].name;
                answers += Environment.NewLine;
            }
            orderVM.Answer = answers;
            orderVM.Status = "Teklif Bekliyor";
            orderServices.Insert(orderVM);
            var UserManager = new UserManager<ApplicationUser>(
                     new UserStore<ApplicationUser>(new ZuuCargoEntities()));

            AccountController.SendMail(User.Identity.GetUserId(), 3);
            //Send Providers mail for new order
            //List<ProviderSubCategoryVM> providerSubCategoryVM = providerSubCategoryServices.GetAllBySubCat(Convert.ToInt16(orderVM.SubCatId)).ToList();
            //foreach (ProviderSubCategoryVM providersubcat in providerSubCategoryVM)
            //{
            // AccountController.SendMail(providersubcat.User_Id, 4);
            //}
            //Send Provider mail end 
            return Json(new { newUrl = Url.Action("OrderCompleted", "Order") });
        }

        [CustomAuthorize(Roles = "Provider")]

        public ActionResult CompleteOrder(int Id)
        {
            string orderOwnerUserId = orderServices.GetById(Id).UsersId;
            AccountController.SendMail(orderOwnerUserId, 6);
            OrderVM orderVM = orderServices.GetById(Id);
            orderVM.Status = "Puan Bekleniyor";
            orderServices.Update(orderVM);
            return RedirectToAction("WinningOrders","Bid");
        }
public ActionResult OrderCompleted()
        {
            return View();
        }
      
        //public ActionResult DeleteOrder(int id)
        //{

        //    OrderVM model = orderServices.GetById(id);
        //    orderServices.Delete(model);
        //    bidsToDelete = bidServices.GetAllByOrderId(model.Id);
        //    return RedirectToAction("Orders","Account");
        //}
        public ActionResult SelectBid(int Id)//OrderId
        {
            
            

            return View();
        }
    }
}