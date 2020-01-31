using AppointmentManager.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManager.Data
{
    public class IdentityInitializer
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public IdentityInitializer(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        /// <summary>
        /// Seed Indentity users if they do not already exist in the system
        /// </summary>
        /// <returns></returns>
        public async Task Seed()
        {
            var user = await userManager.FindByNameAsync("testpatient");
           
            if (user == null)
            {
                if (!(await roleManager.RoleExistsAsync("patient")))
                {
                    var patientRole = new IdentityRole("patient");
                    await roleManager.CreateAsync(patientRole);
                }

                user = new User
                {
                    UserName = "testpatient",
                    FirstName = "Test",
                    LastName = "Patient",
                    Email = "testpatient@test.com"
                };

                var userResult = await userManager.CreateAsync(user, "P@ssw0rd!");
                var roleResult = await userManager.AddToRoleAsync(user, "patient");

                if (!userResult.Succeeded || !roleResult.Succeeded)
                {
                    throw new InvalidOperationException("Unable to seed identity");
                }
            }
        }
    }
}
