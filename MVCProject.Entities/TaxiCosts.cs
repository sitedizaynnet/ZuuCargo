using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Entities
{
    [Table("TaxiCosts")]
    public partial class TaxiCosts
    {

        [Key]
        public int Id { get; set; }
        public double Amount { get; set; }
        public double Total { get; set; }
        public DateTime Date { get; set; }
        public string   Name { get; set; }
        public string Notes { get; set; }


    }
}
