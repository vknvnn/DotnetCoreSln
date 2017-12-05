using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostgreSqlDemo.Bases
{
    public class EntityVersionTenant : EntityTracking
    {
        [Column("Version")]
        public int Version { get; set; }
        [Column("TenantId")]
        public Guid TenantId { get; set; }
    }
}
