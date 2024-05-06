using Microsoft.EntityFrameworkCore;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Data.Concrete.EntitiyFramework.Contexts;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Shared.Data.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Data.Concrete.EntitiyFramework.Repositories
{
    public class EfArticleRepository: EfEntityRepositoryBase<Article>,IArticleRepository
    {
        public EfArticleRepository(DbContext context):base(context)
        {
        }

        public IList<ArticleDetailsDto> GetAllQuaryable()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProgrammersBlogContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ProgrammersBlog;Trusted_Connection=True;Connect Timeout=30;MultipleActiveResultSets=True;");

            //_db = new ProgrammersBlogContext(optionsBuilder.Options)
            using (ProgrammersBlogContext context = new ProgrammersBlogContext(optionsBuilder.Options))
            {
                var query = from a in context.Articles
                            join c in context.Categories on a.CategoryId equals c.Id
                            where !a.IsDeleted
                            select new ArticleDetailsDto
                            {
                                Id = a.Id,
                                Title = a.Title,
                                CategoryName = c.Name,
                                Content = a.Content,
                                Author = a.SeoAuthor
                            };
                return query.ToList();
            }
        }
    }
}
