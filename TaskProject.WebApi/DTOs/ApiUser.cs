using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace TaskProject.WebApi.DTOs
{
    public class ApiUser
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}