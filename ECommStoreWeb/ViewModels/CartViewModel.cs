using ECommStoreWeb.Models;
using System.ComponentModel.DataAnnotations;

namespace ECommStoreWeb.ViewModels
{
    public class CartViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        public string ProductName { get; set; }

        public string? ProductPhotoLink { get; set; }

        public string ProductCategory { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a valid price.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Stock quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a valid quantity.")]
        public int StockQuantity { get; set; }
        public int CartQuantity { get; set; }


        public CartViewModel() { }
        public CartViewModel(int id,Product product, int cartQuantity)
        {
            Id = id;
            ProductId=product.ProductId;
            ProductName=product.ProductName;
            ProductPhotoLink=product.ProductPhotoLink;
            ProductCategory=product.ProductCategory;
            Description=product.Description;
            Price=product.Price;
            StockQuantity=product.StockQuantity;
            CartQuantity=cartQuantity;
        }
    }
}
