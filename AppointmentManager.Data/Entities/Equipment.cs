using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentManager.Data.Entities
{
    /// <summary>
    /// Equipment Entity
    /// </summary>
    public class Equipment
    {
        /// <summary>
        /// Gets or sets the Equipment Identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Equipment Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Equipment Status
        /// </summary>
        public EquipmentStatus Status { get; set; }
    }
}
