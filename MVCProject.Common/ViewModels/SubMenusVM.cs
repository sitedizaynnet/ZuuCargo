using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Common.ViewModels
{
    public class SubMenusVM : BaseVM
    {
        public int Id { get; set; }
        [Display(Name = "Menü Seçiniz")]
        public int MenusId { get; set; }
        [Display(Name = "Alt Menü Adı")]
        public string SubMenuAdi { get; set; }
        [Display(Name = "Url")]
        public string Url { get; set; }
        [Display(Name = "Icon")]
        public string Icon { get; set; }
        public int RoleId { get; set; }

    }
}
