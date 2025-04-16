using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAppTest.Admin.Pages_Roles
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public IndexModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public class RolesModel : IdentityRole
        {
            public string[] Claims { get; set; } = Array.Empty<string>();
        }

        public List<RolesModel> Roles { get; set; } = new List<RolesModel>();

        public async Task OnGet()
        {
            var roles = _roleManager.Roles.ToList();
            foreach (var role in roles)
            {
                var claims = await _roleManager.GetClaimsAsync(role);
                Roles.Add(new RolesModel
                {
                    Id = role.Id,
                    Name = role.Name,
                    Claims = claims.Select(c => c.Type + ": " + c.Value).ToArray()
                });
            }
        }
    }
}
