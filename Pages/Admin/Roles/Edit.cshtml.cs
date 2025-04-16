using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WebAppTest.Data;

namespace WebAppTest.Admin.Pages_Roles
{
    public class EditModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public EditModel(RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _context = context;
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
        public IdentityRole? Role { get; set; }

        public List<IdentityRoleClaim<string>>? Claims { get; set; }

        public async Task<IActionResult> OnGet(string id)
        {
            if (id == null)
            {
                return NotFound("Không tìm thấy role");
            }
            Role = await _roleManager.FindByIdAsync(id);
            if (Role == null)
            {
                return NotFound("Không tìm thấy role");
            }

            Input = new InputModel
            {
                RoleName = Role.Name
            };
            Claims = await _context.RoleClaims
                    .Where(c => c.RoleId == Role.Id)
                    .ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (id == null)
            {
                return NotFound("Không tìm thấy role");
            }

            Role = await _roleManager.FindByIdAsync(id);

            if (Role == null)
            {
                return NotFound("Không tìm thấy role");
            } else
            {
                Claims = await _context.RoleClaims
                        .Where(c => c.RoleId == Role.Id)
                        .ToListAsync();
                Role.Name = Input.RoleName;
                var result = await _roleManager.UpdateAsync(Role);
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
