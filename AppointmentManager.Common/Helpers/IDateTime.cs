using System;

namespace AppointmentManager.Common
{
    public interface IDateTime
    {
        DateTimeOffset Now { get; }
    }
}