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

        }

        [Fact]
        public void WhenAppointmentCantBeFound_ShouldThrowValidationException()
        {

        }

        [Fact]
        public void WhenAttemptingToChangeLessThan2DaysFromCurrent_ShouldThrowValidationException()
        {

        }

        [Fact]
        public void WhenAttemptingToScheduleGreaterThan2Weeks_ShouldThrowValidationException()
        {

        }

        [Fact]
        public void WhenAppointmentChanged_ShouldSetEquipmentUnavailable()
        {

        }
    }
}
