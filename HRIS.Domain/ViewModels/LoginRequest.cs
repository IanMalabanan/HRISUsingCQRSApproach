using System.ComponentModel.DataAnnotations;

namespace HRIS.Domain.ViewModels
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}