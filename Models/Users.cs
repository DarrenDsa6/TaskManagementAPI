using System.ComponentModel.DataAnnotations;

namespace TaskManagementAPI.Models
{
    public class Users
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password confirmation is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        [StringLength(100, ErrorMessage = "Full name can't exceed 100 characters")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Role is required")]
        [StringLength(50, ErrorMessage = "Role cannot exceed 50 characters")]
        public string Role { get; set; }

    }
}
