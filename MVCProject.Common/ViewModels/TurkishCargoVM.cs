using System;
using System.ComponentModel.DataAnnotations;

namespace MVCProject.Common.ViewModels
{
    public partial class TurkishCargoVM : BaseVM
    {

        [Key]
        public int Id { get; set; }
        public string AirBill { get; set; }
        public double Price { get; set; }

        public double TotalWeight { get; set; }

        public string Notes { get; set; }

    }
}
