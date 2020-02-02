using AppointmentManager.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace AppointmentManager.Data.Repositories
{
    /// <summary>
    /// Appointment Repository
    /// </summary>
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppDbContext dbContext;

        /// <summary>
        /// Initialise and instance of the AppointmentRepository
        /// </summary>
        /// <param name="dbContext"></param>
        public AppointmentRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Appointment Exists
        /// </summary>
        /// <param name="appointmentDate"></param>
        /// <returns></returns>
        public bool AppointmentExists(string patientId, DateTimeOffset appointmentDate)
        {
            var appointment = dbContext
                .Appointments
                .FirstOrDefault(x => x.Date.Date == appointmentDate.Date 
                    && x.PatientId == patientId
                    && !x.IsDeleted);

            return appointment == null
                ? false
                : true;
        }

        /// <summary>
        /// Gets an Appointment by PatientId and Appointment Date
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="appointmentDate"></param>
        /// <returns></returns>
        public Appointment GetAppointment(string patientId, DateTimeOffset appointmentDate)
        {
            return dbContext
                .Appointments
                .FirstOrDefault(x => x.Date.Date == appointmentDate.Date 
                    && x.PatientId == patientId 
                    && !x.IsDeleted);
        }

        /// <summary>
        /// Create an Appointment
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="equipment"></param>
        /// <param name="appointmentDate"></param>
        public void CreateAppointment(string patientId, Equipment equipment, DateTimeOffset appointmentDate)
        {
            var newAppointment = new Appointment
            {
                EquipmentId = equipment.Id,
                Date = appointmentDate,
                PatientId = patientId,
                IsDeleted = false
            };

            dbContext.Appointments.Add(newAppointment);
        }

        /// <summary>
        /// Cancel an Appointment
        /// </summary>
        /// <param name="appointment"></param>
        public void CancelAppointment(Appointment appointment)
        {
            appointment.IsDeleted = true;
        }
    }
}
