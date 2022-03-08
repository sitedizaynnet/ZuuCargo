using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Common.ViewModels
{

    public partial class CategoryVM : BaseVM
    {
        public CategoryVM()
        {
          
        }

      
        public Int16 Id { get; set; }

        public string Title { get; set; }

     
    }
}
