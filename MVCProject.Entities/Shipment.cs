using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Entities
{
    public partial class Shipment
    {
        public int Id { get; set; } //invoceNo //salesCode
        public string TrackingNo { get; set; } //barcode
        public string SenderName { get; set; }  //senderName
        public Nullable<int> SenderIdVATNumber { get; set; }
        public string SenderTelephone { get; set; }
        public string SenderEmail { get; set; } //senderMail
        public string SenderAddress { get; set; } //senderAdres

        public Nullable<int> SenderPostalCode { get; set; }
        public string SenderCountry { get; set; }
        public Nullable<short> ReceiversAccountNo { get; set; }
        public Nullable<short> ReceiversVatNo { get; set; }
        public string ReceiverName { get; set; } //receiverName
        public string ReceiverAddress { get; set; } //receiverAdres
        public string ReceiverAddress2 { get; set; } //receiverAdres2

        public string ReceiverCity { get; set; }

        public string ReceiverTelephoneNo { get; set; } //receiverPhone receiverSMS
        public string ReceiverEmail { get; set; } //receiverMail
        public string ReceiverPostalCode { get; set; } //receiverPostCode
        public Nullable<bool> IsResidental { get; set; }
        public string ReceiverCountry { get; set; }
      
        public Nullable<short> PackageCount { get; set; }
        public Nullable<short> TotalWeight { get; set; }
        public Nullable<short> DimensionalWeight { get; set; }
        public Nullable<bool> IsSameSize { get; set; }
     
        public string SpecialInstructions { get; set; }
        public Nullable<System.DateTime> ShipmentDate { get; set; }
        public string Status { get; set; }

        public Nullable<int> StatusId { get; set; }
        public string ExternalTrackingCode { get; set; }

        public bool IsPtt { get; set; }
        public bool IsDhl { get; set; }

        public double TotalDolar { get; set; }
        public string Notes { get; set; }
        public double Desi { get; set; } //desi
        public int Height { get; set; } //height
        public int Lenght { get; set; }//lenght
        public int Weight { get; set; } //
        public int Width { get; set; } //

        public string PaymentMethod { get; set; } //paymentmethod
        public string ReceiverCountryId { get; set; } //receiverCountryId 
        public string ReceiverMail { get; set; } //ReceiverMail
        public string PostType { get; set; }

        public string SenderTCNo { get; set; }
        public string Konsimento { get; set; }

        public string Acente { get; set; }
        public string Content { get; set; }
        public double ValueOfPackage { get; set; }
        public string ReceiverCityId { get; set; }
        public int HarmonyCode { get; set; }
        public double MoneyForBuy { get; set; }
        public bool IsDelivered { get; set; }

        public int StatusCounter { get; set; }
        public int IsApiSuccess { get; set; }
    }
}
     

        
         
        

        
        
         