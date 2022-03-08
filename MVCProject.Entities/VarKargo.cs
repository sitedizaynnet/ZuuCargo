using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Entities
{
    [Table("ZuuCargo")]
    public partial class ZuuCargo
    {

        [Key]
        public int Id { get; set; }

        public string CustomerAccountId { get; set; }
        public string StockName { get; set; }
        public string CustomersName { get; set; }
     
        public Nullable<int> NumberOfContainer { get; set; }
        public Nullable<int> Kg { get; set; }
        public Nullable<int> Pcs { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<DateTime> Date { get; set; }
        public Nullable<double> Debt { get; set; }


        public string Notes { get; set; }

    }
}
