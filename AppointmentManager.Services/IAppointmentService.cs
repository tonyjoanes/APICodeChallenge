using AppointmentManager.Data.Entities;
using System;
using System.Collections.Generic;

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

        /// <summary>
        /// Get a list of Appointments by date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        IEnumerable<Appointment> GetByDate(DateTime date);
    }
}