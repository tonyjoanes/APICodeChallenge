using AppointmentManager.Common;
using AppointmentManager.Common.Validation;
using AppointmentManager.Data.Entities;
using AppointmentManager.Data.Repositories;
using Moq;
using System;
using Xunit;

namespace AppointmentManager.Services.Tests
{
    public class AppointmentCreationTests
    {
        private AppointmentService sut;
        private Mock<IDateTime> mockDate = new Mock<IDateTime>();
        private Mock<IEquipmentService> mockEquipmentService = new Mock<IEquipmentService>();
        private Mock<IAppointmentRepository> mockAppointmentRepository = new Mock<IAppointmentRepository>();

        public AppointmentCreationTests()
        {
            sut = new AppointmentService(mockEquipmentService.Object, 
                                         mockAppointmentRepository.Object, 
                                         mockDate.Object);
        }

        [Fact]
        public void UsingSub2WeekDate_ShouldNotThrowValidationException()
        {
            var appointmentDate = new DateTimeOffset(2020, 1, 1, 10, 00, 00, TimeSpan.Zero);
            mockDate.Setup(x => x.Now)
                    .Returns(appointmentDate.AddDays(-1));

            mockEquipmentService.Setup(x => x.GetAvailableEquipment(appointmentDate))
                                .Returns(new Equipment { Id = 1, Name = "Device-1" });

            var exception = Record.Exception(() => sut.Create("patient1", appointmentDate));
            Assert.Null(exception);
        }

        [Theory]
        [InlineData(15)]
        [InlineData(67)]
        [InlineData(1000)]
        public void UsingDateGreaterThan2Weeks_ShouldThrowValidationException(int daysSubAppointmentDate)
        {
            var appointmentDate = new DateTimeOffset(2020, 1, 1, 10, 00, 00, TimeSpan.Zero);
            mockDate.Setup(x => x.Now)
                    .Returns(appointmentDate.AddDays(-daysSubAppointmentDate));         

            var exception = Assert.Throws<ValidationException>(() => sut.Create("patient1", appointmentDate));
            Assert.NotNull(exception);
            Assert.Equal("Appointment date must not be later than two weeks from now", exception.Message);
        }

        [Fact]
        public void UsingDateInPast_ShouldThrowValidationException()
        {
            var appointmentDate = new DateTimeOffset(2020, 1, 1, 10, 00, 00, TimeSpan.Zero);
            mockDate.Setup(x => x.Now)
                    .Returns(new DateTimeOffset(2020, 1, 2, 10, 00, 00, TimeSpan.Zero));

            var exception = Assert.Throws<ValidationException>(() => sut.Create("patient1", appointmentDate));
            Assert.NotNull(exception);
            Assert.Equal("Appointment date cannot be in the past", exception.Message);
        }

        [Fact]
        public void WhenNoAvailableEquipment_ShouldThrowValidationException()
        {
            var appointmentDate = new DateTimeOffset(2020, 2, 1, 10, 00, 00, TimeSpan.Zero);

            mockDate.Setup(x => x.Now)
                    .Returns(new DateTimeOffset(2020, 2, 1, 10, 00, 00, TimeSpan.Zero));

            mockEquipmentService.Setup(x => x.GetAvailableEquipment(appointmentDate))
                                .Returns((Equipment)null);

            var exception = Assert.Throws<ValidationException>(() => sut.Create("35152", appointmentDate));
            Assert.NotNull(exception);
        }

        [Fact]
        public void WhenAppointmentAlreadyExistsOnDateProvided_ShouldThrowValidationError()
        {
            var appointmentDate = new DateTimeOffset(2020, 2, 1, 10, 00, 00, TimeSpan.Zero);
            var availableEquipment = new Equipment { Id = 1, Name = "Device 101", Status = Status.Available };

            mockDate.Setup(x => x.Now)
                    .Returns(new DateTimeOffset(2020, 2, 1, 10, 00, 00, TimeSpan.Zero));
            mockEquipmentService.Setup(x => x.GetAvailableEquipment(appointmentDate))
                                .Returns(availableEquipment);

            mockAppointmentRepository.Setup(x => x.AppointmentExists(appointmentDate)).Returns(true);

            var exception = Assert.Throws<ValidationException>(() => sut.Create("tdfbfdb", appointmentDate));
            Assert.Equal("An appointment already exists for this date", exception.Message);
        }

        [Fact]
        public void WhenEquipmentAvailable_ShouldCreateApppointment()
        {
            var appointmentDate = new DateTimeOffset(2020, 2, 1, 10, 00, 00, TimeSpan.Zero);
            var availableEquipment = new Equipment { Id = 1, Name = "Device 101", Status = Status.Available };
            var patientId = "3523525";

            mockDate.Setup(x => x.Now)
                    .Returns(new DateTimeOffset(2020, 2, 1, 10, 00, 00, TimeSpan.Zero));

            mockEquipmentService.Setup(x => x.GetAvailableEquipment(appointmentDate))
                                .Returns(availableEquipment);
            
            sut.Create(patientId, appointmentDate);

            mockAppointmentRepository.Verify(x => x.CreateAppointment(patientId, availableEquipment, appointmentDate), Times.Once);
        }

        [Fact]
        public void WhenAppointmentIsCreated_ShouldSetEquipmentStatus()
        {
            var appointmentDate = new DateTimeOffset(2020, 2, 1, 10, 00, 00, TimeSpan.Zero);
            var availableEquipment = new Equipment { Id = 1, Name = "Device 101", Status = Status.Available };
            var patientId = "3523525";

            mockDate.Setup(x => x.Now)
                    .Returns(new DateTimeOffset(2020, 2, 1, 10, 00, 00, TimeSpan.Zero));

            mockEquipmentService.Setup(x => x.GetAvailableEquipment(appointmentDate))
                                .Returns(availableEquipment);

            mockEquipmentService.Setup(x => x.SetEquipmentUnavailable(availableEquipment));

            sut.Create(patientId, appointmentDate);

            mockEquipmentService.Verify(x => x.SetEquipmentUnavailable(availableEquipment), Times.Once);
        }
    }
}
