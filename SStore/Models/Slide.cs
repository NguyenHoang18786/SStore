using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SStore.Models
{
    public class Slide
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        [Required]
        [StringLength(255)]
        public string Image { get; set; }
    }
}