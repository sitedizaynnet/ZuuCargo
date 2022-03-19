
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCProject.Models;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;
using static MVCProject.Entities.ZuuCargoEntities;
using System.Security.Claims;
using Microsoft.AspNet.Identity.Owin;


namespace MVCProject.Entities
{

    public class ZuuCargoEntities : IdentityDbContext<ApplicationUser>
    {
        public static ZuuCargoEntities Create()
        {
            return new ZuuCargoEntities();
        }

        public class ApplicationUser : IdentityUser
        {
            
            public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
            {
                // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
                var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
                // Add custom user claims here
                return userIdentity;
            }


            public string FirstName { get; set; }

         
            public string LastName { get; set; }

            [Required]
            public override string Email { get; set; }

            // Add the new address properties:
            public string Address { get; set; }
            [Required]

            public  short? City { get; set; }
            [Required]
   
            public int Ilce { get; set; }
            [Required]
         

            public int Semt { get; set; }
            [Required]
            
            public int Mahalle { get; set; }

     
            public DateTime? BirthDate { get; set; }
            public string State { get; set; }

            public string TCNo { get; set; }
            public string Phone { get; set; }
            //public DateTime BirthDate { get; set; }
            public byte[] ProfilePicture { get; set; }
            public Nullable<double> Balance { get; set; }
            public Nullable<double> Debt { get; set; }

        }


        public ZuuCargoEntities() : base("name=ZuuCargo")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<ZuuCargoEntities>(null);
            base.OnModelCreating(modelBuilder);

            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public virtual DbSet<Menus> Menus { get; set; }
        public virtual DbSet<SubMenus> SubMenus { get; set; }
    
        public virtual DbSet<Category> Category { get; set; }

       
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Ilceler> Ilceler { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Shipment> Shipments { get; set; }

        public virtual DbSet<Sehirler> Sehirler { get; set; }
        public virtual DbSet<Semt> Semt { get; set; }
       
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Reply> Replies { get; set; }
        public virtual DbSet<Price> Prices{ get; set; }
        public virtual DbSet<Zone> Zones { get; set; }
        public virtual DbSet<StatusUpdates> StatusUpdates { get; set; }

        public virtual DbSet<Accounting> Accountings { get; set; }
        public virtual DbSet<AccountingProducts> AccountingProducts { get; set; }
        public virtual DbSet<Refund> Refunds { get; set; }
        public virtual DbSet<LostOrder> LostOrders { get; set; }
        public virtual DbSet<Cargos> Cargos { get; set; }
        public virtual DbSet<TotalCargo> TotalCargo { get; set; }
        public virtual DbSet<KamuBorc> KamuBorc { get; set; }
        public virtual DbSet<BoxsVar> BoxsVar { get; set; }
        public virtual DbSet<ZuuCargo> ZuuCargo { get; set; }
        public virtual DbSet<Deposit> Deposit { get; set; }
        public virtual DbSet<GpsData> GpsData { get; set; }
        public virtual DbSet<ArrivedMoney> ArrivedMoney { get; set; }


        public virtual DbSet<CustomerLocation> CustomerLocation { get; set; }

        public virtual DbSet<MyBalance> MyBalance { get; set; }

        public virtual DbSet<Expensive> Expensive { get; set; }
        public virtual DbSet<TurkishCargo> TurkishCargo { get; set; }

        public virtual DbSet<TaxiCosts> TaxiCosts { get; set; }
        public virtual DbSet<ArrivedExchange> ArrivedExchange { get; set; }

        public virtual DbSet<Komerk> Komerk { get; set; }
        public virtual DbSet<ShipmentBarcodes> ShipmentBarcodes { get; set; }
        public virtual DbSet<ShipmentTurpexBarcodes> ShipmentTurpexBarcodes { get; set; }

        public virtual DbSet<Reklam> Reklam { get; set; }
        public virtual DbSet<RemainingCost> RemainingCost { get; set; }


        public class IdentityManager
        {
            public bool RoleExists(string name)
            {
                var rm = new RoleManager<IdentityRole>(
                    new RoleStore<IdentityRole>(new ZuuCargoEntities()));
                return rm.RoleExists(name);
            }


            public bool CreateRole(string name)
            {
                var rm = new RoleManager<IdentityRole>(
                    new RoleStore<IdentityRole>(new ZuuCargoEntities()));
                var idResult = rm.Create(new IdentityRole(name));
                return idResult.Succeeded;
            }


            public bool CreateUser(ApplicationUser user, string password)
            {
               

                var um = new UserManager<ApplicationUser>(
                    new UserStore<ApplicationUser>(new ZuuCargoEntities()));
               
                var idResult = um.Create(user, password);
               
                return idResult.Succeeded;
            }


            public bool AddUserToRole(string userId, string roleName)
            {
                var um = new UserManager<ApplicationUser>(
                    new UserStore<ApplicationUser>(new ZuuCargoEntities()));
                var idResult = um.AddToRole(userId, roleName);
                return idResult.Succeeded;
            }


            public void ClearUserRoles(string userId)
            {
                var um = new UserManager<ApplicationUser>(
                    new UserStore<ApplicationUser>(new ZuuCargoEntities()));
                var user = um.FindById(userId);
                var currentRoles = new List<IdentityUserRole>();
                currentRoles.AddRange(user.Roles);
                foreach (var role in currentRoles)
                {
                    um.RemoveFromRole(userId, role.RoleId);
                }
            }
        }

        
    }


}
