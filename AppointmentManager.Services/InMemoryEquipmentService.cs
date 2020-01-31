using AppointmentManager.Data.Entities;
using System;

namespace AppointmentManager.Services
{
    public class InMemoryEquipmentService : IEquipmentService
    {
        private Equipment equipment;

        public InMemoryEquipmentService()
        {
            // there is only one piece of equipment deployed
            // this is just an in memory equipment service
            equipment = new Equipment
            {
                Id = 1,
                Name = "MEDICAL_DEVICE_100",
                Status = Status.Available
            };
        }
        public Equipment GetAvailableEquipment(DateTimeOffset appointmentDate)
        {
            // todo: Should check equipment isn't being used in an
            // existing appointment
            return equipment.Status == Status.Available
                ? equipment
                : null;
        }

        public void SetEquipmentAvailable(Equipment equipment)
        {
            equipment.Status = Status.Available;
        }

        public void SetEquipmentUnavailable(Equipment equipment)
        {
            equipment.Status = Status.Unavailable;
        }
    }
}
