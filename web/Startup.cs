using entities;
using entities.config;
using entities.entities;
using entities.repository;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using services.commands.core;
using System;
using System.Globalization;
using web.Logger;

namespace web
{
    public class Startup
    {


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;


        }

        public IConfiguration Configuration { get; }

        private static void AddMediatr(IServiceCollection services)
        {
            const string applicationAssemblyName = nameof(services);
            var assembly = AppDomain.CurrentDomain.Load(applicationAssemblyName);

            AssemblyScanner.FindValidatorsInAssembly(assembly)
                .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FailFastRequestBehavior<,>));

            services.AddMediatR(assembly);
        }

        public void AddServices(IServiceCollection services)
        {
            services.AddScoped<IRepository<Employee>, EFRepository<Employee>>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            AddServices(services);

            //For postgres database 
            services.AddDbContext<EFAppContext>(optionsAction =>
            {
                optionsAction.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
            });


            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.SetIsOriginAllowed(_ => true)
                                      .AllowAnyMethod()
                                      .AllowAnyHeader()
                                      .AllowCredentials().Build());
            });

            services.Configure<RequestLocalizationOptions>(options =>
            {
#pragma warning disable CC0030 // Make Local Variable Constant.
                var cultureBr = "pt-BR";
#pragma warning restore CC0030 // Make Local Variable Constant.
                var suppCulture = new[]
                {
                    new CultureInfo(cultureBr)
                };

                options.DefaultRequestCulture = new RequestCulture(cultureBr);
                options.SupportedCultures = suppCulture;
                options.SupportedUICultures = suppCulture;
            });

            //Add MediatR
            AddMediatr(services);

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            //ILoggerFactory loggerFactory,
            EFAppContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }



            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            //loggerFactory.AddContext(LogLevel.Warning, Configuration.GetConnectionString("DefaultConnection"));


            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
