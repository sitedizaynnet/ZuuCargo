using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Common.ViewModels
{
    public partial class TotalCargoVM :BaseVM
    {


        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Category { get; set; }

        public Nullable<double> Expensive { get; set; }
        public Nullable<double> OrderTotal { get; set; }
        public string Telephone { get; set; }
        public Nullable<DateTime> Date { get; set; }
        public string Notes { get; set; }

    }
}
