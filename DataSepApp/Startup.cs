using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abstractions;
using DataSepApp.Licenses;
using DataSepApp.Widgets;
using DataTypes;
using FakeDataStore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SrsBidness.Widgets;

namespace DataSepApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = "UseDevelopmentStorage=true";


            services.AddMvc();

            // method one
            services.AddTransient<ILicenseManager, LicenseManager>();
            services.AddTransient<ILicenseStore, LicenseStore2>();

            // method two
            services.AddTransient<IWidgetDataManager, WidgetDataManager>();
            //services.AddTransient<IDataStore<Widget, int, WidgetSearchRequest>, FakeWidgetDataStore>();
            services.AddTransient<IDataStore<Widget, int, WidgetSearchRequest>, AzureStorageDataStore.AzureTableStorageWidgetDataStore>((sp) => { return new AzureStorageDataStore.AzureTableStorageWidgetDataStore(connectionString); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
