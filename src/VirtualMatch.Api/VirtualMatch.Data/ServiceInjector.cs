using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualMatch.Data;
using VirtualMatch.Data.Interfaces;
using VirtualMatch.Data.Repositories;

namespace VirtualMatch.Data
{
    public static class ServiceInjector
    {
        public static void SetDataInjection(IServiceCollection services, string connection)
        {
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(connection);
            });

            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
