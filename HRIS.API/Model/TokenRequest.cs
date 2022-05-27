using System.ComponentModel.DataAnnotations;

namespace HRIS.API.Model
{
    public class TokenRequest
    {
        [Required]
        public string Token { get; set; }

         [Required]
        public string RefreshToken { get; set; }
    }
}