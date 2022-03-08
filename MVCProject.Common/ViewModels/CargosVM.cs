using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Common.ViewModels
{
    public partial class CargosVM :BaseVM
    {

       
            [Key]
        public int Id { get; set; }

        public string CustomerName { get; set; }
            public string Telephone { get; set; }

            public int Quantity { get; set; }
        public Nullable<double> Expensive { get; set; }

        public Nullable<double> TotalDollar { get; set; }
        public Nullable<double> TotalCargo { get; set; }

        public Nullable<double> Tax { get; set; }
        public string Notes { get; set; }
       

        public Nullable<DateTime> Date { get; set; }

    }
}
