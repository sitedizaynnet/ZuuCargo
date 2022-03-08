﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Common.ViewModels
{
    public partial class StatusUpdatesVM : BaseVM
    {
        [Key]
        public int Id { get; set; }
        public Nullable<System.DateTime> UpdatedDateTime { get; set; }
        public string Status { get; set; }
        public Nullable<int> ShipmentId { get; set; }

        public bool IsSendSMS { get; set; }
        public string SmsText { get; set; }
        public Nullable<int> StatusId { get; set; }
        public string Location { get; set; }

    }
}