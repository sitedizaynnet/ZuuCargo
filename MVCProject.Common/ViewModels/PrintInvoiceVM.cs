using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Common.ViewModels
{
   public  class PrintInvoiceVM : BaseVM
    {
        public AccountingVM AccountInfo { get; set; }
        public List<AccountingProductsVM> AccountProducts { get; set; }

    }
}
