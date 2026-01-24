
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using SGen.Framework.Web.Authentication;
using SGen.Framework.Web.ServiceRegistration;
using SGen.Framework.Web.Swagger;
using System;


namespace SGen.Framework.Web.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // DbContext
            services.AddDatabase(configuration);

            // Domain Services
            services.AddDomainServices();

            // Repositories
            services.AddRepositories();

            // AutoMapper
            services.AddMappingProfiles();

            // Authentication (optional)
            //services.AddJwtAuthentication(configuration);

            // ✅ Swagger + JWT Setup
            services.AddSwaggerWithJwt();


            // Middleware, Logging, Email, etc.
            // services.AddScoped<ILoggingService, LoggingService>();

            return services;
        }
    }
}
