using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Entities
{
    [Table("ShipmentTurpexBarcodes")]
    public partial class ShipmentTurpexBarcodes
    {
        //public Category()
        //{
        //    SubCategory = new HashSet<SubCategory>();
        //}

        [Key]
        [Required]
        public Int16 Id { get; set; }

        [Required]
  
        public int  ShipmentId { get; set; }
        public string Barcode { get; set; }


    }
}
