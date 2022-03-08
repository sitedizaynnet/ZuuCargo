using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Entities
{
    [Table("Komerk")]
    public partial class Komerk
    {

        [Key]
        public int Id { get; set; }
        public double? Price { get; set; }
        public double? TotalWeight { get; set; }

      

        public string Notes { get; set; }


    }
}
