using Microsoft.EntityFrameworkCore;

namespace ContextBase
{
    
    public class DataContext : DbContext
    {
        public readonly ITenantFactory TenantFactory;
        public DataContext()
        {
            TenantFactory = new TenantFactory();
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
