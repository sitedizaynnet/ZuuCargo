using System.ComponentModel.DataAnnotations;

// New namespace imports:
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using MVCProject.Entities;
using static MVCProject.Entities.ZuuCargoEntities;
using MVCProject.Common.ViewModels;
using System;

namespace MVCProject.Models
{
    public class ManageUserViewModel : BaseVM
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Şimdiki Şifre")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage =
            "{0} en az {2} karakter olmalıdır.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Yeni Şifre")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Yeniden Girin")]
        [Compare("NewPassword", ErrorMessage =
            "Girdiğiniz şifreler uyuşmuyor.")]
        public string ConfirmPassword { get; set; }
    }
    public class DepositUserVM
    {
        public string UserId { get; set; }
        public string UserMail { get; set; }
        public double DepositBalance { get; set; }
    }

        public class LoginViewModel
    {

        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "E-Posta Adresi")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [Display(Name = "Beni Hatırla?")]
        public bool RememberMe { get; set; }
    }

    public class GroupedUserViewModel
    {
        public List<ApplicationUser> Users { get; set; }
        public List<ApplicationUser> Admins { get; set; }
    }
   
    public class RegisterViewModel
    {
        
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage =
            "{0} en az {2} karakter olmaldır.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Şifre Onayı")]
        [Compare("Password", ErrorMessage =
            "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        // New Fields added to extend Application User class:


        [Display(Name = "Adınız")]
        public string FirstName { get; set; }

     
        [Display(Name = "Soy Adınız")]
        public string LastName { get; set; }


        [Required]
        public string Email { get; set; }

        //[Display(Name = "Doğum Tarihi")]
        //public DateTime? BirthDate { get; set; }

        [Display(Name = "TC Kimlik / Pasaport No")]
        public string TCNo { get; set; }
        [Display(Name = "Doğum Tarihi")]
        public DateTime? BirthDate { get; set; }
        [Display(Name = "Telefon Numarası")]
        public string Phone { get; set; }

        // Add the new address properties:
        [Display(Name = "Adres")]
        public string Address { get; set; }

        [Display(Name = "Şehir")]
        public short City { get; set; }


        [Display(Name = "İlçe")]
        public int Ilce { get; set; }


        [Display(Name = "Semt")]
        public int Semt { get; set; }

        [Display(Name = "Token")]
        public string Token { get; set; }

        [Display(Name = "Yer")]
        public string State { get; set; }

        // Return a pre-poulated instance of AppliationUser:
        public ApplicationUser GetUser()
        {
            var user = new ApplicationUser()
            {
                UserName = this.Email,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email,
                Address = this.Address,
                City = this.City,
                State = this.State,
                TCNo = this.TCNo,
                Phone = this.Phone,
                BirthDate = this.BirthDate,
                Ilce = this.Ilce,
                Semt = this.Semt,
                Token  = this.Token
            };
            return user;
        }
    }


    public class EditUserViewModel
    {
        public string Role { get; set; }
        public EditUserViewModel() { }

        // Allow Initialization with an instance of ApplicationUser:
        public EditUserViewModel(ApplicationUser user)
        {
            this.UserName = user.UserName;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Email = user.Email;
            this.Address = user.Address;
            this.City = user.City;
            this.Ilce = user.Ilce;
            this.Semt = user.Semt;
            this.Token = user.Token;
            this.userId = user.Id;
            this.TCNo = user.TCNo;
            this.BirthDate = user.BirthDate;
            
            this.Phone = user.Phone;


        }

        public string userId { get; set; }
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Adı")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Soyadı")]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }
        // Add the new address properties:

        [Display(Name = "Adres")]
        public string Address { get; set; }

        [Display(Name = "Şehir")]
        public short? City { get; set; }


        [Display(Name = "Doğum Tarihi")]
        public DateTime? BirthDate { get; set; }
        [Display(Name = "TC Kimlik / Pasaport No")]
        public string TCNo { get; set; }

        [Display(Name = "Telefon Numarası")]
        public string Phone { get; set; }
     
        [Required]
       
        public int Ilce { get; set; }
        [Required]
      

        public int Semt { get; set; }
        [Required]
        [Display(Name = "Token")]

        public string Token { get; set; }
    }


    public class SelectUserRolesViewModel
    {
        public string UserId { get; set; }
        public SelectUserRolesViewModel()
        {
            this.Roles = new List<SelectRoleEditorViewModel>();
        }
        public string Role { get; set; }
        public bool isCargo { get; set; }
        public bool isAccounter { get; set; }
        public bool isReport { get; set; }
        public bool isCost { get; set; }
        public bool isAgent { get; set; }



        // Enable initialization with an instance of ApplicationUser:
        public SelectUserRolesViewModel(ApplicationUser user)
            : this()
        {
            this.UserName = user.UserName;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Address = user.Address;
          
        var Db = new ZuuCargoEntities();

            // Add all available roles to the list of EditorViewModels:
            var allRoles = Db.Roles;
            foreach (var role in allRoles)
            {
                // An EditorViewModel will be used by Editor Template:
                var rvm = new SelectRoleEditorViewModel(role);
                this.Roles.Add(rvm);
            }

            // Set the Selected property to true for those roles for 
            // which the current user is a member:
            foreach (var userRole in user.Roles)
            {
                //var checkUserRole = this.Roles.Find(r => r.RoleName == userRole.RoleId);
                //checkUserRole.Selected = true;
            }
        }

        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        // Add the new address properties:
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public List<SelectRoleEditorViewModel> Roles { get; set; }
    }

    // Used to display a single role with a checkbox, within a list structure:
    public class SelectRoleEditorViewModel
    {
        public SelectRoleEditorViewModel() { }
        public SelectRoleEditorViewModel(IdentityRole role)
        {
            this.RoleName = role.Name;
        }

        public bool Selected { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} ila {2} karakter olmalıdır.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Şifrenizi tekrar yazınız")]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
    public class ExternalLoginConfirmationViewModel
    {
  
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

        
        [Required]
        [Display(Name = "Adınız")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Soy Adınız")]
        public string LastName { get; set; }


        [Required]
        public string Email { get; set; }

        //[Display(Name = "Doğum Tarihi")]
        //public DateTime? BirthDate { get; set; }

        [Display(Name = "TC Kimlik / Pasaport No")]
        public string TCNo { get; set; }
        [Display(Name = "Doğum Tarihi")]
        public DateTime? BirthDate { get; set; }
        [Display(Name = "Telefon Numarası")]
        public string Phone { get; set; }

        // Add the new address properties:
        [Display(Name = "Adres")]
        public string Address { get; set; }

        [Display(Name = "Şehir")]
        public short City { get; set; }


        [Display(Name = "İlçe")]
        public int Ilce { get; set; }


        [Display(Name = "Semt")]
        public int Semt { get; set; }

        [Display(Name = "Token")]
        public string Token { get; set; }

    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    //public class SendCodeViewModel
    //{
    //    public string SelectedProvider { get; set; }
    //    public ICollection<SelectListItem> Providers { get; set; }
    //    public string ReturnUrl { get; set; }
    //    public bool RememberMe { get; set; }
    //}

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}