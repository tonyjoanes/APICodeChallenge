using AppointmentManager.Common;
using AppointmentManager.Common.Validation;
using AppointmentManager.Data.Repositories;
using System;

namespace AppointmentManager.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IEquipmentService equipmentService;
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IDateTime dateTime;

        public AppointmentService(IEquipmentService equipmentService,
                                  IAppointmentRepository AppointmentRepository,
                                  IDateTime dateTime)
        {
            this.equipmentService = equipmentService;
            appointmentRepository = AppointmentRepository;
            this.dateTime = dateTime;
        }

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
                equipmentService.SetEquipmentUnavailable(availableEquipment);
            }
            catch (Exception ex)
            {
                throw new Exception($"There was an issue creating the appointment: {ex.Message}");
            }
        }
    }
}
