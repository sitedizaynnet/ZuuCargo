using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Entities
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string MessageToPost { get; set; }
        public string Sender { get; set; }
        public DateTime DatePosted { get; set; }
        public string OwnerId { get; set; }
        public string ToId { get; set; }
        public Nullable<int> OrderId { get; set; }
        public bool IsReaded { get; set; }

    }
}
