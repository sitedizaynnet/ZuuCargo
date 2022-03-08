using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Common.ViewModels
{
    public class JsonAnswer : BaseVM
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class JsonSearchCriteria : BaseVM
    {
        public string name { get; set; }
        public string value { get; set; }


    }
}
