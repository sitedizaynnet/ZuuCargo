using System;
using System.ComponentModel.DataAnnotations;

namespace MVCProject.Common.ViewModels
{
    public partial class KomerkVM : BaseVM
    {
       
        [Key]
        public int Id { get; set; }
        public double? Price { get; set; }
        public double? TotalWeight { get; set; }
        public string Notes { get; set; }

    }
}
