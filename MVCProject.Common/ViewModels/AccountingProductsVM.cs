using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Common.ViewModels
{
    public partial class AccountingProductsVM : BaseVM
    {
        public int Id { get; set; }
        public Nullable<int> AccountingID { get; set; }
        public string ProductName { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<double> PriceDollar { get; set; }
        public Nullable<double> PriceLira { get; set; }
        public Nullable<double> Tax { get; set; }
        public Nullable<DateTime> Date { get; set; }
        public Nullable<double> Cargo { get; set; }


    }
}
