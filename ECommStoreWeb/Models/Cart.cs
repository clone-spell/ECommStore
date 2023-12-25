using System.ComponentModel.DataAnnotations;

namespace ECommStoreWeb.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string UserId { get; set; }

        [Range(1, 10, ErrorMessage ="Product Quantity Should Be 1 to 10")]
        public int Quantity { get; set; }
    }
}
