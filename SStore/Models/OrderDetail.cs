using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SStore.Models
{
    public class OrderDetail
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }
        [Key, Column(Order = 1)]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [Column(Order = 2)]
        public int Quantity { get; set; }
        [Column(Order = 3)]
        public decimal Price { get; set; }
    }
}