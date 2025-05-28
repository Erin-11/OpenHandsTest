using System.ComponentModel.DataAnnotations;

namespace PropertySystem.Models
{
    public class Property
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string Region { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string District { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string PropertyType { get; set; } = string.Empty;
        
        [Required]
        public decimal SalePrice { get; set; }
        
        [StringLength(500)]
        public string ImageUrl { get; set; } = string.Empty;
        
        public int Bedrooms { get; set; }
        
        public int Bathrooms { get; set; }
        
        public double Area { get; set; }
        
        [StringLength(200)]
        public string Address { get; set; } = string.Empty;
        
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}