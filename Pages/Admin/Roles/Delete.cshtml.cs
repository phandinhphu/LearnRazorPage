using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace WebAppTest.Admin.Pages_Roles
{
    public class DeleteModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public DeleteModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public class InputModel
        {
            [Display(Name = "Role Name")]
            [Required(ErrorMessage = "Role name is required.")]
            [StringLength(256, MinimumLength = 3, ErrorMessage = "Role name cannot be longer than 256 characters.")]
            public string? RoleName { get; set; }
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        public async Task<IActionResult> OnGet(string id)
        {
            if (id == null)
            {
                return NotFound("Không tìm thấy role");
            }
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound("Không tìm thấy role");
            }

            Input = new InputModel
            {
                RoleName = role.Name
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound("Không tìm thấy role");
            }
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound("Không tìm thấy role");
            } else
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToPage("./Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }
    }
}
