using System;
using System.Threading.Tasks;
using AppointmentManager.API.Models;
using AppointmentManager.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AppointmentManager.API.Controllers
{
    /// <summary>
    /// Athorisation Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<User> signInManager;
        private readonly ILogger<AuthController> logger;

        /// <summary>
        /// Initialise an instance of the AuthController
        /// </summary>
        /// <param name="signInManager"></param>
        /// <param name="logger"></param>
        public AuthController(SignInManager<User> signInManager, ILogger<AuthController> logger)
        {
            this.signInManager = signInManager;
            this.logger = logger;
        }

        public int Get()
        {
            return 69;
        }

        /// <summary>
        /// Login with credentials
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] CredentialModel model)
        {
            try
            {
                var signInResult = await signInManager.PasswordSignInAsync(model.UserName, model.Password, isPersistent: false, lockoutOnFailure: false);

                if (signInResult.Succeeded)
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"There was a problem logging in the user {ex}");
            }

            // Don't give away any information why this
            // has failed for security reasons ;-)
            return BadRequest();
        }
    }
}