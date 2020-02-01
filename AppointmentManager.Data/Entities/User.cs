using Microsoft.AspNetCore.Identity;
namespace AppointmentManager.Data.Entities
{
    /// <summary>
    /// Custom Identity User
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        /// Gets or sets the PatientId
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// Gets or sets the FirstName
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the LastName
        /// </summary>
        public string LastName { get; set; }
    }
}
