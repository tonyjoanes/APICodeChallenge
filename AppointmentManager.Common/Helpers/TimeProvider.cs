using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentManager.Common
{
    public class DateTimeWrapper : IDateTime
    {
        public DateTimeOffset Now { get { return DateTimeOffset.Now; } }
    }
}
