using AppointmentManager.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManager.Data.Repositories
{
    public interface IAppointmentRepository
    {
        void CreateAppointment(string patientId, Equipment equipment, DateTimeOffset apppointmentDate);
        bool AppointmentExists(DateTimeOffset appointmentDate);
        Appointment GetAppointment(string patientId, DateTimeOffset appointmentDate);
    }
}
