using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Entities
{
    public partial class GpsData
    {
        [Key]
        public int Id { get; set; }


        public string DeviceId { get; set; }


        public string DeviceIMEI { get; set; }


        public DateTime Date { get; set; }


        public string LocationMark { get; set; }


        public string Latitude { get; set; }


        public string DirectionLat { get; set; }


        public string Longitude { get; set; }


        public string DirectionLong { get; set; }

        public string Speed { get; set; }


        public string UTCDate { get; set; }


        public string Direction { get; set; }

        public string State { get; set; }

        public string Status { get; set; }
        public string Mileage { get; set; }

    }
}
