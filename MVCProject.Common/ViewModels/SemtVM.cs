using MVCProject.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Common.ViewModels
{
    public partial class SemtVM : BaseVM
    {
        public int SemtId { get; set; }
        public Int16 IlceId { get; set; }

        [Required]
        [StringLength(32)]
        [Display(Name = "Semt")]
        public string SemtAdi { get; set; }

    }
}
