using Microsoft.Extensions.DependencyInjection;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Data.Concrete;
using ProgrammersBlog.Data.Concrete.EntitiyFramework.Contexts;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Services.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgrammersBlog.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace ProgrammersBlog.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection serviceCollection,string connectionString)
        {
            serviceCollection.AddDbContext<ProgrammersBlogContext>(options=>options.UseSqlServer(connectionString));
            serviceCollection.AddIdentity<User, Role>(options =>
            {
                // kullanıcıların şifre ayarları

                options.Password.RequireDigit=false; //şifrede rakam bulunsun mu?
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 0; //unique karakterlerden kaç tane olmalı?
                options.Password.RequireNonAlphanumeric = false; //özel karakterlerin zorunlu olmasını istiyor musun?
                options.Password.RequireLowercase=false; //küçük karakterler zorunlu kılınsın mı?
                options.Password.RequireUppercase = false;

                //kullanıcıların isim ve email ayarları

                options.User.AllowedUserNameCharacters= "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+$";
                options.User.RequireUniqueEmail = true; //tüm emailler eşsiz mi?
            }).AddEntityFrameworkStores<ProgrammersBlogContext>();
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();//yapılan her requestte nesne tekrar oluşur ve bir request
                                                                   //içerisinde sadece bir tane nesne kullanılır.
            serviceCollection.AddScoped<ICategoryService, CategoryManager>();
            serviceCollection.AddScoped<IArticleService, ArticleManager>();
            return serviceCollection;
        }
    }
}
