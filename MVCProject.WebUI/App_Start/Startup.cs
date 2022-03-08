
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using MVCProject.Entities;
using Microsoft.Owin.Security.Facebook;
using System.Net.Http;
using System;

[assembly: OwinStartup(typeof(MVCProject.WebUI.App_Start.Startup))]

namespace MVCProject.WebUI.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Veritabanı bağlamını ve kullanıcı yöneticisini ve oturum açma yöneticisini istek başına tek örnek kullanacak şekilde yapılandırın
            app.CreatePerOwinContext(ZuuCargoEntities.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");


        //    var facebookOptions = new FacebookAuthenticationOptions()
        //    {
        //        AppId = "2184696628438277",
        //        AppSecret = "169579e4819d8753448f2133dae026ff",
        //        BackchannelHttpHandler = new FacebookBackChannelHandler(),
        //        UserInformationEndpoint = "https://graph.facebook.com/v2.8/me?fields=id,name,email,first_name,last_name",
        //        Scope = { "email" }
        //    };
            
        //app.UseFacebookAuthentication(facebookOptions);


            app.UseGoogleAuthentication(
        clientId: "1079699565652-6g42sosqplql53pffbo04b3r42ec89kh.apps.googleusercontent.com",
        clientSecret: "T4vCtyfBSs7ZOCBU8-RDSy-q");

              }
    }
    public class FacebookBackChannelHandler : HttpClientHandler
    {
        protected override async System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            System.Threading.CancellationToken cancellationToken)
        {
            // Replace the RequestUri so it's not malformed
            if (!request.RequestUri.AbsolutePath.Contains("/oauth"))
            {
                request.RequestUri = new Uri(request.RequestUri.AbsoluteUri.Replace("?access_token", "&access_token"));
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
