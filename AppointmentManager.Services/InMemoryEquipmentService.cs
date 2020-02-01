using AppointmentManager.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppointmentManager.Services
{
    /// <summary>
    /// In memory version of the Equipment Service
    /// </summary>
    public class InMemoryEquipmentService : IEquipmentService
    {
        private Equipment equipment;

        // In real world we'd persist equipment bookings so we
        // had an audit trail and additional details but for this
        // stubbed service this will do just nicely
        private List<DateTime> equipmentBookings;

        /// <summary>
        /// Initialise an instance of the InMemoryEquipmentService
        /// </summary>
        public InMemoryEquipmentService()
        {
            // there is only one piece of equipment deployed
            // this is just an in memory equipment service
            equipment = new Equipment
            {
                Id = 1,
                Name = "MEDICAL_DEVICE_100",
                Status = EquipmentStatus.Available
            };

            equipmentBookings = new List<DateTime>();
        }

        /// <summary>
        /// Get Available Equipment
        /// </summary>
        /// <param name="appointmentDate"></param>
        /// <returns></returns>
        public Equipment GetAvailableEquipment(DateTimeOffset appointmentDate)
        {
            return equipmentBookings.IndexOf(appointmentDate.Date) == -1
                ? equipment
                : null;
        }

        public void SetEquipmentAvailable(DateTimeOffset appointmentDate)
        {
            var removeBooking = equipmentBookings.Single(x => x == appointmentDate.Date);
            equipmentBookings.Remove(removeBooking);
        }

        public void SetEquipmentAvailable(int id)
        {
            equipment.Status = EquipmentStatus.Available;
        }

        public void SetEquipmentUnavailable(DateTimeOffset appointmentDate)
        {
            equipmentBookings.Add(appointmentDate.Date);
        }
    }
}
