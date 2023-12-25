using System.ComponentModel.DataAnnotations;

namespace ECommStoreWeb.IdentityModels
{
    public class Login
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [DataType(DataType.Password)]
        public string Password { get; set; }


        public bool Remember { get; set; }
    }
}
