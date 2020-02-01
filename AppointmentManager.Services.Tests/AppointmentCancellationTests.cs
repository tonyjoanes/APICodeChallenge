using AppointmentManager.Common;
using AppointmentManager.Common.Validation;
using AppointmentManager.Data.Entities;
using AppointmentManager.Data.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AppointmentManager.Services.Tests
{
    public class AppointmentCancellationTests : BaseTestClass
    {
        [Fact]
        public void WhenAppointmentDoesNotExist_ShouldThrowValidationException()
        {
            var patientId = "332646";
            var appointmentDate = new DateTimeOffset(2020, 1, 1, 10, 00, 00, TimeSpan.Zero);

            mockAppointmentRepository.Setup(x => x.GetAppointment(patientId, appointmentDate)).Returns((Appointment)null);

            Assert.Throws<ValidationException>(() => sut.Cancel(patientId, appointmentDate));
        }

        [Theory]
        [InlineData(10, 8)]
        [InlineData(21, 20)]
        public void WhenCancellationDateIsLessThan3DaysBeforeAppointmentDate_ShouldThrowValidationException(int appointmentDateDay, int currentDateDay)
        {
            var patientId = "332646";
            var appointmentDate = new DateTimeOffset(2020, 2, appointmentDateDay, 10, 10, 00, 00, TimeSpan.Zero);

            mockDate.Setup(x => x.Now)
                    .Returns(new DateTimeOffset(2020, 2, currentDateDay, 10, 00, 00, TimeSpan.Zero));

            mockAppointmentRepository.Setup(x => x.GetAppointment(patientId, appointmentDate))
                                     .Returns((Appointment)null);

            Assert.Throws<ValidationException>(() => sut.Cancel(patientId, appointmentDate));
        }

        [Fact]
        public void WhenAppointmentValid_ShouldCallDeleteOnAppointment()
        {
            var patientId = "332646";
            var appointmentDate = new DateTimeOffset(2020, 2, 10, 10, 00, 00, TimeSpan.Zero);
            var appointment = new Appointment
            {
                Id = 1,
                Date = appointmentDate,
                PatientId = patientId
            };

            mockDate.Setup(x => x.Now)
                    .Returns(new DateTimeOffset(2020, 2, 2, 10, 00, 00, TimeSpan.Zero));

            mockAppointmentRepository.Setup(x => x.GetAppointment(patientId, appointmentDate)).Returns(appointment);

            sut.Cancel(patientId, appointmentDate);

            mockAppointmentRepository.Verify(x => x.CancelAppointment(appointment), Times.Once);
        }

        [Fact]
        public void WhenCancellingAppointmentAndErrorOccurs_ShouldThrowAnError()
        {
            var patientId = "332646";
            var appointmentDate = new DateTimeOffset(2020, 2, 10, 10, 00, 00, TimeSpan.Zero);
            var appointment = new Appointment
            {
                Id = 1,
                Date = appointmentDate,
                PatientId = patientId
            };

            mockDate.Setup(x => x.Now)
                    .Returns(new DateTimeOffset(2020, 2, 2, 10, 00, 00, TimeSpan.Zero));

            mockAppointmentRepository.Setup(x => x.GetAppointment(patientId, appointmentDate)).Returns(appointment);
            mockAppointmentRepository.Setup(x => x.CancelAppointment(appointment)).Throws<ValidationException>();

            Assert.Throws<Exception>(() => sut.Cancel(patientId, appointmentDate));
        }

        [Fact]
        public void WhenAppointmentValid_ShouldSetEquipmentAvailable()
        {
            var patientId = "332646";
            var appointmentDate = new DateTimeOffset(2020, 2, 10, 10, 00, 00, TimeSpan.Zero);
            var appointment = new Appointment
            {
                Id = 1,
                Date = appointmentDate,
                PatientId = patientId
            };

            mockDate.Setup(x => x.Now)
                    .Returns(new DateTimeOffset(2020, 2, 2, 10, 00, 00, TimeSpan.Zero));

            mockAppointmentRepository.Setup(x => x.GetAppointment(patientId, appointmentDate)).Returns(appointment);

            sut.Cancel(patientId, appointmentDate);

            mockEquipmentService.Verify(x => x.SetEquipmentAvailable(appointmentDate), Times.Once);
        }
    }
}
