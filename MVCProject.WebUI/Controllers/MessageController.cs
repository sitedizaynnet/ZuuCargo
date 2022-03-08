using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using MVCProject.BLL.Services;
using MVCProject.Common.ViewModels;
using MVCProject.Entities;
using MVCProject.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static MVCProject.Entities.ZuuCargoEntities;

namespace MVCProject.WebUI.Controllers
{
    public class MessageController : BaseController
    {

        
        MessageServices messageServices = new MessageServices();
        ReplyServices replyServices = new ReplyServices();
     
        UserServices userServices = new UserServices();

        [HttpPost]
        [Authorize]
        public ActionResult PostMessage(MessageReplyVM vm)
        {
            var username = User.Identity.Name;
            var UserManager = new UserManager<ApplicationUser>(
                    new UserStore<ApplicationUser>(new ZuuCargoEntities()));
           
            string fullName = "";
            if (!string.IsNullOrEmpty(username))
            {
                var user = UserManager.Users.SingleOrDefault(u => u.UserName == username);
                fullName = string.Concat(new string[] { user.FirstName, " ", user.LastName });
            }
            int msgid = 0;
            
            MessageVM messagetoPost = new MessageVM();
            if (vm.Message.Subject != string.Empty && vm.Message.MessageToPost != string.Empty)
            {
                messagetoPost.DatePosted = DateTime.Now;
                messagetoPost.Subject = vm.Message.Subject;
                messagetoPost.MessageToPost = vm.Message.MessageToPost;
                messagetoPost.Sender = fullName;
                messagetoPost.OwnerId = User.Identity.GetUserId();
                messagetoPost.IsReaded = false;
                messageServices.Insert(messagetoPost);

                AccountController.SendMail(messagetoPost.OwnerId, 5);

                msgid = messagetoPost.Id;
            }

            return RedirectToAction("Messages", "Message");
        }

        [HttpPost]
        [Authorize]
        public ActionResult PostMessageFromProviderDetail(MessageReplyVM vm)
        {
            var username = User.Identity.Name;
            var UserManager = new UserManager<ApplicationUser>(
                    new UserStore<ApplicationUser>(new ZuuCargoEntities()));

            string fullName = "";
            if (!string.IsNullOrEmpty(username))
            {
                var user = UserManager.Users.SingleOrDefault(u => u.UserName == username);
                fullName = string.Concat(new string[] { user.FirstName, " ", user.LastName });
            }
            int msgid = 0;

            MessageVM messagetoPost = new MessageVM();
            if (vm.Message.Subject != string.Empty && vm.Message.MessageToPost != string.Empty)
            {
                messagetoPost.DatePosted = DateTime.Now;
                messagetoPost.Subject = vm.Message.Subject;
                messagetoPost.MessageToPost = vm.Message.MessageToPost;
                messagetoPost.Sender = fullName;
                messagetoPost.OwnerId = User.Identity.GetUserId();
                messagetoPost.OrderId = vm.OrderId;
                messagetoPost.IsReaded = false;

                messageServices.Insert(messagetoPost);

                AccountController.SendMail(messagetoPost.OwnerId, 5);

                msgid = messagetoPost.Id;
            }

            return RedirectToAction("Messages", "Message");
        }
        public ActionResult Create(string id)
        {
            MessageReplyVM vm = new MessageReplyVM();
            ViewBag.ToId = id;
            try
            {
                //ProviderVM provider = providerServices.GetAllByUserId(id).First();
                //ViewBag.Provider = provider;
            }
            catch (Exception)
            {
                try
                {
                    ApplicationUser user = userServices.GetAll().Where(x => x.Id == id).First();
                    EditUserViewModel userVM = new EditUserViewModel();
                    userVM.FirstName = user.FirstName;
                    userVM.LastName = user.LastName;
                    userVM.Email = user.Email;

                    ViewBag.User = userVM;
                }
                catch (Exception)
                {


                }
            }
           
            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> ReplyMessage(MessageReplyVM vm, int messageId)
        {
            var UserManager = new UserManager<ApplicationUser>(
                    new UserStore<ApplicationUser>(new ZuuCargoEntities()));
            var username = User.Identity.Name;
            string fullName = "";
            if (!string.IsNullOrEmpty(username))
            {
                var user = UserManager.Users.SingleOrDefault(u => u.UserName == username);
                fullName = string.Concat(new string[] { user.FirstName, " ", user.LastName });
            }

            if (vm.Reply.ReplyMessage != null)
            {
                ReplyVM _reply = new ReplyVM();
                _reply.ReplyDateTime = DateTime.Now;
                _reply.MessageId = messageId;
                _reply.ReplyFrom = fullName;
                _reply.ReplyMessage = vm.Reply.ReplyMessage;
                _reply.IsReaded = false;

                replyServices.Insert(_reply);
             
            }
            //reply to the message owner          - using email template

            var messageOwner = messageServices.GetAll().Where(x => x.Id == messageId).Select(s => s.Sender).FirstOrDefault();
            var users = from user in UserManager.Users
                        orderby user.FirstName
                        select new
                        {
                            FullName = user.FirstName + " " + user.LastName,
                            UserEmail = user.Email
                        };

            var uemail = users.Where(x => x.FullName == messageOwner).Select(s => s.UserEmail).FirstOrDefault();
            await UserManager.SendEmailAsync(User.Identity.GetUserId(),
                       "ZuuCargo.net Yeni Mesaj",
                       "Mesajınıza cevap verildi. Lütfen ZuuCargo.net'a girerek mesaj kutunuza bakınız :");



          
           
            return RedirectToAction("Message", "Message", new { Id = messageId });

        }

        [HttpPost]
        [Authorize]
        public ActionResult DeleteMessage(int messageId)
        {
            MessageVM _messageToDelete = messageServices.GetById(messageId);
            messageServices.Delete(_messageToDelete);
           

            // also delete the replies related to the message
            var _repliesToDelete = replyServices.GetAll().Where(i => i.MessageId == messageId).ToList();
            if (_repliesToDelete != null)
            {
                foreach (var rep in _repliesToDelete)
                {
                    replyServices.Delete(rep);
                    
                }
            }


            return RedirectToAction("Messages", "Message");
        }
        public ActionResult Messages(int? Id, int? page)
        {
            ViewBag.Id = Id;
            var UserManager = new UserManager<ApplicationUser>(
                   new UserStore<ApplicationUser>(new ZuuCargoEntities()));
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            MessageReplyVM vm = new MessageReplyVM();
            var count = messageServices.GetAll().Where(x => x.Sender == User.Identity.GetUserName()).Count();

            decimal totalPages = count / (decimal)pageSize;
            ViewBag.TotalPages = Math.Ceiling(totalPages);
            var loggedUser = User.Identity.GetUserName();
            var user = UserManager.Users.SingleOrDefault(u => u.UserName == loggedUser);
            var fullName = string.Concat(new string[] { user.FirstName, " ", user.LastName });
            ViewBag.Username = fullName;
            vm.Messages = messageServices.GetAll().Where(x => x.OwnerId == User.Identity.GetUserId() || x.ToId == User.Identity.GetUserId())
                                       .OrderByDescending(x => x.DatePosted).ToPagedList(pageNumber, pageSize);
            foreach (MessageVM m in vm.Messages)
            {
                try
                {
                    if (m.IsReaded != false || !m.IsReaded)
                    {
                        m.IsReaded = true;
                        messageServices.Update(m);
                    }
                    //bool isProvider = providerServices.GetAllByUserId(m.ToId).Any();
                    //if (!isProvider)
                    //{
                    //    m.OwnerName = userServices.GetByUserId(m.ToId).FirstName + " " + userServices.GetByUserId(m.ToId).LastName;

                    //}
                    //else
                    //{
                    //    m.OwnerName = providerServices.GetAllByUserId(m.ToId).FirstOrDefault().ProviderName;
                    //}
                }
                catch (Exception)
                {

                    m.OwnerName = "";
                }
            }
            ViewBag.MessagesInOnePage = vm.Messages;
            ViewBag.PageNumber = pageNumber;

        

            return View(vm);
        }
            public ActionResult Message(int? Id, int? page)
        {
            ViewBag.Id = Id;
            var UserManager = new UserManager<ApplicationUser>(
                    new UserStore<ApplicationUser>(new ZuuCargoEntities()));
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            MessageReplyVM vm = new MessageReplyVM();
            var count = messageServices.GetAll().Where(x=> x.Sender == User.Identity.GetUserName()).Count();
           
            decimal totalPages = count / (decimal)pageSize;
            ViewBag.TotalPages = Math.Ceiling(totalPages);
            var loggedUser = User.Identity.GetUserName();
            var user = UserManager.Users.SingleOrDefault(u => u.UserName == loggedUser);
            var fullName = string.Concat(new string[] { user.FirstName, " ", user.LastName });
            ViewBag.Username = fullName;
            vm.Messages = messageServices.GetAll().Where(x => x.OwnerId == User.Identity.GetUserId() || x.ToId == User.Identity.GetUserId())
                                       .OrderByDescending(x => x.DatePosted).ToPagedList(pageNumber, pageSize);
            foreach (MessageVM m in vm.Messages)
            {
                ViewBag.Owner = ""/*vm.Messages[0].ToId*/;
                try
                {
                    if (m.IsReaded!=false || !m.IsReaded)
                    {
                        m.IsReaded = true;
                        messageServices.Update(m);
                    }
                    //bool isProvider = providerServices.GetAllByUserId(m.OwnerId).Any();
                    //if (!isProvider)
                    //{
                    //    m.OwnerName = userServices.GetByUserId(m.OwnerId).FirstName + " " + userServices.GetByUserId(m.ToId).LastName;
                        
                    //}
                    //else
                    //{
                    //    m.OwnerName = providerServices.GetAllByUserId(m.OwnerId).FirstOrDefault().ProviderName;
                    //}
                }
                catch (Exception)
                {

                    m.OwnerName = "";
                }
            }
            ViewBag.MessagesInOnePage = vm.Messages;
            ViewBag.PageNumber = pageNumber;

            if (Id != null)
            {

                var replies = replyServices.GetAll().Where(x => x.MessageId == Id.Value).OrderByDescending(x => x.ReplyDateTime).ToList();
                if (replies != null)
                {
                    foreach (var rep in replies)
                    {
                        MessageReplyVM.MessageReply reply = new MessageReplyVM.MessageReply();
                        reply.MessageId = rep.MessageId;
                        reply.Id = rep.Id;
                        
                        reply.ReplyMessage = rep.ReplyMessage;
                        
                        reply.ReplyDateTime = rep.ReplyDateTime;
                        reply.MessageDetails = messageServices.GetAll().Where(x => x.Id == rep.MessageId).Select(s => s.MessageToPost).FirstOrDefault();
                        reply.ReplyFrom = rep.ReplyFrom;

                            rep.IsReaded = true;
                            replyServices.Update(rep);
     
                        vm.Replies.Add(reply);
                    }

                }
                else
                {
                    vm.Replies.Add(null);
                }
                vm.Replies= vm.Replies.OrderBy(x => x.ReplyDateTime).ToList();
                
                ViewBag.MessageId = Id.Value;
            }
            return View(vm);
        }

        public JsonResult GetMessageCount()
        {
            
            string userId = User.Identity.GetUserId();
            int messageCount = messageServices.GetAllByUserId(userId).Where(x => x.IsReaded == false).Count();
            return Json(messageCount, JsonRequestBehavior.AllowGet);
        }
    }
}