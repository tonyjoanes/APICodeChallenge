using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentManager.Common
{
    /// <summary>
    /// The DateTimeWrapper
    /// </summary>
    public class DateTimeWrapper : IDateTime
    {
        /// <summary>
        /// Gets or sets the Now
        /// </summary>
        public DateTimeOffset Now { get { return DateTimeOffset.Now; } }
    }
}
