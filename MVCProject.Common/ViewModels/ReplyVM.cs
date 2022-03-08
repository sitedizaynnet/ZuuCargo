using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Common.ViewModels
{
    public class ReplyVM : BaseVM
    {
  
        public int Id { get; set; }
        public int MessageId { get; set; }
        public string ReplyFrom { get; set; }

        public string ReplyMessage { get; set; }
        public DateTime ReplyDateTime { get; set; }
        public bool IsReaded { get; set; }

    }
}
