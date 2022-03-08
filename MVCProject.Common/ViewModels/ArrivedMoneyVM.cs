using System;
using System.ComponentModel.DataAnnotations;

namespace MVCProject.Common.ViewModels
{

    public partial class ArrivedMoneyVM : BaseVM
    {



        [Key]
        public int Id { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public int OrderId { get; set; }

        public string Notes { get; set; }

    }
}
