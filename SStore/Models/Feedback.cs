using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SStore.Models
{
    public class Feedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(150, ErrorMessage = "Please input title with less than 150 characters")]
        [Required(ErrorMessage = "Please input title")]
        public string Title { get; set; }
        [StringLength(550, ErrorMessage = "Please describe with less than 550 characters")]
        [Required(ErrorMessage = "Please input description")]
        public string Description { get; set; }
        [StringLength(255)]
        public string FullName { get; set; }
        [StringLength(255)]
        public string Email { get; set; }
        [StringLength(100)]
        public string PhoneNumber { get; set; }
        public DateTime CreateDate { get; set; }
    }
}