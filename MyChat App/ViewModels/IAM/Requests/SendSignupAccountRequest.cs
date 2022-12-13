using System.ComponentModel.DataAnnotations;

namespace MyChat_App.ViewModels.IAM.Requests
{
    public class SendSignupAccountRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
