using Framework.Repositories;
using Framework.Repositories.Implementations;
using Framework.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;
using SGen.Framework.Services;
using SGen.Framework.Services.Implementations;
using System;


namespace SGen.Framework.Web.ServiceRegistration
{
    public static class DomainServiceRegistration
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            //services.AddScoped<IUserService, UserService>();
            //services.AddScoped<IProjectMasterService, ProjectMasterService>();
            //services.AddScoped<IItemMasterService, ItemMasterService>();
            //services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();
            //services.AddScoped<ISupplierMasterService, SupplierMasterService>();
            //services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IIdentityServices,IdentityServices>();
            //services.AddScoped(typeof(ICrudService<,>), typeof(CrudService<,>));
            //services.AddScoped(typeof(ICrudRepository<,>), typeof(CrudRepository<,>));
            //services.AddScoped<IUnitService, UnitService>();
            return services;
        }
    }
}
