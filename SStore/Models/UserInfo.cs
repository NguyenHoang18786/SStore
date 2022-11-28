using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SStore.Models
{
    public class UserInfo
    {
        [Key]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        [Required]
        [StringLength(255)]
        public string Email { get; set; }
        [StringLength(100)]
        public string Country { get; set; }
        [StringLength(100)]
        public string City { get; set; }
        [StringLength(255)]
        public string Address { get; set; }
        [StringLength(100)]
        public string Phone { get; set; }
        public Nullable<DateTime> RegisterDate { get; set; }
    }
    public enum Gender
    {
        Male,
        Female
    }
}