using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SStore.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public Status Status { get; set; }
        [ForeignKey("UserInfo")]
        public string UserId { get; set; }
        public UserInfo UserInfo { get; set; }
        [StringLength(100)]
        public string PaymentType { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Name")]
        public string CustomerName { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Phone Number")]
        public string CustomerPhone { get; set; }
        [EmailAddress]
        [Display(Name = "Email")]
        public string CustomerEmail { get; set; }
        [Required]
        [Display(Name = "Address")]
        [StringLength(255)]
        public string DeliveryAddress { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public enum Status
    {
        Processing,
        Success,
        Denied
    }
    public enum PaymentStatus
    {
        Waiting,
        Paid,
        Denied
    }
}