using MVCProject.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Models
{


    [Table("Comment")]
    public partial class Comment
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public int OrderId { get; set; }

        public int ProviderId { get; set; }
        [Column("Comment")]
        [Required]
        [StringLength(350)]
        public string Comment_Text { get; set; }

        public Int16 Rate1 { get; set; }
        public Int16 Rate2 { get; set; }
        public Int16 Rate3 { get; set; }
        public Int16 Rate4 { get; set; }


    }
}
