using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using WebAppTest.Data;

namespace WebAppTest.Admin.Pages_Roles
{
    public class EditRoleClaimModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public EditRoleClaimModel(RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _context = context;
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
        public IdentityRoleClaim<string>? RoleClaim { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            if (id == null)
            {
                ModelState.AddModelError(string.Empty, "Không tìm thấy role");
                return Page();
            }

            RoleClaim = await _context.RoleClaims.FindAsync(id);
            if (RoleClaim == null)
            {
                ModelState.AddModelError(string.Empty, "Không tìm thấy role claim");
                return Page();
            }

            Role = await _roleManager.FindByIdAsync(RoleClaim.RoleId);
            if (Role == null)
            {
                ModelState.AddModelError(string.Empty, "Không tìm thấy role");
                return Page();
            }

            Input = new InputModel
            {
                ClaimType = RoleClaim.ClaimType,
                ClaimValue = RoleClaim.ClaimValue
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            RoleClaim = await _context.RoleClaims.FindAsync(id);
            if (RoleClaim == null)
            {
                ModelState.AddModelError(string.Empty, "Không tìm thấy role claim");
                return Page();
            }

            Role = await _roleManager.FindByIdAsync(RoleClaim.RoleId);
            if (Role == null)
            {
                ModelState.AddModelError(string.Empty, "Không tìm thấy role");
                return Page();
            }

            if ((_context.RoleClaims.Any(c =>
                    c.RoleId == Role.Id &&
                    c.ClaimType == Input.ClaimType && 
                    c.ClaimValue == Input.ClaimValue && 
                    c.Id != RoleClaim.Id)))
            {
                ModelState.AddModelError(string.Empty, "Claim đã tồn tại");
                return Page();
            }

            RoleClaim.ClaimType = Input.ClaimType;
            RoleClaim.ClaimValue = Input.ClaimValue;
            await _context.SaveChangesAsync();

            return RedirectToPage("./Edit", new { id = Role.Id });
        }

        public async Task<IActionResult> OnPostDeleteAsync(int? id)
        {
            RoleClaim = await _context.RoleClaims.FindAsync(id);
            if (RoleClaim == null)
            {
                ModelState.AddModelError(string.Empty, "Không tìm thấy role claim");
                return Page();
            }

            Role = await _roleManager.FindByIdAsync(RoleClaim.RoleId);
            if (Role == null)
            {
                ModelState.AddModelError(string.Empty, "Không tìm thấy role");
                return Page();
            }

            await _roleManager.RemoveClaimAsync(Role, new Claim(RoleClaim.ClaimType, RoleClaim.ClaimValue));

            return RedirectToPage("./Edit", new { id = Role.Id });
        }
    }
}
