using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Identity
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]

        public string Username { get; set; }
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [RegularExpression("(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,15})$", ErrorMessage = "Password must be at least 8 characters, have atleast 1 letter and atleast 1 digit ")]
        public string Password { get; set; }
    }
}
