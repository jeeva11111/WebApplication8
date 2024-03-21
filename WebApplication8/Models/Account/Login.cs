using System.ComponentModel.DataAnnotations;

namespace WebApplication8.Models.Account
{
    public class Login
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

    }

    public class Register
    {
        

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        [Phone]
        public string? PhoneNum { get; set; }

    }
}
