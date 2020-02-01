using AppointmentManager.Common;
using AppointmentManager.Data.Repositories;
using Moq;

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

        /// <summary>
        /// Initialise an instance of the BaseTestClass
        /// </summary>
        public BaseTestClass()
        {
            sut = new AppointmentService(mockEquipmentService.Object,
                                         mockAppointmentRepository.Object,
                                         mockDate.Object);
        }
    }
}
