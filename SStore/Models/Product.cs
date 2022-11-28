using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SStore.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string ProductName { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }
        [Required]
        public decimal Price { get; set; }
        public Nullable<double> Weight { get; set; }
        [StringLength(20)]
        public string Size { get; set; }
        [StringLength(100)]
        public string Color { get; set; }
        [Required]
        public int BrandId { get; set; }
        public ProductBrand productBrand { get; set; }
        public bool Status { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        [StringLength(255)]
        public string Image { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Nullable<bool> Hot { get; set; }
        public Nullable<int> View { get; set; }

    }
}