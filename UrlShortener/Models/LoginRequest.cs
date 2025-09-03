using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Models
{
    public class LoginRequest
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }

        public override string ToString()
        {
            return Username + ":" + Password;
        }
    }
}
