using System;

namespace Framework.Common
{
    public class IdentityModel
    {
        public Guid TenantId { get; set; }
        public Guid CompanyId { get; set; }
        public string UserId { get; set; }
    }
}
