using AppointmentManager.Data.Entities;
using System;

namespace AppointmentManager.Services
{
    /// <summary>
    /// Appointment Service Interface
    /// </summary>
    public interface IAppointmentService
    {
        void Create(string patientId, DateTimeOffset appointmentDate);
        void Cancel(string patientId, DateTimeOffset appointmentDate);
        void Change(string patientId, DateTimeOffset appointmentDate, DateTimeOffset newAppointmentDate);
    }
}