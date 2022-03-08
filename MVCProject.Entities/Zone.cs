using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Entities
{
    public partial class Zone
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public Nullable<short> ZoneNo { get; set; }
        public string Time { get; set; }
        public bool IsExpress { get; set; }

    }
}
