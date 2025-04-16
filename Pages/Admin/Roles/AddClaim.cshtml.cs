using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace WebAppTest.Admin.Pages_Roles
{
    public class AddClaimModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public AddClaimModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public class InputModel
        {
            [Display(Name = "Claim Type")]
            [Required(ErrorMessage = "ClaimType is required.")]
            [StringLength(256, MinimumLength = 3, ErrorMessage = "Role name cannot be longer than 256 characters.")]
            public string? ClaimType { get; set; }

            [Display(Name = "Claim Value")]
            [Required(ErrorMessage = "ClaimValue is required.")]
            [StringLength(256, MinimumLength = 3, ErrorMessage = "Role name cannot be longer than 256 characters.")]
            public string? ClaimValue { get; set; }
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();
        public IdentityRole? Role { get; set; }

        public async Task<IActionResult> OnGet(string id)
        {
            if (id == null)
            {
                ModelState.AddModelError(string.Empty, "Không tìm thấy role");
                return Page();
            }
            Role = await _roleManager.FindByIdAsync(id);
            if (Role == null)
            {
                ModelState.AddModelError(string.Empty, "Không tìm thấy role");
                return Page();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Role = await _roleManager.FindByIdAsync(id);
            if (Role == null)
            {
                ModelState.AddModelError(string.Empty, "Không tìm thấy role");
                return Page();
            }

            if ((await _roleManager.GetClaimsAsync(Role)).Any(c => c.Type == Input.ClaimType && c.Value == Input.ClaimValue))
            {
                ModelState.AddModelError(string.Empty, "Claim đã tồn tại");
                return Page();
            }

            var result = await _roleManager.AddClaimAsync(Role, new Claim(Input.ClaimType, Input.ClaimValue));

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return RedirectToPage("./Edit", new { id = Role.Id });
        }
    }
}
