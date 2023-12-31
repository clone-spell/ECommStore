using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ECommStoreWeb.ViewModels
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Product name is required.")]
        public string ProductName { get; set; }

        public string? ProductPhotoLink { get; set; }

        public IFormFile? ProductPhoto { get; set; }

        public string ProductCategory { get; set; }
        public IEnumerable<SelectListItem>? ProductCategoryList { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a valid price.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Stock quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a valid quantity.")]
        public int StockQuantity { get; set; }
    }
}
