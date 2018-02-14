using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using DS.Core.Configuration;
using DS.Core.Infrastructure;
using DS.Data;
using DS.Web.Framework.Filters;

using Microsoft.EntityFrameworkCore;


using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using DS.Core.Data;
using Serilog;

namespace Nop.Web.Framework.Infrastructure.Extensions
{
    /// <summary>
    /// Represents extensions of IServiceCollection
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add services to the application and configure service provider
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Configuration root of the application</param>
        /// <returns>Configured service provider</returns>
        public static IServiceProvider ConfigureApplicationServices(this IServiceCollection services, IConfigurationRoot configuration)
        {

            Log.Information("ConfigureApplicationServices : Begin");

            services.AddNSMvc();
            services.AddOptions();

            // setup database connection
            services.Configure<DBSettings>(options =>
            {
                options.ConnectionString = configuration.GetSection("SQLConnection:ConnectionString").Value;
                options.Database = configuration.GetSection("SQLConnection:Database").Value;
            });

            services.ConfigureStartupConfig<HostingConfig>(configuration.GetSection("Hosting"));

            services.AddDbContext<DbContext>(options => options.UseMySql(configuration.GetSection("SQLConnection:ConnectionString").Value));

            // Load NS Config from the appsetting.json file
            services.ConfigureStartupConfig<DSConfig>(configuration.GetSection("DSConfig"));
            services.ConfigureStartupConfig<MongoConfig>(configuration.GetSection("MongoConnection"));


            // Load Auth Config from the appsetting.json file
            var authConfig = services.ConfigureStartupConfig<AuthConfig>(configuration.GetSection("AuthConfig"));

            //add accessor to HttpContext
            services.AddHttpContextAccessor();

            services.ConfigureJwtAuthService(authConfig);

            //create, initialize and configure the engine
            var engine = EngineContext.Create();
            engine.Initialize(services);
            var serviceProvider = engine.ConfigureServices(services, configuration);
          //  throw new Exception("Test Exception ");

            //if (DataSettingsHelper.DatabaseIsInstalled())
            //{
            //    //implement schedule tasks
            //    //database is already installed, so start scheduled tasks
            //    //TaskManager.Instance.Initialize();
            //    //TaskManager.Instance.Start();

            //    //log application start
                Log.Information("Application started");
            //}

            return serviceProvider;
        }


        /// <summary>
        /// Register HttpContextAccessor
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void AddHttpContextAccessor(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }


        /// <summary>
        /// Create, bind and register as service the specified configuration parameters 
        /// </summary>
        /// <typeparam name="TConfig">Configuration parameters</typeparam>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Set of key/value application configuration properties</param>
        /// <returns>Instance of configuration parameters</returns>
        public static TConfig ConfigureStartupConfig<TConfig>(this IServiceCollection services, IConfiguration configuration) where TConfig : class, new()
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            //create instance of config
            var config = new TConfig();

            //bind it to the appropriate section of configuration
            configuration.Bind(config);

            //and register it as a service
            services.AddSingleton(config);

            return config;
        }

        /// <summary>
        /// Add and configure MVC for the application
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <returns>A builder for configuring MVC services</returns>
        public static IMvcBuilder AddNSMvc(this IServiceCollection services)
        {
            
            //add basic MVC feature
            var mvcBuilder = services.AddMvc(
                               config =>
                               {
                                   config.Filters.Add(typeof(DSExceptionFilter));
                               });

            //use session temp data provider
            mvcBuilder.AddSessionStateTempDataProvider();

            //MVC now serializes JSON with camel case names by default, use this code to avoid it
            mvcBuilder.AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
            mvcBuilder.AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);




            return mvcBuilder;
        }

        public static void ConfigureJwtAuthService(this IServiceCollection services, AuthConfig authConfig)
        {

            var symmetricKeyAsBase64 = authConfig.Secret;
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);

            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!  
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                //// Validate the JWT Issuer (iss) claim  
                ValidateIssuer = true,
                ValidIssuer = authConfig.Issuer,

                //// Validate the JWT Audience (aud) claim  
                ValidateAudience = true,
                ValidAudience = authConfig.Audience,

                // Validate the token expiry  
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = tokenValidationParameters;
            });
        }



    }
}
