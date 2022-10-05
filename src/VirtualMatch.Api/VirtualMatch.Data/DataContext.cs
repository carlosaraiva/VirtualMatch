using Microsoft.EntityFrameworkCore;
using System;
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
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                string connStr;

                // Depending on if in development or production, use either Heroku-provided
                // connection string, or development connection string from env var.
                if (env == "Development")
                {
                    // Use connection string from file.
                    connStr = "Server=localhost; User Id=postgres; Password=123; Database=virtualmatch";

                    connStr = "Server=ec2-107-23-76-12.compute-1.amazonaws.com;Port=5432;User Id=kzegiepnzondsv;Password=7a9aa200b263dcc81471deb34c81cb26133df6600aa521df514f757094dfb879;Database=de6q2g3fl6rql5;SSL=true;SSL Mode=Require;TrustServerCertificate=True";
                }
                else
                {
                    // Use connection string provided at runtime by Heroku.
                    /*var connUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

                    // Parse connection URL to connection string for Npgsql
                    connUrl = connUrl.Replace("postgres://", string.Empty);
                    var pgUserPass = connUrl.Split("@")[0];
                    var pgHostPortDb = connUrl.Split("@")[1];
                    var pgHostPort = pgHostPortDb.Split("/")[0];
                    var pgDb = pgHostPortDb.Split("/")[1];
                    var pgUser = pgUserPass.Split(":")[0];
                    var pgPass = pgUserPass.Split(":")[1];
                    var pgHost = pgHostPort.Split(":")[0];
                    var pgPort = pgHostPort.Split(":")[1];

                    connStr = $"Server={pgHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb};SSL Mode=Require;TrustServerCertificate=True";*/

                    connStr = "Server=ec2-107-23-76-12.compute-1.amazonaws.com;Port=5432;User Id=kzegiepnzondsv;Password=7a9aa200b263dcc81471deb34c81cb26133df6600aa521df514f757094dfb879;Database=de6q2g3fl6rql5;SSL=true;SSL Mode=Require;TrustServerCertificate=True";
                }

                // Whether the connection string came from the local development configuration file
                // or from the environment variable from Heroku, use it to set up your DbContext.
                options.UseNpgsql(connStr);

                //options.UseNpgsql("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=VirtualMatch;Data Source=localhost\\SQLEXPRESS");
                options.UseNpgsql();
            }

        }

        public DbSet<User> Users { get; set; }

        public DbSet<UserLike> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserLike>().HasKey(k => new { k.SourceUserId, k.LikedUserId });

            builder.Entity<UserLike>().HasOne(s => s.SourceUser).WithMany(l => l.LikedUsers).HasForeignKey(s => s.SourceUserId).OnDelete(DeleteBehavior.NoAction);

            builder.Entity<UserLike>().HasOne(s => s.LikedUser).WithMany(l => l.LikedByUsers).HasForeignKey(s => s.LikedUserId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
