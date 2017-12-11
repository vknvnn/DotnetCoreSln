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
            TenantFactory = new TenantFactory();
            TenantFactory.Load(_httpContextAccessor.HttpContext);
        }
        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<EntityVersionTenant>())
            {
                switch (entry.State)
                {
                    case EntityState.Detached:
                        break;                    
                    case EntityState.Modified:
                        break;
                    case EntityState.Added:
                        break;                    
                }
            }
            return base.SaveChanges();
        }
    }
}
