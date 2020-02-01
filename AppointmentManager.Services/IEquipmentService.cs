using AppointmentManager.Data.Entities;
using System;

namespace AppointmentManager.Services
{
    /// <summary>
    /// Equipment Service Interface
    /// </summary>
    public interface IEquipmentService
    {
        Equipment GetAvailableEquipment(DateTimeOffset appointmentDate);
        void SetEquipmentUnavailable(DateTimeOffset appointmentDate);
        void SetEquipmentAvailable(DateTimeOffset appointmentDate);
    }
}