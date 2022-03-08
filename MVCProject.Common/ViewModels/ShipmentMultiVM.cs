using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Common.ViewModels
{
    public partial class ShipmentMultiVM :BaseVM
    {
        public ShipmentVM ShipmentVM { get; set; }
        public List<StatusUpdatesVM> StatusUpdatesVMList { get; set; }

       
    }
}
