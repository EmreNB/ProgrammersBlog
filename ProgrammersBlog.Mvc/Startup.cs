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
            //    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null) // Jqgrid içinde Json seriazalize kullanilirken nesnelerin büyük küçük karakter yapisini JsonNamingPolicy.CamelCase yapiyordu grid üzerinde eslesme sorunu çikiyordu. bundan dolayi default null atandi.
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
                    HttpOnly = true,//kullanýcýnýn js ile bizim cookie bilgilerimizi görmesini engelliyoruz
                    SameSite = SameSiteMode.Strict, //cookie bilgileri sadece kendi sitemizden geldiðinde iþlensin
                    SecurePolicy = CookieSecurePolicy.SameAsRequest //always olmalý 
                };
                options.SlidingExpiration = true; //kullanýcý sitemize girdiðinde süre tanýnýyor
                options.ExpireTimeSpan = System.TimeSpan.FromDays(7); // 7 gün tekrar giriþ gerekmeyecek
                options.AccessDeniedPath = new PathString("/Admin/User/AccessDenied"); //yetkisiz eriþim
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();//404 not found hatasý
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
