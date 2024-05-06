using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Mvc.Areas.Admin.Models;

namespace ProgrammersBlog.Mvc.Areas.Admin.ViewComponents
{
    public class AdminMenuViewComponent:ViewComponent
    {
        private readonly UserManager<User> _usermanager;

        public AdminMenuViewComponent(UserManager<User> usermanager)
        {
            _usermanager = usermanager;
        }

        public ViewViewComponentResult Invoke()
        {
            var user = _usermanager.GetUserAsync(HttpContext.User).Result;
            var roles = _usermanager.GetRolesAsync(user).Result;
            return View(new UserWithRolesViewModel
            {
                User = user,
                Roles = roles
            });
        }
    }
}
