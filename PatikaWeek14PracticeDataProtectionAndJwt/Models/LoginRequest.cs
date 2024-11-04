using System.ComponentModel.DataAnnotations;

namespace PatikaWeek14PracticeDataProtectionAndJwt.Models
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
