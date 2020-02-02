using System;

namespace AppointmentManager.Services
{
    public class NotificationService : INotificationService
    {
        public void SendConfirmationEmail(string patientId, string email, DateTimeOffset appointmentDate)
        {
            /// Email logic
        }
    }
}
