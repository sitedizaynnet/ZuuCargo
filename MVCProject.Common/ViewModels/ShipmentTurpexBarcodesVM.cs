using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Common.ViewModels
{

    public partial class ShipmentTurpexBarcodesVM : BaseVM
    {
        public ShipmentTurpexBarcodesVM()
        {
          
        }


        [Key]
        [Required]
        public Int16 Id { get; set; }

        [Required]

        public int ShipmentId { get; set; }
        public string Barcode { get; set; }


    }
}
