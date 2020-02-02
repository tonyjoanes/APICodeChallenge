using System;
using System.ComponentModel.DataAnnotations;

namespace AppointmentManager.API.Models
{
    /// <summary>
    /// Appointment change model
    /// </summary>
    public class AppointmentChangeModel
    {
        /// <summary>
        /// Gets or sets the PatientId
        /// </summary>
        [Required]
        public string PatientId { get; set; }
        
        /// <summary>
        /// Gets or sets the CurrentAppointmentDate
        /// </summary>
        [Required]
        public DateTimeOffset CurrentAppointmentDate { get; set; }
        
        /// <summary>
        /// Gets or sets the NewAppointmentDate
        /// </summary>
        [Required]
        public DateTimeOffset NewAppointmentDate { get; set; }
    }
}
