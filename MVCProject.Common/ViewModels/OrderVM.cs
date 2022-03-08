using MVCProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Common.ViewModels
{

    public partial class OrderVM:BaseVM
    {
        public OrderVM()
        {
         

        }
        public int Id { get; set; }

        [Display(Name = "Detaylar")]

        public string Answer { get; set; }

        [Display(Name = "İlan Tarihi")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy hh:mm}")]
        public DateTime? OrderTime { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy hh:mm}")]

        public DateTime? BitisTarihi { get; set; }

       

        public int WinnerBid { get; set; }


       
        public string UsersId { get; set; }
        public string UserName { get; set; }
        [Display(Name = "Kategori")]
        public string SubCatName { get; set; }

        [Display(Name = "Şehir")]
        public short CityId { get; set; }


        [Display(Name = "İlçe")]
        public short IlceId { get; set; }


        [Display(Name = "Semt")]
        public int SemtId { get; set; }

        [Display(Name = "Mahalle")]
        public int MahalleId { get; set; }

        public string Status { get; set; }

        [Display(Name = "Şehir")]
        public string CityAdi { get; set; }
        [Display(Name = "İlçe")]
        public string IlceAdi { get; set; }
        [Display(Name = "Semt")]
        public string SemtAdi { get; set; }
        [Display(Name = "Mahalle")]
        public string MahalleAdi { get; set; }


      
        public string KalanSure { get; set; }

        public int WinningPrice { get; set; }


    }

    public partial class SelectBidVM
    {

        public int OrderId { get; set; }
        public int BidId { get; set; }

        public int ProviderId { get; set; }
        public string ProviderName { get; set; }
        public string BidComment { get; set; }
        public string AuthorizedPersonnelName { get; set; }
        public string Phone { get; set; }
        [Display(Name = "Şehir")]
        public string CityAdi { get; set; }
        [Display(Name = "İlçe")]
        public string IlceAdi { get; set; }
        [Display(Name = "Semt")]
        public string SemtAdi { get; set; }
        [Display(Name = "Mahalle")]
        public string MahalleAdi { get; set; }
        public int Price { get; set; }
        public string ImagePath { get; set; }
        public int AvarageScore { get; set; }

    }
    public partial class OrderDetailsVM
    {
        public int WinningPrice { get; set; }
        public string UsersId { get; set; }
        public string UserName { get; set; }
        public string Answer { get; set; }
        public DateTime? BitisTarihi { get; set; }
        public string SubCatName { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }

}
