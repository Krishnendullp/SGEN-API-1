
using Framework.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Entities
{
    public abstract class BaseEntity<TKey> 
    {
        //public required TKey Id { get; set; }
        public Guid TenantId { get; set; }
        public Guid CompanyId { get; set; }
        public string? CreatedBy { get; set; } 
        //public long? UpdatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsActive { get; set; } 
    }
}