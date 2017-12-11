using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContextBase
{
    public class EntityVersionTenant : EntityBase
    {
        [Column("Version")]
        public int Version { get; set; }
        [Column("TenantId")]
        public Guid TenantId { get; set; }
    }
}
