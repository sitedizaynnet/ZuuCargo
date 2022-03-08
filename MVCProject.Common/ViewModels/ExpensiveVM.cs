using System;
using System.ComponentModel.DataAnnotations;

namespace MVCProject.Common.ViewModels
{
    public partial class ExpensiveVM : BaseVM
    {
       
        [Key]
        public int Id { get; set; }
        public double? Price { get; set; }
        public double? Total { get; set; }

        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime? DateAdded { get; set; }
        public string Notes { get; set; }

    }
}
