using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Common.ViewModels
{
    public class MessageVM : BaseVM
    {
 
        public int Id { get; set; }
   
        public string Subject { get; set; }

        public string MessageToPost { get; set; }
        public string Sender { get; set; }
        public string OwnerName { get; set; }
        public DateTime DatePosted { get; set; }
        public string OwnerId { get; set; }
        public string ToId { get; set; }
        public Nullable<int> OrderId { get; set; }
        public bool IsReaded { get; set; }


    }
}
