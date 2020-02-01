using System;

namespace AppointmentManager.Data.Entities
{
    /// <summary>
    /// Equipment Entity
    /// </summary>
    public class Appointment
    {
        /// <summary>
        /// Gets or Sets the Appointment Identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Appointment Date
        /// </summary>
        public DateTimeOffset Date { get; set; }

        /// <summary>
        /// Gets or sets the PatientId
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// Gets or sets the EquipmentId
        /// </summary>
        public int EquipmentId { get; set; }

        /// <summary>
        /// Gets or sets the IsDeleted flag
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
