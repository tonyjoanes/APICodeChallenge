using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentManager.Data.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public string PatientId { get; set; }
        public int EquipmentId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
