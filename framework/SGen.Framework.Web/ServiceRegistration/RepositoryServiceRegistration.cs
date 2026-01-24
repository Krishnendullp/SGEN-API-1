using Microsoft.Extensions.DependencyInjection;


namespace SGen.Framework.Web.ServiceRegistration
{
    public static class RepositoryServiceRegistration
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            //services.AddScoped<IUserRepository, UserRepository>();
            //services.AddScoped<IProjectMasterRepository, ProjectMasterRepository>();
            //services.AddScoped<IItemMasterRepository, ItemMasterRepository>();
            //services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();
            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //services.AddScoped<IPrimaryKeyGenerator, PrimaryKeyGenerator>();
            //services.AddScoped<ISupplierMasterRepository, SupplierMasterRepository>();

            return services;
        }
    }
}
