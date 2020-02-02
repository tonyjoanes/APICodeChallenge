using System;
using System.ComponentModel.DataAnnotations;

namespace AppointmentManager.API.Models
{
    public class AppointmentChangeModel
    {
        [Required]
        public string PatientId { get; set; }
        
        [Required]
        public DateTimeOffset CurrentAppointmentDate { get; set; }
        
        [Required]
        public DateTimeOffset NewAppointmentDate { get; set; }
    }
}
