using System.ComponentModel.DataAnnotations; // For data annotations used in model validation

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required] // Indicates that this property is required, meaning it must be provided when creating a new user, this is data annotation that enforces validation rules
        public string DisplayName { get; set; } = ""; // Default value is an empty string, required for user registration

        [Required]
        [EmailAddress]

        public string Email { get; set; } = ""; // Default value is an empty string, required for user registration and must be a valid email format

        [Required]
        [MinLength(4)]
        public string Password { get; set; }
    }
}
