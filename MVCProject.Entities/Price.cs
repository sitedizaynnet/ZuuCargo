using System;
using System.ComponentModel.DataAnnotations;

namespace MVCProject.Entities
{
    public partial class Price
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public Nullable<double> FromWeight { get; set; }
        public Nullable<double> ToWeight { get; set; }
        public Nullable<short> Zone { get; set; }
        public Nullable<int> ServicePrice { get; set; }
        public bool IsExpress { get; set; }

    }
}
