using MVCProject.DAL.Repository;
using MVCProject.DAL.UnitOfWork;
using MVCProject.Entities;
using MVCProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MVCProject.Entities.ZuuCargoEntities;
using System;

namespace MVCProject.BLL.Services
{
    public class UserServices 
    {
        
      



        public List<ApplicationUser> GetAll()
        {
            var Db = new ZuuCargoEntities();
            var users = Db.Users.ToList();
            return (List<ApplicationUser>)users;
        }

        public ApplicationUser GetById(string userName)
        {
            var Db = new ZuuCargoEntities();
            var user = Db.Users.First(u => u.UserName == userName);
            return user;

        }
        public ApplicationUser GetByUserId(string userId)
        {
            var Db = new ZuuCargoEntities();
            var user = Db.Users.First(u => u.Id == userId);
            return user;

        }


    }


}
