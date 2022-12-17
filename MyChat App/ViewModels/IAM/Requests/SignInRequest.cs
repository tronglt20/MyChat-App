using System.ComponentModel.DataAnnotations;

namespace MyChat_App.ViewModels.IAM.Requests
{
    public class SignInRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
