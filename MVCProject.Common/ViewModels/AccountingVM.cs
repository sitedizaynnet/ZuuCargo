using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Common.ViewModels
{
    public partial class AccountingVM:BaseVM
    {
        [Key]
        public int Id { get; set; }
        public  string  CustomerName { get; set; }
        public string  Telephone { get; set; }
        public string City { get; set; }
        public Nullable<double> Balance { get; set; }
        public string ProductName { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<double> TotalDollar { get; set; }
        public Nullable<double> TotalLira { get; set; }
        public Nullable<double> CargoPrice { get; set; }
        public Nullable<double> Exchange { get; set; }
        public Nullable<double> Expensive { get; set; }
        public Nullable<double> Tax { get; set; }
        public Nullable<bool> IsPaid { get; set; }

        public Nullable<DateTime> Date { get; set; }
        public Nullable<int> InvoiceNo { get; set; }
        public Nullable<bool> IsCargo { get; set; }
        public Nullable<bool> IsDelivered { get; set; }

        public string Notes { get; set; }
        public string InvoiceLink1 { get; set; }
        public string InvoiceLink2 { get; set; }
        public string InvoiceLink3 { get; set; }
        public string InvoiceLink4 { get; set; }
        public string InvoiceLink5 { get; set; }

        public Nullable<bool> IsSendSMSAfterSave { get; set; }



    }
}
