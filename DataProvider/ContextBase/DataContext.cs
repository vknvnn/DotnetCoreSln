using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ContextBase
{
    
    public class DataContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public readonly ITenantFactory TenantFactory;
        public DataContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            TenantFactory = new TenantFactory(_httpContextAccessor.HttpContext);            
        }

        private void HandleTracking()
        {
            
            foreach (var entry in ChangeTracker.Entries<EntityVersionTenant>())
            {
                if (entry.Entity.IsNoneTracking)
                {
                    continue;
                }
                EntityTracking entity;
                switch (entry.State)
                {
                    case EntityState.Detached:
                    case EntityState.Modified:
                        entry.Entity.Version++;
                        entity = (entry.Entity as EntityTracking);
                        if (entity != null)
                        {
                            entity.ModifiedDate = new DateTimeOffset(DateTime.UtcNow, TimeSpan.FromMinutes(TenantFactory.GetClientOffset()));
                            entity.ModifiedBy = TenantFactory.GetUserName();
                        }
                        break;
                    case EntityState.Added:
                        entry.Entity.Version = 0;
                        entry.Entity.TenantId = TenantFactory.GetTenantId();
                        entity = (entry.Entity as EntityTracking);
                        if (entity != null)
                        {
                            entity.CreatedDate = new DateTimeOffset(DateTime.UtcNow, TimeSpan.FromMinutes(TenantFactory.GetClientOffset()));
                            entity.CreatedBy = TenantFactory.GetUserName();
                            entity.ModifiedDate = new DateTimeOffset(DateTime.UtcNow, TimeSpan.FromMinutes(TenantFactory.GetClientOffset()));
                            entity.ModifiedBy = TenantFactory.GetUserName();
                        }
                        break;
                }                
            }            
        }

        public override int SaveChanges()
        {
            HandleTracking();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            HandleTracking();
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
