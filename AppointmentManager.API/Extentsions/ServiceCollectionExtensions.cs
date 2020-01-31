using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace AppointmentManager.API.Extentsions
{
    public static class ServiceCollectionExtensions
    {
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
