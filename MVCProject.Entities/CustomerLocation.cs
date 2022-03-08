using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Entities
{
    public partial class CustomerLocation
    {
        [Key]
        public int Id { get; set; }


        public string CustomerName { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }


    }
}
