using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentManager.API.Models
{
    /// <summary>
    /// AppoinmentModel
    /// </summary>
    public class AppointmentModel
    {
        /// <summary>
        /// Gets or sets the PatientId
        /// </summary>
        [Required]
        public string PatientId { get; set; }

        /// <summary>
        /// Gets or sets the AppointmentDate
        /// </summary>
        [Required]
        public DateTimeOffset AppointmentDate { get; set; }
    }
}
