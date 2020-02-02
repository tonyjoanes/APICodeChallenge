using System;
using System.Threading.Tasks;
using AppointmentManager.API.Models;
using AppointmentManager.Common.Validation;
using AppointmentManager.Data.Entities;
using AppointmentManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService appointmentService;
        private readonly INotificationService notificationService;
        private readonly UserManager<User> userManager;

        /// <summary>
        /// Initialise a new instance of the AppointmentController
        /// </summary>
        public AppointmentController(IAppointmentService appointmentService, 
                                     INotificationService notificationService,
                                     UserManager<User> userManager)
        {
            this.appointmentService = appointmentService;
            this.notificationService = notificationService;
            this.userManager = userManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Endpoint to make an appointment
        /// </summary>
        /// <param name="value"></param>
        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] AppointmentModel createAppointmentModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // we could use the PatientID from the logged in user really, it would be them creating an
                // appointment for themselves.
                var user = await userManager.GetUserAsync(HttpContext.User);
                appointmentService.Create(createAppointmentModel.PatientId, createAppointmentModel.AppointmentDate);

                // In real life I would just send this message to a queue and allow a distributed
                // service pick up the message and send off the email so that this can return
                // immediately
                notificationService.SendConfirmationEmail(createAppointmentModel.PatientId, 
                                                          user.Email, 
                                                          createAppointmentModel.AppointmentDate);
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
        public IActionResult Change([FromBody] AppointmentChangeModel changeAppointmentModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                appointmentService.Change(changeAppointmentModel.PatientId,
                                          changeAppointmentModel.CurrentAppointmentDate,
                                          changeAppointmentModel.NewAppointmentDate);

                return Ok();
            }
            catch (ValidationException validationException)
            {
                return BadRequest(validationException.Message);
            }
        }

        /// <summary>
        /// Endpoint to cancel an existing appointment
        /// </summary>
        /// <param name="value"></param>
        [HttpPut("cancel")]
        public IActionResult Cancel([FromBody] AppointmentModel cancelAppointmentModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                appointmentService.Cancel(cancelAppointmentModel.PatientId, cancelAppointmentModel.AppointmentDate);
                return Ok();
            }
            catch (ValidationException validationException)
            {
                return BadRequest(validationException.Message);
            }
        }
    }
}