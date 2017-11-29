using Microsoft.EntityFrameworkCore;
using MemoryDatabase.Entities;

namespace MemoryDatabase
{
    public class MemoryContext : DbContext
    {
        public MemoryContext(DbContextOptions<MemoryContext> options)
            : base(options)
        {

        }

        public DbSet<Todo> Todos { get; set; }
    }
}
