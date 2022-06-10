using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualMatch.Business.Interfaces;
using VirtualMatch.Business.Services;

namespace VirtualMatch.Business
{
    public static class ServiceInjector
    {
        public static void SetBusinessInjection(IServiceCollection services)
        {

            services.AddScoped<AccountsService>();
            services.AddScoped<TokenService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
