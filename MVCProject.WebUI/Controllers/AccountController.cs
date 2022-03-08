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

using MVCProject.WebUI.Areas.Admin.Controllers;
using static MVCProject.WebUI.Controllers.BaseController;
using MVCProject.BLL.Services;
using System;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.AspNet.Identity.Owin;
using MVCProject.WebUI.App_Start;
using static MVCProject.WebUI.App_Start.IdentityConfig;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Web.Security;

namespace MVCProject.WebUI.Controllers
{

    public class AccountController : BaseController
    {
        OrderServices orderServices = new OrderServices();
        SehirlerServices sehirlerServices = new SehirlerServices();
        IlcelerServices IlcelerServices = new IlcelerServices();
        SemtServices semtServices = new SemtServices();
       
        
      
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        [CustomAuthorizeAttribute]
        public ActionResult Home()
        {

            List<ZoneVM> zoneList = zoneServices.GetAll().ToList();
            ViewData["ZoneList"] = zoneList;

            return View();
        }

        public class CaptchaResult
        {
            [JsonProperty("success")]
            public bool Success { get; set; }
            [JsonProperty("error-codes")]
            public List<string> ErrorCodes { get; set; }
        }

        public ActionResult OrderDetails(int id)
        {
            OrderVM orderVM = orderServices.GetById(id);
          
           
            try
            {
                UserServices userServices = new UserServices();

                orderVM.UserName = userServices.GetByUserId(orderVM.UsersId).FirstName + " " + userServices.GetByUserId(orderVM.UsersId).LastName;

            }
            catch (Exception)
            {

               
            }

            return View(orderVM);
        }
            //GET ORDERS
            public ActionResult Orders()
        {
            //Create Lists

            List<OrderVM> UserOrders = orderServices.GetAllByUserId(User.Identity.GetUserId()).OrderBy(o => o.OrderTime).ToList();

            foreach (OrderVM item in UserOrders)
            {
                
                try
                {
                 
                    item.CityAdi = sehirlerServices.GetById(item.CityId).SehirAdi;
                    item.IlceAdi = IlcelerServices.GetById(item.IlceId).IlceAdi;
                    item.SemtAdi = semtServices.GetById(item.SemtId).SemtAdi;
                 
                }
                catch (Exception)
                {

                   
                }
               
            }
            //Return VM
            return View(UserOrders);
        }
        public ActionResult _CreateComment(/*int rentorderid*/)
        {
            return PartialView();
        }
        public ActionResult CreateComment(int orderId,Int16 rate_1, Int16 rate_2, Int16 rate_3, Int16 rate_4, string rate_content)
        {
            CommentVM commentVM = new CommentVM();
            commentVM.Rate1 = rate_1;
            commentVM.Rate2 = rate_2;
            commentVM.Rate3 = rate_3;
            commentVM.Rate4 = rate_4;
            commentVM.Comment_Text = rate_content;
            commentVM.OrderId = orderId;
            CommentServices commentServices = new CommentServices();
            commentServices.Insert(commentVM);
            OrderServices orderServices = new OrderServices();
            OrderVM orderVM = orderServices.GetById(orderId);
            orderVM.Status = "Tamamlandı";
            orderServices.Update(orderVM);
            bool success = false;





           // orderVM.Status = "Tamamlandı";


           
            return Json(success,JsonRequestBehavior.AllowGet);
        }

        //public ActionResult UpdateOrAddUser(string userName, object myData, ManageMessageId? Message = null)
        //{
        //    ViewBag.MessageId = Message;
        //    bool success = false;
        //    string error = "";

        //    try
        //    {
        //        var Db = new ZuuCargoEntities();
        //        success = (Db.Users.First(u => u.UserName == userName) == null);
        //        ApplicationUser user = (ApplicationUser)myData;
        //        Db.Users.Attach(user);
        //    }
        //    catch (NullReferenceException)
        //    {
        //        ViewBag.MessageId = Message;
        //        bool success = false;
        //        string error = "";

        //        var registermodel = new RegisterViewModel(user);
        //        SupplierCommentsServices commentservice = new SupplierCommentsServices();
        //        success = commentservice.Insert(suppliercomment);


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

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public AccountController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ZuuCargoEntities())))
        {
           
        }


        public AccountController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
            var provider = new DpapiDataProtectionProvider("MVCProject");
            UserManager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(provider.Create("EmailConfirmation"));
            userManager.EmailService = new EmailService();
            // Configure validation logic for usernames
            userManager.UserValidator = new UserValidator<ApplicationUser>(userManager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,

            };

            // Configure user lockout defaults
            userManager.UserLockoutEnabledByDefault = true;
            userManager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            userManager.MaxFailedAccessAttemptsBeforeLockout = 5;
        }

        public static  void SendMail(string usersId, int type)
        {
            var account = new AccountController();
            try
            {
                if (type == 2)
                {
                    account.UserManager.SendEmailAsync(usersId,
             "Yeni Teklif Alındı - ZuuCargo.net",
             "İlanınıza yeni teklif geldi. Gelen teklifleri incelemek için hesabınıza giriş yapınız. https://www.ZuuCargo.net/Account/Orders");
                   
                }
                else if (type == 3)
                {
                    account.UserManager.SendEmailAsync(usersId, "ZuuCargo.net -Yeni İlan",
        "İlanınız kaydedildi. 48 saat içinde ilanınıza teklifler toplanacaktır. Bu süre sonunda sizi bilgilendireceğiz. Gelen tekliflerden sizin için en uygununu seçip işinizi uygun bir şekilde yaptırabilirsiniz. ");
                   
                }
                else if (type == 4)
                {
                    account.UserManager.SendEmailAsync(usersId, "ZuuCargo.net - Yeni İş Fırsatı",
                         "Hizmet verdiğiniz kategoride yeni bir iş fırsatı bulunuyor. Hemen teklif verin işi siz kapın. https://www.ZuuCargo.net/account/Login");

                }
                else if (type == 5)
                {
                    account.UserManager.SendEmailAsync(usersId,
                                 "Yeni Mesaj Alındı - ZuuCargo.net",
                                 "Yeni bir mesaj aldınız. Okumak için hesabınıza giriş yapınız. https://www.ZuuCargo.net/Account/Login");
                }
                else if (type == 6)
                {
                    account.UserManager.SendEmailAsync(usersId,
                                 "İşiniz Tamamlandı - ZuuCargo.net",
                                 "Ustamıza verdiğiniz iş tamamlanmış görünüyor. Usta hakkında yorumunuzu bırakmak için hesabınıza giriş yapınız. https://www.ZuuCargo.net/Account/Login");
                }
                else if (type == 7)
                {
                    account.UserManager.SendEmailAsync(usersId,
                                 "Teklifiniz Seçildi - ZuuCargo.net",
                                 "İşi aldınız! Kullanıcı verdiğiniz teklifi seçti. İşi tamamlamak için hesabınıza giriş yaparak kullanıcı ile iletişime geçiniz.Hesabınıza giriş yapmak için tıklayınız. https://www.ZuuCargo.net/Account/Login");
                }


            }
            catch (Exception ex)
            {

                
            }
        }
        public  UserManager<ApplicationUser> UserManager { get; private set; }

        ZoneServices zoneServices = new ZoneServices();
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            List<ZoneVM> zoneList = zoneServices.GetAll().ToList();
            ViewData["ZoneList"] = zoneList;

            ViewBag.ReturnUrl = returnUrl;
            return View();

        }


        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await UserManager.FindAsync(UserManager.FindByEmailAsync(model.Email).Result.UserName, model.Password);


                    if (user != null)
                    {
                        await SignInAsync(user, model.RememberMe);
                        if (returnUrl != null)
                        {
                            return Redirect(returnUrl);

                        }
                        else
                        {

                            return RedirectToAction("Index", "Home");

                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Kullanıcı adı yada şifreniz hatalı.");
                        
                        
                    }
                }
            }
            catch (Exception)
            {

                ModelState.AddModelError("", "Wrong password or username.");
                
            }
            List<ZoneVM> zoneList = zoneServices.GetAll().ToList();
            ViewData["ZoneList"] = zoneList;

            // If we got this far, something failed, redisplay form
            TempData["Error"] = "Wrong password or username";
            return RedirectToAction("Index","Home");
        }



    
        public ActionResult Index(EditUserViewModel model)
        {

           
            return View(model);
        }

    public ActionResult Register()
        {
            List<ZoneVM> zoneList = zoneServices.GetAll().ToList();
            ViewData["ZoneList"] = zoneList;
            return View();
        }
        public ActionResult RegisterCompleted()
        {
            List<ZoneVM> zoneList = zoneServices.GetAll().ToList();
            ViewData["ZoneList"] = zoneList;
            return View();
        }
  [CustomAuthorize]
        public ActionResult ListKargo()
        {
            ZuuCargoServices ZuuCargoServices = new ZuuCargoServices();
            List<ZuuCargoVM> ZuuCargoVMList = new List<ZuuCargoVM>();
            string userId = User.Identity.GetUserId();
            ZuuCargoVMList = ZuuCargoServices.GetAll().Where(x =>  x.CustomerAccountId.Trim() == userId ).OrderByDescending(x => x.Date).ToList();

            List<ZoneVM> zoneList = zoneServices.GetAll().ToList();
            ViewData["ZoneList"] = zoneList;

            return View(ZuuCargoVMList);
        }

        public ActionResult ListKargoWithId(int id)
        {
            ZuuCargoServices ZuuCargoServices = new ZuuCargoServices();
            List<ZuuCargoVM> ZuuCargoVMList = new List<ZuuCargoVM>();

            ZuuCargoVMList = ZuuCargoServices.GetAll().Where(x => x.Id == id).OrderByDescending(x => x.Date).ToList();

            List<ZoneVM> zoneList = zoneServices.GetAll().ToList();
            ViewData["ZoneList"] = zoneList;

            return View(ZuuCargoVMList);
        }
        public JsonResult RemoteDataSource_SehirleriGetir(string text)
        {

            SehirlerServices sehirlerServices = new SehirlerServices();

            var sehirler = sehirlerServices.GetAll().Select(x => new SehirlerVM
            {
                SehirId = x.SehirId,
                SehirAdi = x.SehirAdi
               
            });

            return Json(sehirler, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RemoteDataSource_IlceleriGetir(int? city)
        {
            IlcelerServices ilcelerServices = new IlcelerServices();
            var ilce = ilcelerServices.GetAll().AsQueryable().Where(p => p.SehirId == city);


            return Json(ilce.Select(p => new { IlceId = p.IlceId, IlceAdi = p.IlceAdi }), JsonRequestBehavior.AllowGet);
        }
        public JsonResult RemoteDataSource_IlceleriGetir2(int? ilsecim)
        {
            IlcelerServices ilcelerServices = new IlcelerServices();
            var ilce = ilcelerServices.GetAll().AsQueryable().Where(p => p.SehirId == ilsecim);


            return Json(ilce.Select(p => new { IlceId = p.IlceId, IlceAdi = p.IlceAdi }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult RemoteDataSource_SemtGetir(int? ilce)
        {
            SemtServices semtServices = new SemtServices();
            var semt = semtServices.GetAll().AsQueryable().Where(o => o.IlceId == ilce); 

            return Json(semt.Select(o => new { SemtId = o.SemtId, SemtAdi = o.SemtAdi}), JsonRequestBehavior.AllowGet);
        }

        //public JsonResult RemoteDataSource_MahalleGetir(int? semt)
        //{
        //    MahalleServices mahalleServices = new MahalleServices();
        //    var mahalle = mahalleServices.GetAll().AsQueryable().Where(o => o.SemtId == semt);


        //    return Json(mahalle.Select(o => new { MahalleId = o.MahalleId, MahalleAdi = o.MahalleAdi }), JsonRequestBehavior.AllowGet);
        //}
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Dış oturum açma sağlayıcısına yönlendirme iste
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }
        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }
            // Dışarıdan oturum açma sağlayıcıları eklerken XSRF koruması için kullanılır
            private const string XsrfKey = "XsrfId";
            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }
            var user = await UserManager.FindByEmailAsync(loginInfo.Email);
            if (user !=null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                return RedirectToAction("Home", "Account");
            }
            // Kullanıcı zaten oturum açtıysa, bu dışarıdan oturum açma sağlayıcısıyla kullanıcı oturumunu açın
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // Kullanıcı bir hesaba sahip değilse, kullanıcıdan bir hesap oluşturmasını isteyin
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Dış oturum açma sağlayıcısından kullanıcıyla ilgili bilgileri al
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                user.Address = model.Address;
                user.City = model.City;
                user.FirstName = model.FirstName;
                user.Ilce = model.Ilce;
                user.LastName = model.LastName;
             
                user.Semt = model.Semt;
               
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {

           

           
              
                    var user = model.GetUser();
                    ////EMail Confirmed TRUE 
                    //user.EmailConfirmed = true;
                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        var callbackUrl = Url.Action(
                           "ConfirmEmail", "Account",
                           new { userId = user.Id, code = code },
                           protocol: Request.Url.Scheme);




                        //await UserManager.SendEmailAsync(user.Id,
                        //   "ZuuCargo.net Hesap Onay Maili",
                        //   "Hesabınızı onaylamak için bu bağlantıya tıklayınız : <a href=\""
                        //                                   + callbackUrl + "\">Hesabımı Onayla</a>");
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", result.Errors.First().ToString()); 
                    }

                
            

            // If we got this far, something failed, redisplay form
            return RedirectToAction("Index","Home");
        }
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                var code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account",
            new { UserId = user.Id, code = code }, protocol: Request.Url.Scheme);
                await UserManager.SendEmailAsync(user.Id, "Reset Password",
            "Bağlantıya tıklayarak şifrenizi sıfırlayabilirsiniz : <a href=\"" + callbackUrl + "\">Şifremi Sıfırla</a>");
                return View("ForgotPasswordConfirmation");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            if (result.Succeeded)
            {
                return RedirectToAction("RegisterCompleted", "Account");

            }
            AddErrors(result);
            return View();
        }
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }


        [CustomAuthorizeAttribute]
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Şifreniz Değiştirildi."
                : message == ManageMessageId.SetPasswordSuccess ? "Şifreniz Ayarlandı."
                : message == ManageMessageId.RemoveLoginSuccess ? "Harici giriş silindi."
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




        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
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




        [CustomAuthorizeAttribute]
        public ActionResult Edit( ManageMessageId? Message = null)
        {
            var Db = new ZuuCargoEntities();
            string userName = User.Identity.GetUserName();
            var user = Db.Users.First(u => u.UserName == userName);
            var model = new EditUserViewModel(user);
            ViewBag.MessageId = Message;
            
            return View(model);
        }

        [HttpPost]
        public  JsonResult EditAjax(EditUserViewModel userModel , ManageMessageId? Message = null)
         {
            var Db = new ZuuCargoEntities();
            string userName = userModel.UserName;
            var user = Db.Users.First(u => u.UserName == userName);

            user.FirstName = userModel.FirstName;
            user.LastName = userModel.LastName;
            user.Phone = userModel.Phone;
          
            user.TCNo = userModel.TCNo;
            user.Address = userModel.Address;
            user.City = userModel.City;
      
            ViewBag.MessageId = Message;
            Db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            var success = Db.SaveChanges();
            return Json("'result':'success'", JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        [CustomAuthorizeAttribute]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var Db = new ZuuCargoEntities();
                string UserId = User.Identity.GetUserId();
                var user = Db.Users.First(u => u.Id == UserId);
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.City = model.City;
                user.Ilce =  model.Ilce;
                user.Semt = model.Semt;
             
                user.Address = model.Address;
                user.BirthDate = model.BirthDate;
                user.TCNo = model.TCNo;
                user.Phone = model.Phone;
                Db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                await Db.SaveChangesAsync();
                return RedirectToAction("Home","Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        [CustomAuthorizeAttribute]
        public ActionResult Delete(string id = null)
        {
            var Db = new ZuuCargoEntities();
            var user = Db.Users.First(u => u.UserName == id);
            var model = new EditUserViewModel(user);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }



        #region Helpers

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

        #endregion
    }
}