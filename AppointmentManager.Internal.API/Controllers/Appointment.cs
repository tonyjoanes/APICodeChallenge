using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentManager.Common;
using AppointmentManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AppointmentManager.Internal.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]/api")]
    public class Appointment : Controller
    {
        private readonly ILogger<Appointment> _logger;
        private readonly IAppointmentService appointmentService;

        public Appointment(ILogger<Appointment> logger, 
                           IAppointmentService appointmentService,
                           IDateTime dateTime)
        {
            _logger = logger;
            this.appointmentService = appointmentService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var appointments = appointmentService.GetByDate(DateTime.Now.Date);

            return Ok(appointments);
        }
    }
}
