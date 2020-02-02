using AppointmentManager.Common.Validation;
using AppointmentManager.Data.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AppointmentManager.Services.Tests
{
    public class AppointmentChangeTests : BaseTestClass
    {
        [Fact]
        public void WhenChangingAppointment_ShouldCheckForAppointmentExistence()
        {
            var appointmentDate = GetFebruaryWithDayDate(5);
            var currentDate = GetFebruaryWithDayDate(1);
            var newAppointmentDate = GetFebruaryWithDayDate(8);

            mockDate.Setup(x => x.Now).Returns(currentDate);
            mockAppointmentRepository.Setup(x => x.GetAppointment(patientId, appointmentDate)).Returns(new Appointment());
            mockEquipmentService.Setup(x => x.GetAvailableEquipment(newAppointmentDate)).Returns(equipment);

            sut.Change(patientId, appointmentDate, newAppointmentDate);

            mockAppointmentRepository.Verify(x => x.GetAppointment(patientId, appointmentDate));
        }

        [Fact]
        public void WhenAppointmentCantBeFound_ShouldThrowValidationException()
        {
            var appointmentDate = GetFebruaryWithDayDate(5);
            var currentDate = GetFebruaryWithDayDate(1);
            var newAppointmentDate = GetFebruaryWithDayDate(8);

            mockDate.Setup(x => x.Now).Returns(currentDate);
            mockAppointmentRepository.Setup(x => x.GetAppointment(patientId, appointmentDate)).Returns((Appointment)null);
            mockEquipmentService.Setup(x => x.GetAvailableEquipment(newAppointmentDate)).Returns(equipment);

            var exception = Assert.Throws<ValidationException>(() => sut.Change(patientId, appointmentDate, newAppointmentDate));
            Assert.NotNull(exception);
            Assert.Equal("No appointment found", exception.Message);
        }

        [Theory]
        [InlineData(8, 9)]
        [InlineData(20, 22)]
        public void WhenAttemptingToChangeLessThan2DaysFromCurrent_ShouldThrowValidationException(int currentDayDate, int existingAppointmentDayDate)
        {
            var appointmentDate = GetFebruaryWithDayDate(existingAppointmentDayDate);
            var currentDate = GetFebruaryWithDayDate(currentDayDate);
            var newAppointmentDate = GetMarchWithDayDate(1);

            mockDate.Setup(x => x.Now).Returns(currentDate);
            mockAppointmentRepository.Setup(x => x.GetAppointment(patientId, appointmentDate)).Returns(new Appointment());

            var exception = Assert.Throws<ValidationException>(() => sut.Change(patientId, appointmentDate, newAppointmentDate));
            Assert.NotNull(exception);
            Assert.Equal("Cannot change appointment less than two days before original appointment date", exception.Message);
        }

        [Fact]
        public void WhenAttemptingToScheduleGreaterThan2Weeks_ShouldThrowValidationException()
        {
            var appointmentDate = GetFebruaryWithDayDate(5);
            var currentDate = GetFebruaryWithDayDate(1);
            var newAppointmentDate = GetFebruaryWithDayDate(28);

            mockDate.Setup(x => x.Now).Returns(currentDate);
            mockAppointmentRepository.Setup(x => x.GetAppointment(patientId, appointmentDate)).Returns(new Appointment());

            var exception = Assert.Throws<ValidationException>(() => sut.Change(patientId, appointmentDate, newAppointmentDate));
            Assert.NotNull(exception);
            Assert.Equal("Appointment date must not be later than two weeks from now", exception.Message);
        }

        [Fact]
        public void WhenChangingAppointment_ShouldCheckEquipmentAvailability()
        {
            var appointmentDate = GetFebruaryWithDayDate(5);
            var currentDate = GetFebruaryWithDayDate(1);
            var newAppointmentDate = GetFebruaryWithDayDate(6);

            mockDate.Setup(x => x.Now).Returns(currentDate);

            mockAppointmentRepository.Setup(x => x.GetAppointment(patientId, appointmentDate)).Returns(new Appointment());
            mockEquipmentService.Setup(x => x.GetAvailableEquipment(newAppointmentDate)).Returns(equipment);

            sut.Change(patientId, appointmentDate, newAppointmentDate);

            mockEquipmentService.Verify(x => x.GetAvailableEquipment(newAppointmentDate), Times.Once);           
        }

        [Fact]
        public void WhenEquipmentUnavailable_ShouldThrowValidationException()
        {
            var appointmentDate = GetFebruaryWithDayDate(5);
            var currentDate = GetFebruaryWithDayDate(1);
            var newAppointmentDate = GetFebruaryWithDayDate(6);

            mockDate.Setup(x => x.Now).Returns(currentDate);
            mockAppointmentRepository.Setup(x => x.GetAppointment(patientId, appointmentDate)).Returns(new Appointment());
            mockEquipmentService.Setup(x => x.GetAvailableEquipment(newAppointmentDate)).Returns((Equipment)null);

            var exception = Assert.Throws<ValidationException>(() => sut.Change(patientId, appointmentDate, newAppointmentDate));
            Assert.NotNull(exception);
            Assert.Equal("No available equipment found for new appointment date", exception.Message);
        }

        [Fact]
        public void WhenChangingAppointment_ShouldCancelExistingAppointment()
        {
            var appointmentDate = GetFebruaryWithDayDate(5);
            var currentDate = GetFebruaryWithDayDate(1);
            var newAppointmentDate = GetFebruaryWithDayDate(6);
            var appointment = new Appointment();

            mockDate.Setup(x => x.Now).Returns(currentDate);
            mockAppointmentRepository.Setup(x => x.GetAppointment(patientId, appointmentDate)).Returns(appointment);
            mockEquipmentService.Setup(x => x.GetAvailableEquipment(newAppointmentDate)).Returns(equipment);

            sut.Change(patientId, appointmentDate, newAppointmentDate);

            mockAppointmentRepository.Verify(x => x.CancelAppointment(appointment), Times.Once);
        }

        [Fact]
        public void WhenChangingAppointment_ShouldCreateNewAppointment()
        {
            var appointmentDate = GetFebruaryWithDayDate(5);
            var currentDate = GetFebruaryWithDayDate(1);
            var newAppointmentDate = GetFebruaryWithDayDate(6);
            var appointment = new Appointment();

            mockDate.Setup(x => x.Now).Returns(currentDate);
            mockAppointmentRepository.Setup(x => x.GetAppointment(patientId, appointmentDate)).Returns(appointment);
            mockEquipmentService.Setup(x => x.GetAvailableEquipment(newAppointmentDate)).Returns(equipment);

            sut.Change(patientId, appointmentDate, newAppointmentDate);

            mockAppointmentRepository.Verify(x => x.CreateAppointment(patientId, equipment, newAppointmentDate), Times.Once);
        }

        [Fact]
        public void WhenChangingAppointment_ShouldSetEquipmentAvailableUsingOldDate()
        {
            var appointmentDate = GetFebruaryWithDayDate(5);
            var currentDate = GetFebruaryWithDayDate(1);
            var newAppointmentDate = GetFebruaryWithDayDate(6);
            var appointment = new Appointment();

            mockDate.Setup(x => x.Now).Returns(currentDate);
            mockAppointmentRepository.Setup(x => x.GetAppointment(patientId, appointmentDate)).Returns(appointment);
            mockEquipmentService.Setup(x => x.GetAvailableEquipment(newAppointmentDate)).Returns(equipment);

            sut.Change(patientId, appointmentDate, newAppointmentDate);

            mockEquipmentService.Verify(x => x.SetEquipmentAvailable(appointmentDate), Times.Once);
        }

        [Fact]
        public void WhenChangingAppointment_ShouldSetEquipmentUnavailableUsingNewDate()
        {
            var appointmentDate = GetFebruaryWithDayDate(5);
            var currentDate = GetFebruaryWithDayDate(1);
            var newAppointmentDate = GetFebruaryWithDayDate(6);
            var appointment = new Appointment();

            mockDate.Setup(x => x.Now).Returns(currentDate);
            mockAppointmentRepository.Setup(x => x.GetAppointment(patientId, appointmentDate)).Returns(appointment);
            mockEquipmentService.Setup(x => x.GetAvailableEquipment(newAppointmentDate)).Returns(equipment);

            sut.Change(patientId, appointmentDate, newAppointmentDate);

            mockEquipmentService.Verify(x => x.SetEquipmentUnavailable(newAppointmentDate), Times.Once);
        }
    }
}
