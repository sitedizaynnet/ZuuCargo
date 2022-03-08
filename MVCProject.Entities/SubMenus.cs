using MVCProject.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Models
{
    public class SubMenus
    {
        [Key]
        public int Id { get; set; }
        public int MenusId { get; set; }
        public string SubMenuAdi { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }

        public virtual Menus Menus { get; set; }

    }
}
