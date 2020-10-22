using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BuyABit.Interfaces;
using BuyABit.Models;
using BuyABit.Models.AddressFactory;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NSwag.Generation.Processors;
using StackExchange.Redis;

namespace BuyABit.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private static AppSettings _appSettings;
        private static ConnectionStrings _connectionStrings;
        public static void SetConfigurationSettings(this IServiceCollection services, IConfiguration configuration)
        {
            IConfigurationSection applicationSettingsConfiguration = configuration.GetSection("ApplicationSettings");
            services.Configure<AppSettings>(applicationSettingsConfiguration);
            _appSettings = applicationSettingsConfiguration.Get<AppSettings>();

            IConfigurationSection connectionStringsConfiguration = configuration.GetSection("ConnectionStrings");
            services.Configure<ConnectionStrings>(connectionStringsConfiguration);
            _connectionStrings = connectionStringsConfiguration.Get<ConnectionStrings>();
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            return services.AddDbContext<FullApplicationContext>(options => options
                    .UseSqlServer(_connectionStrings.SQLConnection));
        }

        public static IServiceCollection AddCaching(this IServiceCollection services)
        {
            ConfigurationOptions config = new ConfigurationOptions
            {
                EndPoints =
                    {
                        { _appSettings.RedisHostName, _appSettings.RedisPort }
                    },
                CommandMap = CommandMap.Create(new HashSet<string>
                    { // EXCLUDE a few commands
                        "INFO", "CONFIG", "CLUSTER",
                        "PING", "ECHO", "CLIENT"
                    }, available: false),
                KeepAlive = 180,
                DefaultVersion = new Version(2, 8, 8)
                //    Password = "changeme"
            };

            ConnectionMultiplexer cm = ConnectionMultiplexer.Connect(config);
      
            return services.AddSingleton<IConnectionMultiplexer>(cm);
        }

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddDefaultIdentity<ApplicationUser>().AddEntityFrameworkStores<FullApplicationContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 8;
            });

            return services;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
        {
            byte[] key = Encoding.ASCII.GetBytes(_appSettings.JSWSecret);

            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = true;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IDatabaseService, DatabaseService>();
            services.AddTransient<IApiProviderService, ApiProviderService>();
            services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
            services.AddTransient<AddressFactory, ConcreteAddressFactory>();
            services.AddSingleton<ICacheService, CacheService>();
            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "BuyABit API";
                    document.Info.Description = "BuyABit web API endpoints";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "BuyABit",
                        Email = "BuyABit@example.com",
                        Url = "https://twitter.com"
                    };
                    document.Info.License = new NSwag.OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = "https://example.com/license"
                    };
                };
            });
        }
    }
}
