using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using SteamTower;
using System;
using System.IO;
using System.Reflection;
using TowerApiLib.Config;
using TowerApiLib.Logic;
using TowerApiLib.Repository;
using TowerApiLib.Services;

namespace Website {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(x => x.LoginPath = new PathString("/Account/Login"));

            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.Configure<AppSettings>(Configuration.GetSection("appSettings"));
            services.Configure<AppSecrets>(Configuration.GetSection("appSecrets"));

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo {
                    Title = "Tower API Documentation",
                    Version = "v1"
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });

            AppSettings appSettings = Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();
            AppSecrets appSecrets = Configuration.GetSection(nameof(AppSecrets)).Get<AppSecrets>();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Debug()
                .CreateLogger();

            services.AddScoped<UnitOfWork>();
            services.AddScoped<LogicService>();
            services.AddSingleton(x => new SteamApiClient(appSecrets.SteamApiKey));
            services.AddSingleton<MemoryCacheService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                //app.UseHttpsRedirection();
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseStatusCodePagesWithReExecute("/Error/Code/{0}");

            app.UseStaticFiles();
            app.UseSerilogRequestLogging();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapDefaultControllerRoute();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("../swagger/v1/swagger.json",
                                  "Tower API v1");
                c.DocumentTitle = "Tower API Documentation - Swagger UI";
                c.InjectStylesheet("/swagger.css");
            });
        }
    }
}
