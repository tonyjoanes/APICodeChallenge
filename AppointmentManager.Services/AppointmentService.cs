using AppointmentManager.Common;
using AppointmentManager.Common.Validation;
using AppointmentManager.Data.Entities;
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

            AssertTwoWeekDateRuleOrThrow(appointmentDate, dateTime.Now);

            if (appointmentRepository.AppointmentExists(patientId, appointmentDate))
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
                appointmentRepository.CommitChanges();
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
            var appointment = GetAppointmentOrThrow(patientId, appointmentDate);

            if ((appointmentDate - dateTime.Now).Days < 3)
            {
                throw new ValidationException("Cannot cancel appointment when its less than 3 days due");
            }

            try
            {
                appointmentRepository.CancelAppointment(appointment);
                equipmentService.SetEquipmentAvailable(appointmentDate);
                appointmentRepository.CommitChanges();
            }
            catch (Exception)
            {
                throw new Exception("There was an issue cancelling the appointment");
            }
        }

        /// <summary>
        /// Change an Appointment
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="appointmentDate"></param>
        /// <param name="newAppointmentDate"></param>
        public void Change(string patientId, DateTimeOffset appointmentDate, DateTimeOffset newAppointmentDate)
        {
            var existingAppointment = GetAppointmentOrThrow(patientId, appointmentDate);

            if ((appointmentDate - dateTime.Now).Days <= 2)
            {
                throw new ValidationException("Cannot change appointment less than two days before original appointment date");
            }

            AssertTwoWeekDateRuleOrThrow(newAppointmentDate, dateTime.Now);
            
            var availableEquipment = equipmentService.GetAvailableEquipment(newAppointmentDate);

            if (availableEquipment == null)
            {
                throw new ValidationException("No available equipment found for new appointment date");
            }

            try
            {
                appointmentRepository.CancelAppointment(existingAppointment);
                appointmentRepository.CreateAppointment(patientId, availableEquipment, newAppointmentDate);
                equipmentService.SetEquipmentAvailable(appointmentDate);
                equipmentService.SetEquipmentUnavailable(newAppointmentDate);
            }
            catch (Exception ex)
            {
                throw new Exception($"There was an error changing an appointment date: {ex.Message}");
            }    
        }

        /// <summary>
        /// Check for existing Appointment
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="appointmentDate"></param>
        /// <returns></returns>
        private Appointment GetAppointmentOrThrow(string patientId, DateTimeOffset appointmentDate)
        {
            var appointment = appointmentRepository.GetAppointment(patientId, appointmentDate);

            if (appointment == null)
            {
                throw new ValidationException("No appointment found");
            }

            return appointment;
        }

        /// <summary>
        /// Check dates are not two weeks apart and throw
        /// </summary>
        /// <param name="dateOne"></param>
        /// <param name="dateTwo"></param>
        private void AssertTwoWeekDateRuleOrThrow(DateTimeOffset dateOne, DateTimeOffset dateTwo)
        {
            if ((dateOne - dateTwo).Days > 14)
            {
                throw new ValidationException("Appointment date must not be later than two weeks from now");
            }
        }
    }
}
