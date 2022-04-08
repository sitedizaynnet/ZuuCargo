using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MVCProject.Models;
using MVCProject.Common.ViewModels;
using static MVCProject.Entities.ZuuCargoEntities;
using MVCProject.Entities;
using System;
using System.Web.Routing;
using static MVCProject.WebUI.Controllers.BaseController;
using System.Security.Claims;
using MVCProject.BLL.Services;

namespace MVCProject.WebUI.Areas.Admin.Controllers
{
    public class AdminAccountController : BaseController
    {
        public AdminAccountController()
        : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ZuuCargoEntities())))
        {
        }


        public AdminAccountController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        //[RequireHttps]
        // GET: Admin/ZuuCargo
        [CustomAuthorizeAttribute(Roles = "Admin, ZuuCargo")]
        public ActionResult LoadData()
        {
            UserServices userServices = new UserServices();
            //Creating instance of DatabaseContext class  

            IEnumerable<ApplicationUser> ZuuCargoList = userServices.GetAll().ToList();

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
            var customerData = ZuuCargoList;

            ////Sorting    
            //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            //{
            //    customerData = customerData.OrderBy<ZuuCargoVM>(sortColumn + " " + sortColumnDir);
            //}
            //Search    
            if (!string.IsNullOrEmpty(searchValue))
            {
                customerData = customerData.Where(m => m.FirstName == searchValue).ToList();
            }

            //total number of rows count     
            recordsTotal = customerData.Count();
            //Paging     
            var data = customerData.Skip(skip).Take(pageSize).ToList();
            //Returning Json Data    
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });




        }
        public UserManager<ApplicationUser> UserManager { get; private set; }

        //[RequireHttps]

        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Admin", "Admin");
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            model.Email = model.UserName;
            if (model.Email != null)
            {
                var user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);
                    
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            //Birşeyler ters giderse formu tekrar göster
            return View(model);
        }






        [CustomAuthorizeAttribute]
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorizeAttribute]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult UserList()
        {
            UserServices userServices = new UserServices();
            ZuuCargoServices ZuuCargoServices = new ZuuCargoServices();
            List<ApplicationUser> userList = new List<ApplicationUser>();
            userList = userServices.GetAll().ToList();
            DepositServices depositServices = new DepositServices();


            IEnumerable<ApplicationUser> UserList = userServices.GetAll();

            foreach (ApplicationUser user in UserList)
            {
                user.Debt = ZuuCargoServices.GetAll().Where(x => x.CustomerAccountId.Trim() == user.Id).Select(i => Convert.ToDouble(i.Price)).Sum();

                user.Balance = depositServices.GetAll().Where(x => x.CustomerId == user.Id).Sum(x=> x.DepositTotal) - user.Debt;

            }



            return View(UserList);
        }


        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("AdminLogin", "Admin");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }
        [CustomAuthorizeAttribute(Roles = "Admin")]
        public ActionResult DepositUser(string Id)
        {
            var Db = new ZuuCargoEntities();
            var usertoUpdate = Db.Users.First(u => u.Id == Id);
            DepositUserVM depositUserVM = new DepositUserVM();
            depositUserVM.UserId = usertoUpdate.Id;
            depositUserVM.UserMail = usertoUpdate.Email;

            return View(depositUserVM);

        }
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin")]
        public ActionResult DepositUser(DepositUserVM depositUserVM)
        {
            UserServices userServices = new UserServices();
           
            
            var Db = new ZuuCargoEntities();
            var usertoUpdate = Db.Users.First(u => u.Id == depositUserVM.UserId);
            usertoUpdate.Balance = depositUserVM.DepositBalance;
            Db.Entry(usertoUpdate).State = System.Data.Entity.EntityState.Modified;
            Db.SaveChangesAsync();

           
            ZuuCargoServices ZuuCargoServices = new ZuuCargoServices();
            List<ApplicationUser> userList = new List<ApplicationUser>();
            userList = userServices.GetAll().ToList();



            IEnumerable<ApplicationUser> UserList = userServices.GetAll();


            return View("UserList", userList);

        }
        [CustomAuthorizeAttribute(Roles = "Admin")]
        public ActionResult Index()
        {

            var users = UserManager.Users.ToList();
            //var admins = users.Where(x => x.Roles.Select(role => role.RoleId).Contains("1" && "a7765c13-acb5-43cb-90b1-a3c0360180e4")).ToList();
            //var adminsVM = admins.Select(user => new EditUserViewModel { UserName = user.UserName, Role = string.Join(",", user.Roles.Select(role => role.RoleId)) }).ToList();
            var model = new List<EditUserViewModel>();
            foreach (var user in users)
            {
                //ApplicationUser user2 = new ApplicationUser();
                //user2.UserName = user.UserName;
                //user2.Id = user.userId;
                //user2.Email = user.Email;
                var u = new EditUserViewModel(user);
                try
                {
                    List<string> roleList = new List<string>();
                    roleList = UserManager.GetRoles(u.userId).ToList();
                    string roleListstring = "";
                    foreach (string role in roleList)
                    {
                        roleListstring = roleListstring + "," + role.ToString();
                    }
                    u.Role = roleListstring;
                }
                catch (Exception)
                {


                }
                model.Add(u);
            }

            ViewBag.Users = model;
            return View();

        }


        [CustomAuthorizeAttribute(Roles = "Admin")]
        public ActionResult Edit(string id, ManageMessageId? Message = null)
        {
            var Db = new ZuuCargoEntities();
            var user = Db.Users.First(u => u.Id == id);
            var model = new EditUserViewModel(user);
            ViewBag.MessageId = Message;
            return View(model);
        }

        [CustomAuthorizeAttribute(Roles = "Admin")]
        public ActionResult LogonWithUser(string userId, ManageMessageId? Message = null)
        {
            var Db = new ZuuCargoEntities();

            //var model = new EditUserViewModel(user);

            //var user = await UserManager.FindAsync(model.UserName, model.Password);
            //if (user != null)
            //{
            //    await SignInAsync(user, model.RememberMe);
            //    return RedirectToAction("Index", "Admin");
            //}
            //else
            //{
            //    ModelState.AddModelError("", "Invalid username or password.");
            //}
            ViewBag.MessageId = Message;
            return View();
        }

        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var Db = new ZuuCargoEntities();
                var user = Db.Users.First(u => u.Id == model.userId);
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                Db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                await Db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }



        [CustomAuthorizeAttribute(Roles = "Admin")]
        public ActionResult Delete(string id = null)
        {
            var Db = new ZuuCargoEntities();
            var user = Db.Users.First(u => u.Id == id);
            var model = new EditUserViewModel(user);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorizeAttribute(Roles = "Admin")]
        public ActionResult DeleteConfirmed(string id)
        {
            var Db = new ZuuCargoEntities();
            var user = Db.Users.First(u => u.Id == id);
            Db.Users.Remove(user);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }


        [Authorize(Roles = "Admin")]
        public ActionResult UserRoles(string id)
        {
            var Db = new ZuuCargoEntities();
            var user = Db.Users.First(u => u.Id == id);
            var model = new SelectUserRolesViewModel(user);

            if (UserManager.IsInRole(id, "Accounter"))
            {
                model.isAccounter = true;
            }
            if (UserManager.IsInRole(id, "Cargo"))
            {
                model.isCargo = true;
            }
            if (UserManager.IsInRole(id, "Cost"))
            {
                model.isCost = true;
            }
            if (UserManager.IsInRole(id, "Reporter"))
            {
                model.isReport = true;
            }
            return View(model);
        }


        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult UserRoles(SelectUserRolesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var idManager = new IdentityManager();
                var Db = new ZuuCargoEntities();
                var userId = Db.Users.First(x => x.UserName == model.UserName).Id;
                var user = Db.Users.First(u => u.Id == userId);

                Db.SaveChanges();
                if (model.isAccounter)
                {
                    UserManager.AddToRole(userId, "Accounter");
                }
                else
                {
                    UserManager.RemoveFromRole(userId, "Accounter");
                }
                if (model.isCargo)
                {
                    UserManager.AddToRole(userId, "Cargo");
                }
                else
                {
                    UserManager.RemoveFromRole(userId, "Cargo");

                }
                if (model.isCost)
                {
                    UserManager.AddToRole(userId, "Cost");
                }
                else
                {
                    UserManager.RemoveFromRole(userId, "Cost");
                }
                if (model.isReport)
                {
                    UserManager.AddToRole(userId, "Reporter");
                }
                else
                {
                    UserManager.RemoveFromRole(userId, "Reporter");
                }
                Db.SaveChanges();


                return RedirectToAction("index");
            }
            return View();
        }


        [Authorize(Roles = "Admin")]
        public ActionResult CreateRole()
        {
            var Db = new ZuuCargoEntities();


            return View();
        }


        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRole(string roleName)
        {

            var idManager = new IdentityManager();

            idManager.CreateRole(roleName);

            return View();
        }



        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }


        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }


        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }


        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }


        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }


        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}