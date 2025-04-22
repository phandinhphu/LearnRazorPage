using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppTest.Models;

namespace WebAppTest.Admin.Pages_Users
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly UserManager<User> _userManager;

        public IndexModel(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public class UserAndRoles : User
        {
            public string? Roles { get; set; }
        }

        public List<UserAndRoles> Users { get; set; } = new List<UserAndRoles>();

        public async Task OnGet()
        {
            var users = await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                var userAndRoles = new UserAndRoles
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = string.Join(", ", await _userManager.GetRolesAsync(user)),
                };
                Users.Add(userAndRoles);
            }
        }
    }
}
