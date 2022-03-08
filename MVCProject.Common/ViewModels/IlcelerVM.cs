using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Common.ViewModels
{
  
    public partial class IlcelerVM:BaseVM
    {
        public IlcelerVM()
        {
            SemtVM = new HashSet<SemtVM>();
        }

       
        public Int16 IlceId { get; set; }

        public Int16 SehirId { get; set; }


        public string IlceAdi { get; set; }


        public virtual SehirlerVM SehirlerVM { get; set; }

        public virtual ICollection<SemtVM> SemtVM { get; set; }
    }
}
