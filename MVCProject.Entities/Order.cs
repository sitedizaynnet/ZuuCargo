using MVCProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Entities
{
    [Table("Order")]
    public partial class Order
    {
       [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(4000)]
        public string Answer { get; set; }

      

        [StringLength(128)]
        public string UsersId { get; set; }
        public DateTime? OrderTime { get; set; }

        public short CityId { get; set; }
        public short IlceId { get; set; }
        public int SemtId { get; set; }
        public int MahalleId { get; set; }
        public string Status { get; set; }
        public DateTime? BitisTarihi { get; set; }
        public int WinnerBid { get; set; }

    }
}
