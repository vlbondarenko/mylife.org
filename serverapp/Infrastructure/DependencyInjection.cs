﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MediatR;

using Infrastructure.Identity;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Infrastructure.Identity.Interfaces;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddDbContext<IdentityDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("IdentityDatabase")));

            services.AddIdentityCore<AppUser>(config =>
            {
                config.Password.RequireDigit = false;
                config.Password.RequireLowercase = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
            })
              .AddEntityFrameworkStores<IdentityDbContext>()
              .AddUserManager<UserManager<AppUser>>()
              .AddSignInManager<SignInManager<AppUser>>()
              .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                 .AddJwtBearer(options =>
                 {
                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuerSigningKey = true,
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"])),
                         ValidateAudience = false,
                         ValidateIssuer = false
                     };
                 });

            services.AddHttpContextAccessor();

            services.AddScoped<ITokenClaimsService, IdentityTokenClaimService>();
            services.AddScoped<IUserManager, UserManager>();

            return services;
        }

        public static IServiceCollection AddInfrastructureServices (this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<EmailOptions>(configuration.GetSection("EmailOptions"));
            services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }
}