using System.ComponentModel.DataAnnotations;

namespace ConnectWise_Web.Models
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
