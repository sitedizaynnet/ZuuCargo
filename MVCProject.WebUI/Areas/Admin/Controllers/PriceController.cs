using MVCProject.BLL.Services;
using MVCProject.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProject.WebUI.Areas.Admin.Controllers
{
    public class PriceController : BaseController
    {
        PriceServices priceServices = new PriceServices();
        // GET: Admin/Price
        public ActionResult Index()
        {
            IEnumerable<PriceVM> priceVMList = priceServices.GetAll();
            return View(priceVMList);
        }

        // GET: Admin/Price/Details/5
        public ActionResult Details(int id)
        {
            PriceVM priceVM = new PriceVM();
            priceVM = priceServices.GetById(id);
            return View();
        }

        // GET: Admin/Price/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Price/Create
        [HttpPost]
        public ActionResult Create(PriceVM price)
        {
            try
            {
               
                priceServices.Insert(price);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Price/Edit/5
        public ActionResult Edit(int id)
        {
            PriceVM priceVM = new PriceVM();
            priceVM = priceServices.GetById(id);
            return View(priceVM);
        }

        // POST: Admin/Price/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, PriceVM price)
        {
            try
            {
                PriceVM priceVM = new PriceVM();
                priceServices.Update(price);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Price/Delete/5
        public ActionResult Delete(int id)
        {
            PriceVM priceVM = new PriceVM();
            priceVM = priceServices.GetById(id);
           
      
            return View(priceVM);
        }

        // POST: Admin/Price/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, PriceVM priceVM)
        {
            try
            {
                // TODO: Add delete logic here
                priceServices.Delete(priceVM);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
