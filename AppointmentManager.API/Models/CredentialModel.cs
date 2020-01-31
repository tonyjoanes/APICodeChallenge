using System.ComponentModel.DataAnnotations;

namespace AppointmentManager.API.Models
{
    public class CredentialModel
    {
        [Required]
        public string UserName { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}