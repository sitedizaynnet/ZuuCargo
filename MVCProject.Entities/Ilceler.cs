using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Entities
{
    [Table("Ilceler")]
    public partial class Ilceler
    {
        public Ilceler()
        {
            Semt = new HashSet<Semt>();
        }

        [Key]
        public Int16 IlceId { get; set; }

        public Int16 SehirId { get; set; }

        [Required]
        [StringLength(30)]
        public string IlceAdi { get; set; }


        public virtual Sehirler Sehirler { get; set; }

        public virtual ICollection<Semt> Semt { get; set; }
    }
}
