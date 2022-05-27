using CorrelationId.HttpClient;
using HRIS.Application.Common.Interfaces;
using HRIS.Application.Common.Interfaces.Application;
using HRIS.Application.Common.Interfaces.Repositories;
using HRIS.Infrastructure.Common.Configuration;
using HRIS.Infrastructure.Common.Configuration.Location;
using HRIS.Infrastructure.Common.Configuration.Messaging;
using HRIS.Infrastructure.Repositories;
using HRIS.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HRIS.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();

            var _apiOptions = configuration.GetSection(nameof(ApiOptions)).Get<ApiOptions>();
            var _rootConfig = configuration.GetSection(nameof(WebUIOptions)).Get<WebUIOptions>();
            
                services.AddDbContext<ApplicationDBContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(ApplicationDBContext).Assembly.FullName)));

            //Services

            services.AddScoped<IDomainEventService, DomainEventService>();
            services.AddTransient<IKeyGenerator, MongoKeyGenerator>();
            services.AddScoped<HttpClient>();

            services.AddScoped<ClientConfigBuilder>();
            services.AddScoped<LocationClientBuilder>();

            services.AddScoped<ILocationService, LocationService>();

            ////Standard services 
            services.AddScoped<IDomainEventService, DomainEventService>();
            services.AddScoped<IDateTime, DateTimeService>();

            var _messagingConfig = configuration.GetSection(nameof(MessagingClientConfig)).Get<MessagingClientConfig>();
            services.AddSingleton(c => new MessagingClientConfigBuilder(_messagingConfig));
            services.AddScoped<IMessagingService, MessagingService>();


            //Repositories
            services.AddScoped<IEmployeeRepository,EmployeeRepository>();
            services.AddScoped<IAuditTrailsRepository, AuditTrailsRepository>();

            services.AddScoped<ITokenAccessorService, TokenAccessorService>();
            return services;
        }




        private static Task OnMessageReceived(MessageReceivedContext context, WebUIOptions webUIOptions)
        {
            context.Properties.IsPersistent = true;
            context.Properties.ExpiresUtc = new DateTimeOffset(DateTime.UtcNow.AddHours(webUIOptions.Authentication.CookieExpiresUtcHours));

            return Task.FromResult(0);
        }

        private static Task OnRedirectToIdentityProvider(RedirectContext n, WebUIOptions webUIOptions)
        {
            n.ProtocolMessage.RedirectUri = webUIOptions.Authentication.IdentityRedirectUri;

            return Task.FromResult(0);
        }
    }
}
