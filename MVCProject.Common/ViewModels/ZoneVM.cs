using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Common.ViewModels
{
    public partial class ZoneVM : BaseVM
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public Nullable<short> ZoneNo { get; set; }


    }
}
