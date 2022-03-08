using MVCProject.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Common.ViewModels
{

    public partial class CommentVM:BaseVM
    {
 
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ProviderId { get; set; }


        public string Comment_Text { get; set; }

        public Int16 Rate1 { get; set; }
        public Int16 Rate2 { get; set; }
        public Int16 Rate3 { get; set; }
        public Int16 Rate4 { get; set; }

        public string CommentedUserName { get; set; }
        public DateTime? CommentDate { get; set; }

    }
}
