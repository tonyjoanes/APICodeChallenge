using System;

namespace AppointmentManager.Services
{
    public interface IAppointmentService
    {
        void Create(string patientId, DateTimeOffset appointmentDate);
        void Cancel(string patientId, DateTimeOffset appointmentDate);
    }
}