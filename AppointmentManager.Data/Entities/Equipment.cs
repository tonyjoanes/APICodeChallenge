using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentManager.Data.Entities
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
    }
}
