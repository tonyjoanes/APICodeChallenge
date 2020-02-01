using AppointmentManager.Common;
using AppointmentManager.Common.Validation;
using AppointmentManager.Data.Repositories;
using System;

namespace AppointmentManager.Services
{
    /// <summary>
    /// AppointmentService
    /// 
    /// Handles the management of Appointment Actions
    /// </summary>
    public class AppointmentService : IAppointmentService
    {
        private readonly IEquipmentService equipmentService;
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IDateTime dateTime;

        /// <summary>
        /// Initialises an instance of the AppointmentService
        /// </summary>
        /// <param name="equipmentService"></param>
        /// <param name="appointmentRepository"></param>
        /// <param name="dateTime"></param>
        public AppointmentService(IEquipmentService equipmentService,
                                  IAppointmentRepository appointmentRepository,
                                  IDateTime dateTime)
        {
            this.equipmentService = equipmentService;
            this.appointmentRepository = appointmentRepository;
            this.dateTime = dateTime;
        }

        /// <summary>
        /// Create a new Appointment for a patient
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="appointmentDate"></param>
        public void Create(string patientId, DateTimeOffset appointmentDate)
        {
            if (appointmentDate < dateTime.Now)
            {
                throw new ValidationException("Appointment date cannot be in the past");
            }

            if ((appointmentDate - dateTime.Now).Days > 14)
            {
                throw new ValidationException("Appointment date must not be later than two weeks from now");
            }

            if (appointmentRepository.AppointmentExists(appointmentDate))
            {
                throw new ValidationException("An appointment already exists for this date");
            }

            var availableEquipment = equipmentService.GetAvailableEquipment(appointmentDate);

            if (availableEquipment == null)
            {
                throw new ValidationException($"There is no available equipment on this date");
            }

            try
            {
                appointmentRepository.CreateAppointment(patientId, availableEquipment, appointmentDate);
                equipmentService.SetEquipmentUnavailable(appointmentDate);
            }
            catch (Exception ex)
            {
                throw new Exception($"There was an issue creating the appointment: {ex.Message}");
            }
        }

        /// <summary>
        /// Cancels an Appointment for a patient
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="appointmentDate"></param>
        public void Cancel(string patientId, DateTimeOffset appointmentDate)
        {
            var appointment = appointmentRepository.GetAppointment(patientId, appointmentDate);

            if (appointment == null)
            {
                throw new ValidationException("No appointment was found to cancel");
            }

            if ((appointmentDate - dateTime.Now).Days < 3)
            {
                throw new ValidationException("Cannot cancel appointment when its less than 3 days due");
            }

            try
            {
                appointmentRepository.CancelAppointment(appointment);
                equipmentService.SetEquipmentAvailable(appointmentDate);
            }
            catch (Exception)
            {
                throw new Exception("There was an issue cancelling the appointment");
            }
        }
    }
}
