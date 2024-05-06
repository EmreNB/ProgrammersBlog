using ProgrammersBlog.Entities.Concrete;
using System.Collections.Generic;

namespace ProgrammersBlog.Mvc.Areas.Admin.Models
{
    public class DashboardViewModel
    {
        public int CategoriesCount { get; set; }
        public int ArticlesCount { get; set; }
        //public int CommentsCount { get; set; }
        public int UsersCount { get; set; }
        public IList<Article> Articles { get; set; }

    }
}
