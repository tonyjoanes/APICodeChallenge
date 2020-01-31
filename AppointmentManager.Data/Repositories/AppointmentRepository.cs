using AppointmentManager.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace AppointmentManager.Data.Repositories
{
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

        public bool AppointmentExists(DateTimeOffset appointmentDate)
        {
            var appointment = dbContext
                .Appointments
                .FirstOrDefault(x => x.Date.Date == appointmentDate.Date 
                    && !x.IsDeleted);

            return appointment == null
                ? false
                : true;
        }

        public Appointment GetAppointment(string patientId, DateTimeOffset appointmentDate)
        {
            return dbContext
                .Appointments
                .FirstOrDefault(x => x.Date.Date == appointmentDate.Date 
                    && x.PatientId == patientId 
                    && !x.IsDeleted);
        }

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
    }
}
