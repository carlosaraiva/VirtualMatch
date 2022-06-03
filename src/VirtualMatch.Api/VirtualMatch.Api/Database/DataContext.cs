using Microsoft.EntityFrameworkCore;
using VirtualMatch.Entities;

namespace VirtualMatch.Api.Database
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
