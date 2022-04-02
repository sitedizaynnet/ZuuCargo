using MVCProject.Entities;
using System.Collections.Generic;
using System.Linq;
using static MVCProject.Entities.ZuuCargoEntities;

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
