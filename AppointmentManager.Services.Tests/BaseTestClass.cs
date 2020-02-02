using AppointmentManager.Common;
using AppointmentManager.Data.Entities;
using AppointmentManager.Data.Repositories;
using Moq;
using System;

namespace AppointmentManager.Services.Tests
{
    /// <summary>
    /// Base Class for AppointmentService Tests
    /// They all have the same setup so can be done here rather than
    /// in each test class
    /// </summary>
    public class BaseTestClass
    {
        protected IAppointmentService sut;
        protected Mock<IDateTime> mockDate = new Mock<IDateTime>();
        protected Mock<IEquipmentService> mockEquipmentService = new Mock<IEquipmentService>();
        protected Mock<IAppointmentRepository> mockAppointmentRepository = new Mock<IAppointmentRepository>();
        protected string patientId = "ABC332646";
        protected Equipment equipment = new Equipment();

        /// <summary>
        /// Initialise an instance of the BaseTestClass
        /// </summary>
        public BaseTestClass()
        {
            sut = new AppointmentService(mockEquipmentService.Object,
                                         mockAppointmentRepository.Object,
                                         mockDate.Object);

            equipment.Id = 1;
            equipment.Name = "MEDICALDEVICE";
            equipment.Status = EquipmentStatus.Available;
        }

        protected DateTimeOffset GetFebruaryWithDayDate(int dayDate)
        {
            return new DateTimeOffset(2020, 2, dayDate, 10, 00, 00, TimeSpan.Zero);
        }

        protected DateTimeOffset GetMarchWithDayDate(int dayDate)
        {
            return new DateTimeOffset(2020, 3, dayDate, 10, 00, 00, TimeSpan.Zero);
        }

        protected DateTimeOffset GetMayWithDayDate(int dayDate)
        {
            return new DateTimeOffset(2020, 5, dayDate, 10, 00, 00, TimeSpan.Zero);
        }
    }
}
