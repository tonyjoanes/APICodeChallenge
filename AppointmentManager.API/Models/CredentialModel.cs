using System.ComponentModel.DataAnnotations;

namespace AppointmentManager.API.Models
{
    /// <summary>
    /// Credential Model
    /// </summary>
    public class CredentialModel
    {
        /// <summary>
        /// Gets or sets the UserName
        /// </summary>
        [Required]
        public string UserName { get; set; }
        
        /// <summary>
        /// Gets or sets the Password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}