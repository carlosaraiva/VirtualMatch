﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualMatch.Business.Interfaces;
using VirtualMatch.Business.Mapper;
using VirtualMatch.Business.Services;
using VirtualMatch.Business.Services.Interface;

namespace VirtualMatch.Business
{
    public static class ServiceInjector
    {
        public static void SetBusinessInjection(IServiceCollection services)
        {

            services.AddScoped<AccountsService>();
            services.AddScoped<TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<ILikesService, LikesService>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
        }
    }
}
