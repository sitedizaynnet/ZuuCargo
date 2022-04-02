using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Entities
{
    [Table("RemainingCost")]
    public partial class RemainingCost
    {

        [Key]
        public int Id { get; set; }
        public double? TotalAccounting { get; set; }
        public double? TurkishCargo { get; set; }
        public double? Komerk { get; set; }
        public double? Taxi { get; set; }
        public double PTTCosts { get; set; }
        public double? RemainingCosts { get; set; }

    
    }
}
