using Microsoft.Extensions.DependencyInjection;
using SGen.Framework.Web.Mapper.MappingProfiles;
using SGen.Web.Infrastructure.Mapper.MappingProfiles;
using System;


namespace SGen.Framework.Web.ServiceRegistration
{
    public static class MapperServiceRegistration
    {
        public static IServiceCollection AddMappingProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
            //    cfg.AddProfile<UserProfile>();
                  cfg.AddProfile<ProjectProfile>();
                  cfg.AddProfile<ItemProfile>();
                  cfg.AddProfile<PurchaseProfile>();
                  cfg.AddProfile<SupplierProfile>();
                  cfg.AddProfile<UnitProfile>();
                  cfg.AddProfile<TaxProfiles>();
                  cfg.AddProfile<SaleProfile>();
                  cfg.AddProfile<UserProfile>();
                  cfg.AddProfile<CustomerProfile>();
                  cfg.AddProfile<FinanceProfiles>();
            //    // cfg.AddProfile<ProductProfile>(); // more profiles can be added here
            });
            return services;
        }
    }
}
