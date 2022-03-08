using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Common.ViewModels
{
    public class CostsReportVM : BaseVM
    {
        public string Category { get; set; }
        public double TotalOrderTotal { get; set; }
        public double TotalExpensive { get; set; }
        public double OrderTotalTurkey { get; set; }
        public double OrderTotalChina { get; set; }
        public double OrderTotalUSA { get; set; }
        public double OrderTotalPost { get; set; }
        public double OrderTotalCargo { get; set; }
        public double OrderTotalTicket { get; set; }
        public double OrderTotalExpensive { get; set; }
        public double OrderTotalExchange { get; set; }





    }
}
