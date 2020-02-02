using System;

namespace AppointmentManager.Common
{
    /// <summary>
    /// DateTime Interface
    /// </summary>
    public interface IDateTime
    {
        /// <summary>
        /// Gets or sets the Now value
        /// </summary>
        DateTimeOffset Now { get; }
    }
}