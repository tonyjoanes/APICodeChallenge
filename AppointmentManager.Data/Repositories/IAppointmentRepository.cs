using AppointmentManager.Data.Entities;
using System;
namespace AppointmentManager.Data.Repositories
{
    /// <summary>
    /// Appointment Repository Interface
    /// </summary>
    public interface IAppointmentRepository
    {
        /// <summary>
        /// Create an Appointment
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="equipment"></param>
        /// <param name="appointmentDate"></param>
        void CreateAppointment(string patientId, Equipment equipment, DateTimeOffset apppointmentDate);

        /// <summary>
        /// Cancel an Appointment
        /// </summary>
        /// <param name="appointment"></param>
        void CancelAppointment(Appointment appointment);

        /// <summary>
        /// Appointment Exists
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="appointmentDate"></param>
        /// <returns></returns>
        bool AppointmentExists(string PatientId, DateTimeOffset appointmentDate);

        /// <summary>
        /// Gets an Appointment by PatientId and Appointment Date
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="appointmentDate"></param>
        /// <returns></returns>
        Appointment GetAppointment(string patientId, DateTimeOffset appointmentDate);
    }
}
