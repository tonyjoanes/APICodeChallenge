using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentManager.API.Models;
using AppointmentManager.Common.Validation;
using AppointmentManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AppointmentManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService appointmentService;

        /// <summary>
        /// Initialise a new instance of the AppointmentController
        /// </summary>
        public AppointmentController(IAppointmentService appointmentService)
        {
            this.appointmentService = appointmentService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        /// <summary>
        /// Endpoint to make an appointment
        /// </summary>
        /// <param name="value"></param>
        [HttpPost("create")]
        public IActionResult Post([FromBody] CreateAppointmentModel createAppointmentModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                appointmentService.Create(createAppointmentModel.PatientId, createAppointmentModel.AppointmentDate);
            }
            catch (ValidationException validationException)
            {
                return BadRequest(validationException.Message);
            }

            return Created($"appointments", null);
        }

        /// <summary>
        /// Endpoint to change an appointment date
        /// </summary>
        /// <param name="value"></param>
        [HttpPut("change")]
        public IActionResult Change([FromBody] string value)
        {
            return Ok(value);
        }

        /// <summary>
        /// Endpoint to cancel an existing appointment
        /// </summary>
        /// <param name="value"></param>
        [HttpPut("cancel")]
        public IActionResult Cancel([FromBody] string value)
        {
            return Ok(value);
        }
    }
}