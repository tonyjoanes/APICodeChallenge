using AppointmentManager.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppointmentManager.Data.Repositories
{
    public interface IAppointmentRepository
    {
        void CreateAppointment(string patientId, Equipment equipment, DateTimeOffset apppointmentDate);
        bool AppointmentExists(DateTimeOffset appointmentDate);
    }

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
                .FirstOrDefault(x => x.Date.Date == appointmentDate.Date);

            return appointment == null
                ? false
                : true;
        }

        public void CreateAppointment(string patientId, Equipment equipment, DateTimeOffset appointmentDate)
        {
            var newAppointment = new Appointment
            {
                EquipmentId = equipment.Id,
                Date = appointmentDate,
                PatientId = patientId,
            };

            dbContext.Appointments.AddAsync(newAppointment);
        }
    }
}
