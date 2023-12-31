
using System.ComponentModel.DataAnnotations;

namespace ECommStoreWeb.Models
{
    public class SubCategory
    {
        [Key]
        public int Id { get; set; }

        public int Category { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be in between 3 to 100 chaecters")]
        public string Name { get; set; }

        public string Image { get; set; }
    }
}
