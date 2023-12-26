using System.ComponentModel.DataAnnotations;

namespace ECommStoreWeb.Models
{
	public class Order
	{
        [Key]
        public Guid OrderId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public DateTime ExpectedDeleveryDate { get; set; } = DateTime.Now.AddDays(7);

        public bool Placed { get; set; }

        public bool Packed { get; set; }

        public bool Shipped { get; set; }

        public bool OutForDelevery { get; set; }
    }
}
