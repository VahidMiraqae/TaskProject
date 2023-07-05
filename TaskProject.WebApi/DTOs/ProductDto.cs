using System.ComponentModel.DataAnnotations;

namespace TaskProject.WebApi.DTOs
{
    public class ProductDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public DateTime ProductDate { get; set; }

        [Required]
        [StringLength(11)]
        public string ManufacturePhone { get; set; }

        [Required]
        [StringLength(50)]
        public string ManufactureEmail { get; set; }

        [Required]
        public bool IsAvailable { get; set; }
    }
}
