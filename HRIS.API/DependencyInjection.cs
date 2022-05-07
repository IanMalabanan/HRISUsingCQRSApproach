using HRIS.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRIS.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUIDependency(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddHttpClient<MessagingService>(c =>
            {
                c.BaseAddress = new Uri(Configuration["MessagingClientConfig:BaseUrl"]);
            });

            
            //services.AddScoped<ILocationService, LocationService>();
            //services.AddScoped<IAuditLoggerService, AuditLoggerService>();


            services.Configure<IdentityServerSettings>(
                Configuration.GetSection("SystemClientConfig:IdentityServerSettings"));

            return services;
        }
    }
}
