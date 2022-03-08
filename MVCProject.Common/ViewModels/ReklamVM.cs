using System;
using System.ComponentModel.DataAnnotations;

namespace MVCProject.Common.ViewModels
{
    public partial class ReklamVM : BaseVM
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }

        public string Text { get; set; }
        public string Image { get; set; }

    }
}
