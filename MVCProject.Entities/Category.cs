using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Entities
{
    [Table("Category")]
    public partial class Category
    {
        //public Category()
        //{
        //    SubCategory = new HashSet<SubCategory>();
        //}

        [Key]
        [Required]
        public Int16 Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Title { get; set; }


    }
}
