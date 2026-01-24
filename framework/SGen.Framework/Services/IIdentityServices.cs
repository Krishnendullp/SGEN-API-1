using Framework.Common;


namespace SGen.Framework.Services
{
    public interface IIdentityServices
    {
       public IdentityModel GetIdentity();
       public  Task<int> GenerateNextPk<T>(Guid tenantId, Guid companyId) where T : class;
    }
}
