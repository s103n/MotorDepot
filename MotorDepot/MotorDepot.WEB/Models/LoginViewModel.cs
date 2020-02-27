using System.ComponentModel.DataAnnotations;

namespace MotorDepot.WEB.Models
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(30, MinimumLength = 2)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}