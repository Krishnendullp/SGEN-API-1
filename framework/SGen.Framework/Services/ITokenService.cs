using Framework.Entities;

namespace Framework.Services
{
    public interface ITokenService
    {
       public string GenerateToken(User user, Guid tenantId, Guid companyId);
    }
}
