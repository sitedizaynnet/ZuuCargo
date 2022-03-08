using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Entities
{
   
    public partial class Semt
    {
        [Key]
        public int SemtId { get; set; }
        public Int16 IlceId { get; set; }

        [Required]
        [StringLength(32)]
        public string SemtAdi { get; set; }

    }
}
