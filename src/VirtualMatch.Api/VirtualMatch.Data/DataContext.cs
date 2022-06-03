using Microsoft.EntityFrameworkCore;
using VirtualMatch.Entities;

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

                options.UseSqlite("Data source=virtualmatch.db");

            }

        }

        public DbSet<User> Users { get; set; }
    }
}
