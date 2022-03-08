using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using SendGrid;


using System.Configuration;
using Microsoft.AspNet.Identity.Owin;
using static MVCProject.Entities.ZuuCargoEntities;
using Microsoft.Owin.Security;
using System.Security.Claims;
using Microsoft.AspNet.Identity.EntityFramework;
using MVCProject.Entities;
using Microsoft.Owin;
using static MVCProject.WebUI.App_Start.IdentityConfig;

namespace MVCProject.WebUI.App_Start
{
    
    public class IdentityConfig
    {
        
        public class EmailService  : IIdentityMessageService
        {
            public async Task SendAsync(IdentityMessage message)
            {
                await ConfigSendGridasync(message);
            }

            // Use NuGet to install SendGrid (Basic C# client lib) 
            private async Task ConfigSendGridasync(IdentityMessage message)
            {
                var myMessage = new SendGridMessage();
                myMessage.AddTo(message.Destination);
                myMessage.From = new System.Net.Mail.MailAddress(
                                    "azure_e5e93296df5fa9c9af328ea2946f13a9@azure.com", "ZuuCargo");
                myMessage.Subject = message.Subject;
                myMessage.Text = message.Body;
                myMessage.Html = message.Body;

                var credentials = new NetworkCredential(
                           ConfigurationManager.AppSettings["mailAccount"],
                           ConfigurationManager.AppSettings["mailPassword"]
                           );

                // Create a Web transport for sending email.
                var transportWeb = new Web(credentials);

                // Send the email.
                if (transportWeb != null)
                {
                    await transportWeb.DeliverAsync(myMessage);
                }
                else
                {
                    Trace.TraceError("Failed to create Web transport.");
                    await Task.FromResult(0);
                }
            }

        }
    }
    // Bu uygulamada kullanılan uygulama kullanıcı yöneticisini yapılandırın. UserManager ASP.NET Identity'de tanımlanır ve uygulama tarafından kullanılır.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ZuuCargoEntities>()));
            // Kullanıcı adları için doğrulama mantığını yapılandırın
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Parolalar için doğrulama mantığını yapılandırın
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Kullanıcı kilitleme varsayılanlarını yapılandırın
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // İki faktörlü kimlik doğrulama sağlayıcılarını kaydedin. Bu uygulama, kullanıcıyı doğrulama adımları olarak Telefon ve E-posta kullanır
            // Kendi sağlayıcınızı yazabilir ve buraya bağlayabilirsiniz.
            manager.RegisterTwoFactorProvider("Telefon Kodu", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Güvenlik kodunuz: {0}"
            });
            manager.RegisterTwoFactorProvider("E-posta Kodu", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Güvenlik Kodu",
                BodyFormat = "Güvenlik kodunuz: {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    internal class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            throw new NotImplementedException();
        }
    }

    // Uygulamada kullanılan uygulama oturum açma yöneticisini yapılandırın.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}