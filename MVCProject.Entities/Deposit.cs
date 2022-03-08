using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Entities
{
    [Table("Deposit")]
    public partial class Deposit
    {

        [Key]
        public int Id { get; set; }

        public string CustomerId { get; set; }
        public double DepositTotal { get; set; }
        public string Notes { get; set; }

    }
}
