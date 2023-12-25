using System.ComponentModel.DataAnnotations;

namespace ECommStoreWeb.IdentityModels
{
    public class Profile
    {
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be in between 3 to 100 Charecters")]
        public string Name { get; set; }


        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string? PhotoLink { get; set; }
        public IFormFile? Photo { get; set; }
        public int? Age { get; set; }

        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }
    }
}
