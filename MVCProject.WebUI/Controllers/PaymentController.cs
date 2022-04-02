using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNet.Identity;
using static MVCProject.Entities.ZuuCargoEntities;
using Microsoft.AspNet.Identity.EntityFramework;
using MVCProject.Entities;
using Nancy.Json;

namespace MVCProject.WebUI.Controllers
{
    public class PaymentController : BaseController
    {
      
        // GET: Payment

        [CustomAuthorize]

        public ActionResult Index()
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            var user = new UserManager<ApplicationUser>(
                    new UserStore<ApplicationUser>(new ZuuCargoEntities()));
            // ####################### DÜZENLEMESİ ZORUNLU ALANLAR #######################
            //
            // API Entegrasyon Bilgileri - Mağaza paneline giriş yaparak BİLGİ sayfasından alabilirsiniz.
            string merchant_id = "111885";
            string merchant_key = "AxxC29YRcMJL1AS7";
            string merchant_salt = "S8BCjpKL5nBe7hA9";
            //
            // Müşterinizin sitenizde kayıtlı veya form vasıtasıyla aldığınız eposta adresi
            string emailstr = user.GetEmail(User.Identity.GetUserId()).ToString();
            //
            // Tahsil edilecek tutar. 9.99 için 9.99 * 100 = 999 gönderilmelidir.
            int payment_amountstr = 9900;
            //
            // Sipariş numarası: Her işlemde benzersiz olmalıdır!! Bu bilgi bildirim sayfanıza yapılacak bildirimde geri gönderilir.
            var random = new Random();
            string merchant_oid = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 7).Select(s => s[random.Next(s.Length)]).ToArray());
            //
            // Müşterinizin sitenizde kayıtlı veya form aracılığıyla aldığınız ad ve soyad bilgisi
            var userId = User.Identity.GetUserId();
            string user_namestr = user.Users.Where(x => x.Id == userId).First().FirstName + " " + user.Users.Where(x => x.Id == userId).First().LastName;
            //
            // Müşterinizin sitenizde kayıtlı veya form aracılığıyla aldığınız adres bilgisi

            string user_addressstr = user.Users.Where(x => x.Id == userId).First().Address;
            //
            // Müşterinizin sitenizde kayıtlı veya form aracılığıyla aldığınız telefon bilgisi
            string user_phonestr = user.Users.Where(x => x.Id == userId).First().Phone;
            //
            // Başarılı ödeme sonrası müşterinizin yönlendirileceği sayfa
            // !!! Bu sayfa siparişi onaylayacağınız sayfa değildir! Yalnızca müşterinizi bilgilendireceğiniz sayfadır!
            // !!! Siparişi onaylayacağız sayfa "Bildirim URL" sayfasıdır (Bakınız: 2.ADIM Klasörü).
            string merchant_ok_url = "https://www.ZuuCargo.net/payment/PaymentComplete";
            //
            // Ödeme sürecinde beklenmedik bir hata oluşması durumunda müşterinizin yönlendirileceği sayfa
            // !!! Bu sayfa siparişi iptal edeceğiniz sayfa değildir! Yalnızca müşterinizi bilgilendireceğiniz sayfadır!
            // !!! Siparişi iptal edeceğiniz sayfa "Bildirim URL" sayfasıdır (Bakınız: 2.ADIM Klasörü).
            string merchant_fail_url = "https://www.ZuuCargo.net/payment/PaymentError";
            //        
            // !!! Eğer bu örnek kodu sunucuda değil local makinanızda çalıştırıyorsanız
            // buraya dış ip adresinizi (https://www.whatismyip.com/) yazmalısınız. Aksi halde geçersiz paytr_token hatası alırsınız.
            string user_ip = GetIPAddress(); /*"212.174.73.82";*/
            if (user_ip == "" || user_ip == null)
            {
                System.Web.HttpContext context = System.Web.HttpContext.Current;

                user_ip = /*Request.ServerVariables["REMOTE_ADDR"]*/context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
            //
            // ÖRNEK $user_basket oluşturma - Ürün adedine göre object'leri çoğaltabilirsiniz
            object[][] user_basket = {
            new object[] {"Premium Üyelik 1 Yıllık", "99.00", 1}, // 1. ürün (Ürün Ad - Birim Fiyat - Adet)
            
            };
            /* ############################################################################################ */


            // İşlem zaman aşımı süresi - dakika cinsinden
            string timeout_limit = "30";
            //
            // Hata mesajlarının ekrana basılması için entegrasyon ve test sürecinde 1 olarak bırakın. Daha sonra 0 yapabilirsiniz.
            string debug_on = "1";
            //
            // Mağaza canlı modda iken test işlem yapmak için 1 olarak gönderilebilir.
            string test_mode = "1";
            //
            // Taksit yapılmasını istemiyorsanız, sadece tek çekim sunacaksanız 1 yapın
            string no_installment = "0";
            //
            // Sayfada görüntülenecek taksit adedini sınırlamak istiyorsanız uygun şekilde değiştirin.
            // Sıfır (0) gönderilmesi durumunda yürürlükteki en fazla izin verilen taksit geçerli olur.
            string max_installment = "0";
            //
            // Para birimi olarak TL, EUR, USD gönderilebilir. USD ve EUR kullanmak için kurumsal@paytr.com 
            // üzerinden bilgi almanız gerekmektedir. Boş gönderilirse TL geçerli olur.
            string currency = "TL";
            //
            // Türkçe için tr veya İngilizce için en gönderilebilir. Boş gönderilirse tr geçerli olur.
            string lang = "tr";


            // Gönderilecek veriler oluşturuluyor
            NameValueCollection data = new NameValueCollection();
            data["merchant_id"] = merchant_id;
            data["user_ip"] = user_ip;
            data["merchant_oid"] = merchant_oid;
            data["email"] = emailstr;
            data["payment_amount"] = payment_amountstr.ToString();
            //
            // Sepet içerği oluşturma fonksiyonu, değiştirilmeden kullanılabilir.
            JavaScriptSerializer ser = new JavaScriptSerializer();
            string user_basket_json = ser.Serialize(user_basket);
            string user_basketstr = Convert.ToBase64String(Encoding.UTF8.GetBytes(user_basket_json));
            data["user_basket"] = user_basketstr;
            //
            // Token oluşturma fonksiyonu, değiştirilmeden kullanılmalıdır.
            string Birlestir = string.Concat(merchant_id, user_ip, merchant_oid, emailstr, payment_amountstr.ToString(), user_basketstr, no_installment, max_installment, currency, test_mode, merchant_salt);
            HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(merchant_key));
            byte[] b = hmac.ComputeHash(Encoding.UTF8.GetBytes(Birlestir));
            data["paytr_token"] = Convert.ToBase64String(b);
            //
            data["debug_on"] = debug_on;
            data["test_mode"] = test_mode;
            data["no_installment"] = no_installment;
            data["max_installment"] = max_installment;
            data["user_name"] = user_namestr;
            data["user_address"] = user_addressstr;
            data["user_phone"] = user_phonestr;
            data["merchant_ok_url"] = merchant_ok_url;
            data["merchant_fail_url"] = merchant_fail_url;
            data["timeout_limit"] = timeout_limit;
            data["currency"] = currency;
            data["lang"] = lang;

            using (WebClient client = new WebClient())
            {
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                byte[] result = client.UploadValues("https://www.paytr.com/odeme/api/get-token", "POST", data);
                string ResultAuthTicket = Encoding.UTF8.GetString(result);
                dynamic json = JValue.Parse(ResultAuthTicket);

                if (json.status == "success")
                {

                    ViewBag.Src = "https://www.paytr.com/odeme/guvenli/" + json.token + "";
                    //Sipariş onayı öncesi sipariş bilgilerini dbye ekle
                  
               
                    //int providerId = providerServices.GetAllByUserId(userId).FirstOrDefault().Id;
                
                    //providerPremiumVM.PaymentCode = merchant_oid;
                    //providerPremiumVM.UserId = userId;
           

                }
                else
                {
                    Response.Write("PAYTR IFRAME failed. reason:" + json.reason + "");
                }
            }
            return View();
        }
        protected string GetIPAddress()
        {
            HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
        // 2. ADIM için örnek kodlar

        // ÖNEMLİ UYARILAR!
        // 1) Bu sayfaya oturum (SESSION) ile veri taşıyamazsınız. Çünkü bu sayfa müşterilerin yönlendirildiği bir sayfa değildir.
        // 2) Entegrasyonun 1. ADIM'ında gönderdiğniz merchant_oid değeri bu sayfaya POST ile gelir. Bu değeri kullanarak
        // veri tabanınızdan ilgili siparişi tespit edip onaylamalı veya iptal etmelisiniz.
        // 3) Aynı sipariş için birden fazla bildirim ulaşabilir (Ağ bağlantı sorunları vb. nedeniyle). Bu nedenle öncelikle
        // siparişin durumunu veri tabanınızdan kontrol edin, eğer onaylandıysa tekrar işlem yapmayın. Örneği aşağıda bulunmaktadır.
        // ####################### DÜZENLEMESİ ZORUNLU ALANLAR #######################
        //
        // API Entegrasyon Bilgileri - Mağaza paneline giriş yaparak BİLGİ sayfasından alabilirsiniz.
        string merchant_key = "AxxC29YRcMJL1AS7";
        string merchant_salt = "S8BCjpKL5nBe7hA9";
        // ###########################################################################
        public ActionResult PaymentComplete()
        {

            return View();
        }
        public ActionResult PaymentError()
        {
            return View();
        }
        public ActionResult PaymentCallback()
        {
            

            // ####### Bu kısımda herhangi bir değişiklik yapmanıza gerek yoktur. #######
            // 
            // POST değerleri ile hash oluştur.
            HttpContext context = System.Web.HttpContext.Current;
            string merchant_oid = context.Request.Form["merchant_oid"];
            string status = context.Request.Form["status"];
            string total_amount = context.Request.Form["total_amount"];
            string hash = context.Request.Form["hash"];

            string Birlestir = string.Concat(merchant_oid, merchant_salt, status, total_amount);
            HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(merchant_key));
            byte[] b = hmac.ComputeHash(Encoding.UTF8.GetBytes(Birlestir));
            string token = Convert.ToBase64String(b);

            //
            // Oluşturulan hash'i, paytr'dan gelen post içindeki hash ile karşılaştır (isteğin paytr'dan geldiğine ve değişmediğine emin olmak için)
            // Bu işlemi yapmazsanız maddi zarara uğramanız olasıdır.
            if (hash.ToString() != token)
            {
                context.Response.Write("PAYTR notification failed: bad hash");
                
            }

            //###########################################################################

            // BURADA YAPILMASI GEREKENLER
            // 1) Siparişin durumunu $post['merchant_oid'] değerini kullanarak veri tabanınızdan sorgulayın.
            // 2) Eğer sipariş zaten daha önceden onaylandıysa veya iptal edildiyse  echo "OK"; exit; yaparak sonlandırın.

            if (status == "success")
            { //Ödeme Onaylandı
              // BURADA YAPILMASI GEREKENLER ONAY İŞLEMLERİDİR.

                try
                {

                    //int providerId = providerPremiumServices.GetAll().Where(x => x.PaymentCode == merchant_oid).FirstOrDefault().ProviderId;
                    //ProviderPremiumVM providerPremiumVM = providerPremiumServices.GetAllByProviderId(providerId).First();
                    //providerPremiumVM.PremiumEndDate = DateTime.Now.AddYears(1);
                    //    providerPremiumServices.Update(providerPremiumVM);

                    context.Response.Clear(); // clear response buffer
                    context.Response.Write("OK"); // write your new text
                    context.Response.End();

                }
                catch (Exception ex)
                {

                    context.Response.Clear(); // clear response buffer
                    context.Response.Write(ex.ToString());
                    context.Response.Write(merchant_oid); // write your new text
                    // write your new text
                    context.Response.End();
                }
                finally
                {

                    context.Response.Clear(); // clear response buffer
                    context.Response.Write("NOT"); // write your new text
                    context.Response.End();

                }
                        
                    
              



                //context.ApplicationInstance.CompleteRequest();
                // end the response so it is sent to the client
                // 1) Siparişi onaylayın.
                // 2) iframe çağırma adımında merchant_oid ve diğer bilgileri veri tabanınıza kayıp edip bu aşamada karşılaştırarak eğer var ise bilgieri çekebilir ve otomatik sipariş tamamlama işlemleri yaptırabilirsiniz.
                // 2) Eğer müşterinize mesaj / SMS / e-posta gibi bilgilendirme yapacaksanız bu aşamada yapabilirsiniz. Bu işlemide yine iframe çağırma adımında merchant_oid bilgisini kayıt edip bu aşamada sorgulayarak verilere ulaşabilirsiniz.
                // 3) 1. ADIM'da gönderilen payment_amount sipariş tutarı taksitli alışveriş yapılması durumunda
                // değişebilir. Güncel tutarı Request.Form['total_amount'] değerinden alarak muhasebe işlemlerinizde kullanabilirsiniz.

            }
            else
            { //Ödemeye Onay Verilmedi

                // Bildirimin alındığını PayTR sistemine bildir.  
                context.Response.Clear(); // clear response buffer
                context.Response.Write("NOL"); // write your new text
                context.Response.End(); // end the response so it is sent to the client
              

                // BURADA YAPILMASI GEREKENLER
                // 1) Siparişi iptal edin.
                // 2) Eğer ödemenin onaylanmama sebebini kayıt edecekseniz aşağıdaki değerleri kullanabilirsiniz.
                // $post['failed_reason_code'] - başarısız hata kodu
                // $post['failed_reason_msg'] - başarısız hata mesajı
            }

            return View();

        }


    }
}

