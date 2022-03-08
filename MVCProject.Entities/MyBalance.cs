using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Entities
{
    [Table("MyBalance")]
    public partial class MyBalance
    {

        [Key]
        public int Id { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public int OrderId { get; set; }
        public string Notes { get; set; }


    }
}
