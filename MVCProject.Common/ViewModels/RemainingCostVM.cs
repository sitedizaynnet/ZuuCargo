using System;
using System.ComponentModel.DataAnnotations;

namespace MVCProject.Common.ViewModels
{
    public partial class RemainingCostVM : BaseVM
    {
       
        [Key]
        public int Id { get; set; }
        public double? TotalAccounting { get; set; }
        public double? TurkishCargo { get; set; }
        public double? Komerk { get; set; }
        public double? Taxi { get; set; }
        public double? RemainingCosts { get; set; }

    }
}
