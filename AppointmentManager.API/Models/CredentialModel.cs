using System.ComponentModel.DataAnnotations;

namespace AppointmentManager.API.Models
{
    public class CredentialModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}