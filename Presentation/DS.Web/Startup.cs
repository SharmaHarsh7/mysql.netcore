﻿using DS.Core.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nop.Web.Framework.Infrastructure.Extensions;
using Serilog;
using System;

namespace DS.Web
{
    public class Startup
    {

        public IConfigurationRoot Configuration { get; }

        #region Ctor

        public Startup(IHostingEnvironment environment)
        {
            //create configuration
            Configuration = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            string mongoURL = Configuration.GetSection("MongoConnection:ConnectionString").Value + Configuration.GetSection("MongoConnection:Database").Value;

            Log.Logger = new LoggerConfiguration()
           .WriteTo.File("logs\\myapp.txt", Serilog.Events.LogEventLevel.Verbose, rollingInterval: RollingInterval.Day)
           .WriteTo.MongoDB(mongoURL, restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information, collectionName: "Logs")
           .CreateLogger();

            Log.Information("Logger Setup Done");

        }

        #endregion

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            return services.ConfigureApplicationServices(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            EngineContext.Current.ConfigureRequestPipeline(app);

        

            //app.UseExceptionHandler(
            //                           options =>
            //                           {
            //                               options.Run(
            //                             async context =>
            //                              {
            //                                  context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //                                  context.Response.ContentType = "text/html";
            //                                  var ex = context.Features.Get<IExceptionHandlerFeature>();
            //                                  if (ex != null)
            //                                  {
            //                                      var err = $"<h1>Error: {ex.Error.Message}</h1>{ex.Error.StackTrace }";
            //                                      await context.Response.WriteAsync(err).ConfigureAwait(false);
            //                                  }
            //                              });
            //                           }
            //                          );

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //    app.UseBrowserLink();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Error");
            //}

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
