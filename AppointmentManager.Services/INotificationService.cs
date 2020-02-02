using System;

namespace AppointmentManager.Services
{
    public interface INotificationService
    {
        void SendConfirmationEmail(string patientId, string email, DateTimeOffset appointmentDate);
    }
}