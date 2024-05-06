using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgrammersBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Data.Concrete.EntitiyFramework.Mappings
{
    public class ArticleMap : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();
            builder.Property(a => a.Title).HasMaxLength(100);
            builder.Property(a=>a.Title).IsRequired(true);
            builder.Property(a => a.Content).IsRequired();
            builder.Property(a => a.Content).HasColumnType("NVARCHAR(MAX)");
            builder.Property(a=>a.Date).IsRequired();
            builder.Property(a=>a.SeoAuthor).IsRequired();
            builder.Property(a=>a.SeoAuthor).HasMaxLength(50);
            builder.Property(a=>a.SeoDescription).HasMaxLength(150);
            builder.Property(a=>a.SeoDescription).IsRequired();
            builder.Property(a=>a.SeoTags).IsRequired();
            builder.Property(a=>a.SeoTags).HasMaxLength(70);
            builder.Property(a=>a.ViewsCount).IsRequired();
            builder.Property(a=>a.CommentCount).IsRequired();
            builder.Property(a=>a.Thumbnail).IsRequired();
            builder.Property(a=>a.Thumbnail).HasMaxLength(250);
            builder.Property(a=>a.CreatedByName).IsRequired();
            builder.Property(a=>a.CreatedByName).HasMaxLength(50);
            builder.Property(a=>a.ModifiedByName).IsRequired();
            builder.Property(a=>a.ModifiedByName).HasMaxLength(50);
            builder.Property(a=>a.CreatedDate).IsRequired();
            builder.Property(a=>a.ModifiedDate).IsRequired();
            builder.Property(a=>a.IsActive).IsRequired();
            builder.Property(a=>a.IsDeleted).IsRequired();
            builder.Property(a=>a.Note).HasMaxLength(500);
            builder.HasOne<Category>(a=>a.Category).WithMany(c=>c.Articles).HasForeignKey(a=>a.CategoryId);
            builder.HasOne<User>(a=>a.User).WithMany(u=>u.Articles).HasForeignKey(a=>a.UserId);
            builder.ToTable("Articles");
            //buradaki kayıtlar silindiği için hiç veri gelmiyor.
            //builder.HasData(
            //    new Article
            //    {
            //        Id = 1,
            //        CategoryId = 1,
            //        Title = "C# 9.0 ve .NET 5 yenilikleri",
            //        Content = "Lorem Ipsum, kısaca Lipsum, masaüstü yayıncılık ve basın yayın sektöründe kullanılan taklit yazı bloğu olarak tanımlanır. Lipsum, oluşturulacak şablon ve taslaklarda içerik yerine geçerek yazı bloğunu doldurmak için kullanılır.",
            //        Thumbnail = "Default.jpg",
            //        SeoDescription = "C# 9.0 ve .NET 5 yenilikleri",
            //        SeoTags = "C#, C# 9, .NET 5, NET Framework, .NET Core",
            //        SeoAuthor = "Ahmet Çiftçi",
            //        Date = DateTime.Now,
            //        IsActive = true,
            //        IsDeleted = false,
            //        CreatedByName = "InitialCreate",
            //        CreatedDate = DateTime.Now,
            //        ModifiedByName = "InitialCreate",
            //        ModifiedDate = DateTime.Now,
            //        Note = "C# 9.0 ve .NET 5 yenilikleri",
            //        UserId = 1,
            //        ViewsCount = 100,
            //        CommentCount = 1,
            //    },
            //    new Article
            //    {
            //        Id = 2,
            //        CategoryId = 2,
            //        Title = "C++ 11 ve 19 yenilikleri",
            //        Content = "Lorem Ipsum, 500 yıl boyunca varlığını sürdürmekle kalmamış ve günümüzde elektronik yazı tipinin gerektiği birçok konuda hazır bir araç olarak kullanılmaya başlanmıştır. Lipsum 1960'larda içinde Lorem Ipsum paragraflarının bulunduğu letrasetlerin piyasaya çıkması ve 1990'larda Lorem Ipsum versiyonlarını içeren Aldus Pagemaker gibi programlarla beraber yaygın hale gelmiştir.",
            //        Thumbnail = "Default.jpg",
            //        SeoDescription = "C++ 11 ve 19 yenilikleri",
            //        SeoTags = "C++ 11 ve 19 yenilikleri",
            //        SeoAuthor = "Ahmet Çiftçi",
            //        Date = DateTime.Now,
            //        IsActive = true,
            //        IsDeleted = false,
            //        CreatedByName = "InitialCreate",
            //        CreatedDate = DateTime.Now,
            //        ModifiedByName = "InitialCreate",
            //        ModifiedDate = DateTime.Now,
            //        Note = "C++ 11 ve 19 yenilikleri",
            //        UserId = 1,
            //        ViewsCount = 295,
            //        CommentCount = 1,
            //    },
            //    new Article
            //    {
            //        Id = 3,
            //        CategoryId = 3,
            //        Title = "JavaScript ES2019 ve ES2020 yenilikleri",
            //        Content = "Lorem Ipsum, 500 yıl boyunca varlığını sürdürmekle kalmamış ve günümüzde elektronik yazı tipinin gerektiği birçok konuda hazır bir araç olarak kullanılmaya başlanmıştır. Lipsum 1960'larda içinde Lorem Ipsum paragraflarının bulunduğu letrasetlerin piyasaya çıkması ve 1990'larda Lorem Ipsum versiyonlarını içeren Aldus Pagemaker gibi programlarla beraber yaygın hale gelmiştir.",
            //        Thumbnail = "Default.jpg",
            //        SeoDescription = "JavaScript ES2019 ve ES2020 yenilikleri",
            //        SeoTags = "JavaScript ES2019 ve ES2020 yenilikleri",
            //        SeoAuthor = "Ahmet Çiftçi",
            //        Date = DateTime.Now,
            //        IsActive = true,
            //        IsDeleted = false,
            //        CreatedByName = "InitialCreate",
            //        CreatedDate = DateTime.Now,
            //        ModifiedByName = "InitialCreate",
            //        ModifiedDate = DateTime.Now,
            //        Note = "JavaScript ES2019 ve ES2020 yenilikleri",
            //        UserId = 1,
            //        ViewsCount = 12,
            //        CommentCount = 1,
            //    }
            //    );

        }
    }
}
