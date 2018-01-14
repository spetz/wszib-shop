using Shop.Core.DTO;
using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.Web.Models
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name was not provided.")]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        [Range(1, 100000)]
        public decimal Price { get; set; }

        public ProductViewModel()
        {
        }

        public ProductViewModel(ProductDto dto)
        {
            Id = dto.Id;
            Name = dto.Name;
            Category = dto.Category;
            Price = dto.Price;
        }
    }
}
