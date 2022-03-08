using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Common.ViewModels
{
    public partial class KamuBorcVM :BaseVM
    {


        [Key]
        public int Id { get; set; }

        public int InvoiceNumber { get; set; }
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string Category { get; set; }
        public Nullable<double> TotalDollar { get; set; }

        public string Notes { get; set; }
        public Nullable<DateTime> Date { get; set; }

    }
}
