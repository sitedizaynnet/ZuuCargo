using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Entities
{
    [Table("Reklam")]
    public partial class Reklam
    {

        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string  Text { get; set; }
        public string Image { get; set; }

    }
}
