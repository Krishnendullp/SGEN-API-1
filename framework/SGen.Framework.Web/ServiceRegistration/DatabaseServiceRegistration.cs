using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SGen.Framework.Contexts;
using System;


namespace SGen.Framework.Web.ServiceRegistration
{
    public static class DatabaseServiceRegistration
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<SGenDbContext>(options =>
            //    options.UseSqlServer(configuration.GetConnectionString("Connection")));

            services.AddDbContext<SGenDbContext>(options =>
            options.UseMySql(
                configuration.GetConnectionString("DefaultConnection"),
                new MySqlServerVersion(new Version(8, 0, 3)) // তোমার MySQL server version বসাও
            ));

            // ISgenDbContext interface কে SGenDbContext এর সাথে bind করা
            //services.AddScoped<ISgenDbContext>(provider => provider.GetRequiredService<SGenDbContext>());

            return services;
        }
    }
}
