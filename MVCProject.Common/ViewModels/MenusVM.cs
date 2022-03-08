using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Common.ViewModels
{
    public class MenusVM : BaseVM
    {
        public int Id { get; set; }
        [Display(Name = "Menü Adı")]
        [Required]
        public string MenuAdi { get; set; }
        [Display(Name = "İkon")]
        [Required]
        public string Icon { get; set; }
        [Display(Name = "Url")]
        [Required]
        public string Url { get; set; }
        public int RoleId { get; set; }

        public List<SubMenusVM> SubMenus { get; set; }
    }
}
