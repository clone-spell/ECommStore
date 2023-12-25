using System.ComponentModel.DataAnnotations;

namespace ECommStoreWeb.IdentityModels
{
    public class Register
    {
        [StringLength(100, MinimumLength =3, ErrorMessage ="Name must be in between 3 to 100 Charecters")]
        public string Name { get; set; }


        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [DataType(DataType.Password)]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

    }
}
