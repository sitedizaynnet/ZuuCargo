using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Entities
{
    [Table("LostOrder")]
    public partial class LostOrder
    {

        [Key]
        public int Id { get; set; }

        public string  Marak { get; set; }
        public int  Adet { get; set; }

        public double TotalLira { get; set; }

        public double TotalDollar { get; set; }
        public string Notes { get; set; }

    }
}
