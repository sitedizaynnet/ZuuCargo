using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Common.ViewModels
{
    public partial class PriceVM : BaseVM
    {
        [Key]
        public int Id { get; set; }

        public string Type { get; set; }
        public Nullable<double> FromDesi { get; set; }
        public Nullable<double> ToDesi { get; set; }
        public Nullable<short> Zone { get; set; }
        public Nullable<int> ServicePrice { get; set; }
        public bool IsExpress { get; set; }

    }
}
