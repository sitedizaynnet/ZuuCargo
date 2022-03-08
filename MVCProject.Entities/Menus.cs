using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Models
{
    public partial class Menus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Menus()
        {
            this.SubMenus = new HashSet<SubMenus>();
        }

        [Key]
        public int Id { get; set; }
        public string MenuAdi { get; set; }
        public string Icon { get; set; }
        public string Url  { get; set; }
        public int RoleId { get; set; }

        public virtual ICollection<SubMenus> SubMenus { get; set; }
    }
}
