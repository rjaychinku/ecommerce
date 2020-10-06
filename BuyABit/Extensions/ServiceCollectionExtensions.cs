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
        private static AppSettings appSettings;

        public static AppSettings GetApplicationSettings(this IServiceCollection services,
            IConfiguration configuration)
        {
            IConfigurationSection applicationSettingsConfiguration = configuration.GetSection("ApplicationSettings");
            services.Configure<AppSettings>(applicationSettingsConfiguration);
            appSettings = applicationSettingsConfiguration.Get<AppSettings>();
            return appSettings;
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContext<FullApplicationContext>(options => options
                    .UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }

        public static IServiceCollection AddCaching(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigurationOptions config = new ConfigurationOptions
            {
                EndPoints =
                    {
                        { appSettings.RedisHostName, appSettings.RedisPort }
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
            services.AddSingleton<ICacheService, CacheService>();
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

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services,
            AppSettings appSettings)
        {
            byte[] key = Encoding.ASCII.GetBytes(appSettings.JSWSecret);

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

        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddTransient<IIdentityService, IdentityService>();

            services.AddTransient<IDatabaseService, DatabaseService>();

            services.AddTransient<IApiProviderService, ApiProviderService>();

            services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();

            services.AddTransient<AddressFactory, ConcreteAddressFactory>();


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
                        Name = "justChinks",
                        Email = "justchinks@gmail.com",
                        Url = "https://twitter.com/justchinks"
                    };
                    document.Info.License = new NSwag.OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = "https://example.com/license"
                    };
                };
            });
            //return services.AddOpenApiDocument(document =>
            //    {

            //        document.DocumentName = "BuyABit API";
            //        document.PostProcess = d =>
            //        {
            //            d.Info.Title = "BuyABit API";
            //            d.Info.Description = "REST interface for BuyABit Api.";
            //            d.Info.Version = "v1";
            //        };
            //        document.OperationProcessors.Add(new ApiVersionProcessor { IncludedVersions = new[] { "1.0" } });
            //    });
        }
    }
}
