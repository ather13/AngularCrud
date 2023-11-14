using System.ComponentModel.DataAnnotations;

namespace CoreCrudApi.Models
{
    public class AuthRequestDto
    {
        [Required(ErrorMessage = "User name is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
