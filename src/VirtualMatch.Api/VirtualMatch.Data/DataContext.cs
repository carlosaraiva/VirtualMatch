using Microsoft.EntityFrameworkCore;
using VirtualMatch.Entities.Database;

namespace VirtualMatch.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {

            if (!options.IsConfigured)
            {
                options.UseSqlServer("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=VirtualMatch;Data Source=localhost\\SQLEXPRESS");
            }

        }

        public DbSet<User> Users { get; set; }
    }
}
