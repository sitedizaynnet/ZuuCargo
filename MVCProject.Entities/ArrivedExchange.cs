using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Entities
{
    [Table("ArrivedExchange")]
    public partial class ArrivedExchange
    {

        [Key]
        public int Id { get; set; }
        public double? Price { get; set; }
        public double? Total { get; set; }

        public string Name { get; set; }
        public string Phone { get; set; }


        public DateTime DateAdded { get; set; }


        public string Notes { get; set; }


    }
}
