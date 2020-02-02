using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace AppointmentManager.API.Extentsions
{
    /// <summary>
    /// Extension methods for the ServiceCollections
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Configure the Application Cookie
        /// 
        /// If we don't do this then API requests that are not authorised will be
        /// routed to the login page by default but we want to return a 401
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureApplicationCooke(this IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.Events = new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents()
                {
                    OnRedirectToLogin = context =>
                    {
                        if (context.Request.Path.StartsWithSegments("/api") && context.Response.StatusCode == 200)
                        {
                            context.Response.StatusCode = 401;
                        }
                        return Task.CompletedTask;
                    },
                    OnRedirectToAccessDenied = context =>
                    {
                        if (context.Request.Path.StartsWithSegments("/api") && context.Response.StatusCode == 200)
                        {
                            context.Response.StatusCode = 401;
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        }
    }
}
