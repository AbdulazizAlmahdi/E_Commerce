using E_commerce.Infersructure;
using E_commerce.Infersructure.Interface;
using E_commerce.Models;
//using E_commerce.Models.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace E_commerce
{
    public class Startup
    {
        public IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var host = Environment.GetEnvironmentVariable("DB_HOST");
            var dbname = Environment.GetEnvironmentVariable("DB_NAME");
            var sapass = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
            services.AddControllersWithViews();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContext<WebContext>(options => options.UseSqlServer(Configuration.GetConnectionString("E_CommerceDB")));
            // services.AddDbContext<WebContext>(options => options.UseSqlServer("Data Source="+host+";Initial Catalog="+dbname+";User Id=sa;MultipleActiveResultSets=true;Password="+sapass));
            /* 
            services.AddScoped<IRepository<Place>, PlaceRepository>();
           services.AddScoped<IRepository<Phone>, PhoneRepository>();
             services.AddScoped<IRepository<Product>, ProductRepository>();
             services.AddScoped<IRepository<ImagesProduct>, ImageProductRepository>();
             services.AddScoped<IRepository<Category>, CategoryRepository>();
             services.AddScoped<IRepository<Purchase>, PurchaseRepository>();
             services.AddScoped<IRepository<Help>, HelpRepository>();
             services.AddScoped<IRepository<Auction>, AuctionsRepository>();
            services.AddScoped<IRepository1<Category>, Repository1<Category>>();  
             services.AddScoped<IAnalyticsRepository, AnalyticsRepository>();
             services.AddScoped<IRepository<User>, UserRepository>();*/

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            /*  services.AddDbContext<WebContext>(options =>
              {
                  options.UseSqlServer(Configuration.GetConnectionString("E_CommerceDB"));
              });*/
            services.AddRazorPages();
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    // Use the default property (Pascal) casing
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WebContext webContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseDefaultFiles();
            app.UseRouting();
            app.UseSession();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "areas",
                   pattern: "{area:exists}/{controller=Analytics}/{action=Index}/{id?}");


                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            SeedData.Seeddb(webContext);
        }
    }
}
