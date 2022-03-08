using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Entities
{
    [Table("Sehirler")]
    public partial class Sehirler
    {
        public Sehirler()
        {
            Ilceler = new HashSet<Ilceler>();
        }

        [Key]
        public Int16 SehirId { get; set; }

        [Required]
        [StringLength(20)]
        public string SehirAdi { get; set; }



        public virtual ICollection<Ilceler> Ilceler { get; set; }
    }
}
