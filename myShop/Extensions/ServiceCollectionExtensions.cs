using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using myShop.Interfaces;
using myShop.Models;
using myShop.Models.AddressFactory;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NSwag.Generation.Processors;
using StackExchange.Redis;

namespace myShop.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static AppSettings GetApplicationSettings(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var applicationSettingsConfiguration = configuration.GetSection("ApplicationSettings");
            services.Configure<AppSettings>(applicationSettingsConfiguration);
            return applicationSettingsConfiguration.Get<AppSettings>();
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
                        { "localhost", 6379 }
                       // { "redis1", 6380 }
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

        public static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services,
            AppSettings appSettings)
        {
            byte[] key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
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

            services.AddAuthentication().AddTwitter(twitterOptions =>
            {
                twitterOptions.ConsumerKey = configuration["Authentication:Twitter:ConsumerKey"];
                twitterOptions.ConsumerSecret = configuration["Authentication:Twitter:ConsumerSecret"];
            });

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
            => services.AddOpenApiDocument(document =>
            {
                document.DocumentName = "myShop API";
                document.PostProcess = d =>
                {
                    d.Info.Title = "myShop API";
                    d.Info.Description = "REST interface for myShop Api.";
                    d.Info.Version = "v1";
                };
                document.OperationProcessors.Add(new ApiVersionProcessor { IncludedVersions = new[] { "1.0" } });
            });

        //public static void AddApiControllers(this IServiceCollection services)
        //    => services
        //        .AddControllers(options => options
        //            .Filters
        //            .Add<ModelOrNotFoundActionFilter>());
    }
}
