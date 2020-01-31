using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentManager.API.Models
{
    public class CreateAppointmentModel
    {
        [Required]
        public string PatientId { get; set; }

        [Required]
        public DateTimeOffset AppointmentDate { get; set; }
    }
}
