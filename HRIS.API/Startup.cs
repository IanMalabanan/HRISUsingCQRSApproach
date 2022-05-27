using HRIS.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using OpenIddict;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using HRIS.Application;
using HRIS.Infrastructure.Repositories;
using HRIS.Application.Common.Interfaces.Application;
using HRIS.API.Services;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.SwaggerGen;
using HRIS.API.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace HRIS.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDBContext>(options =>
            {
                options.UseSqlServer(
                    Configuration["ConnectionStrings:DefaultConnection"]
                    , b => b.MigrationsAssembly("HRIS.Infrastructure"));
            });

            services.AddDbContext<ApiDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:HRISAPIContextConnection"]
                    , b => b.MigrationsAssembly("HRIS.API"));
            });

            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));

            var key = Encoding.ASCII.GetBytes(Configuration["JwtConfig:Secret"]);

            var tokenValidationParams = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                RequireExpirationTime = false,
                ClockSkew = TimeSpan.Zero
            };

            services.AddSingleton(tokenValidationParams);

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt => {
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = tokenValidationParams;
            });

            services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                        .AddEntityFrameworkStores<ApiDbContext>();

            services.AddApplication();
            services.AddInfrastructure(Configuration);
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.Load("HRIS.Application"));
            services.AddMediatR(Assembly.Load("HRIS.Infrastructure"));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("AUTH", new OpenApiInfo { Title = "Identity Server Environment", Version = "v1", Description = "Rest APIs for Identity Server" });
                c.SwaggerDoc("HRIS", new OpenApiInfo { Title = "HRIS UAT Environment", Version = "v1", Description = "Rest APIs for HRIS Application" });
                c.AddSecurityDefinition("BearerAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme.ToLowerInvariant(),
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    BearerFormat = "JWT",
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                c.OperationFilter<AuthResponsesOperationFilter>();
            });

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.DocumentTitle = "HRIS - Rest API Services";
                    c.SwaggerEndpoint("/swagger/AUTH/swagger.json", "AUTH");
                    c.SwaggerEndpoint("/swagger/HRIS/swagger.json", "HRIS");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseCors();
        }
    }

    internal class AuthResponsesOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var attributes = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                                .Union(context.MethodInfo.GetCustomAttributes(true));

            if (attributes.OfType<IAllowAnonymous>().Any())
            {
                return;
            }

            var authAttributes = attributes.OfType<IAuthorizeData>();

            if (authAttributes.Any())
            {
                operation.Responses["401"] = new OpenApiResponse { Description = "Unauthorized" };

                if (authAttributes.Any(att => !String.IsNullOrWhiteSpace(att.Roles) || !String.IsNullOrWhiteSpace(att.Policy)))
                {
                    operation.Responses["403"] = new OpenApiResponse { Description = "Forbidden" };
                }

                operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Id = "BearerAuth",
                                    Type = ReferenceType.SecurityScheme
                                }
                            },
                            Array.Empty<string>()
                        }
                    }
                };
            }
        }
    }
}
