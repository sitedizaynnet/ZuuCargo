using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Common.ViewModels
{
    public class AccountReportVM : BaseVM
    {
        public string  CustomerName { get; set; }
        public double TotalSalesDollar { get; set; }
        public double TotalSalesTL { get; set; }
        public double  TotalExpensive { get; set; }
        public double TotalExchange { get; set; }
        public double TotalCargo { get; set; }
        public int TotalQuantity { get; set; }
        public double Balance { get; set; }
        public int TotalProducts { get; set; }
        public double TotalRefundDolar { get; set; }
        public double TotalRefundLira{ get; set; }
        public int TotalRefundAdet { get; set; }
        public double TotalLostDolar { get; set; }
        public double TotalLostLira { get; set; }
        public int TotalLostAdet { get; set; }
        public double TotalDelivered { get; set; }

        public double TotalDOllar { get; set; }

        public double KamuBorcTurkey { get; set; }
        public double KamuBorcUK { get; set; }
        public double KamuBorcPost { get; set; }
        public double KamuBorcCargo { get; set; }



    }
}
