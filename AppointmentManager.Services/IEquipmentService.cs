using AppointmentManager.Data.Entities;
using System;

namespace AppointmentManager.Services
{
    public interface IEquipmentService
    {
        Equipment GetAvailableEquipment(DateTimeOffset appointmentDate);
        void SetEquipmentUnavailable(Equipment equipment);
        void SetEquipmentAvailable(Equipment equipment);
    }
}