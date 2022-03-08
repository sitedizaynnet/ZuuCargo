using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Entities
{
    [Table("KamuBorc")]
    public partial class KamuBorc
    {

        [Key]
        public int Id { get; set; }

        public int InvoiceNumber { get; set; }
        public string Name   { get; set; }
        public string Telephone { get; set; }

        public Nullable<double> TotalDollar { get; set; }

        public string Notes { get; set; }
        public Nullable<DateTime> Date { get; set; }

        public string Category { get; set; }


    }
}
