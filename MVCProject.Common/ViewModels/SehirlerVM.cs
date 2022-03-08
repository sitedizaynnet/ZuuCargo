using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MVCProject.Common.ViewModels

{

    public partial class SehirlerVM:BaseVM
    {
        public SehirlerVM()
        {
            IlcelerVM = new HashSet<IlcelerVM>();
        }


        public Int16 SehirId { get; set; }

        [Required]
        [StringLength(20)]
        public string SehirAdi { get; set; }



        public virtual ICollection<IlcelerVM> IlcelerVM { get; set; }
    }
}
