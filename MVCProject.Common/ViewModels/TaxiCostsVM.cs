using System;
using System.ComponentModel.DataAnnotations;

namespace MVCProject.Common.ViewModels
{

    public partial class TaxiCostsVM : BaseVM
    {


        [Key]
        public int Id { get; set; }
        public double Amount { get; set; }
        public double Total { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }


    }
}
