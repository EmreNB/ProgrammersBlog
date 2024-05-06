using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProgrammersBlog.Services.AutoMapper.Profiles;
using ProgrammersBlog.Services.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ProgrammersBlog.Mvc.AutoMapper.Profiles;
using Microsoft.Extensions.Configuration;
using ProgrammersBlog.Mvc.Helpers.Abstract;
using ProgrammersBlog.Mvc.Helpers.Concrete;

namespace ProgrammersBlog.Mvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration=configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                opt.JsonSerializerOptions.ReferenceHandler=ReferenceHandler.Preserve;
            });
            //services.AddControllers().AddNewtonsoftJson(options =>
            //    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            //);
            //var builderMvc = services.AddMvc()
            //    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null) // Jqgrid i�inde Json seriazalize kullanilirken nesnelerin b�y�k k���k karakter yapisini JsonNamingPolicy.CamelCase yapiyordu grid �zerinde eslesme sorunu �ikiyordu. bundan dolayi default null atandi.
            //    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);
            services.AddControllersWithViews().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddSession();
            services.AddAutoMapper(typeof(CategoryProfile),typeof(ArticleProfile),typeof(UserProfile));
            services.LoadMyServices(connectionString:Configuration.GetConnectionString("LocalDB"));
            services.AddScoped<IImageHelper, ImageHelper>();
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Admin/User/Login");
                options.LogoutPath = new PathString("/Admin/User/Logout");
                options.Cookie = new CookieBuilder
                {
                    Name="ProgrammersBlog",
                    HttpOnly = true,//kullan�c�n�n js ile bizim cookie bilgilerimizi g�rmesini engelliyoruz
                    SameSite = SameSiteMode.Strict, //cookie bilgileri sadece kendi sitemizden geldi�inde i�lensin
                    SecurePolicy = CookieSecurePolicy.SameAsRequest //always olmal� 
                };
                options.SlidingExpiration = true; //kullan�c� sitemize girdi�inde s�re tan�n�yor
                options.ExpireTimeSpan = System.TimeSpan.FromDays(7); // 7 g�n tekrar giri� gerekmeyecek
                options.AccessDeniedPath = new PathString("/Admin/User/AccessDenied"); //yetkisiz eri�im
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();//404 not found hatas�
            }

            app.UseSession();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication(); //sen kimsin?
            app.UseAuthorization(); //yetkilerin neler?

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "Admin",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
                    );
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
