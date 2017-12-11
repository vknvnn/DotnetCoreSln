using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;
using PostgreSqlDemo.Entities.Tables;

namespace PostgreSqlDemo.Context
{
    public class PortgreSqlDemoContext : DbContext
    {
        

        public virtual DbSet<Student> Students { get; set; }

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Host=localhost;Database=postgresql_demo;Username=postgres;Password=12345^");
            #if DEBUG
            LoggerFactory loggerDebugFactory = new LoggerFactory(new[] { new DebugLoggerProvider((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information) });
            LoggerFactory loggerConsoleFactory = new LoggerFactory(new[] { new ConsoleLoggerProvider((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information, true) });
            optionsBuilder.UseLoggerFactory(loggerDebugFactory);
            optionsBuilder.UseLoggerFactory(loggerConsoleFactory);
            #endif
        }
        
    }
}
