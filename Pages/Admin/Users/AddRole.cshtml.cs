// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppTest.Models;

namespace WebAppTest.Admin.Pages_Users
{
    [Authorize(Roles = "Admin")]
    public class AddRoleModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AddRoleModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public User User { get; set; }
        [BindProperty]
        [DisplayName("Role")]
        public List<string> Roles { get; set; }
        public SelectList RoleList { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound($"Unable to load user.");
            }

            User = await _userManager.FindByIdAsync(id);
            if (User == null)
            {
                return NotFound($"Unable to load user with ID '{id}'.");
            }

            Roles = (await _userManager.GetRolesAsync(User)).ToList();

            RoleList = new SelectList(await _roleManager.Roles.ToListAsync(), "Name", "Name");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (string.IsNullOrEmpty(id))
            {
                return NotFound($"Unable to load user.");
            }

            User = await _userManager.FindByIdAsync(id);
            if (User == null)
            {
                return NotFound($"Unable to load user with ID '{id}'.");
            }

            RoleList = new SelectList(await _roleManager.Roles.ToListAsync(), "Name", "Name");

            // Add the user to the selected roles
            var oldRoles = (await _userManager.GetRolesAsync(User)).ToList();
            var deleteRoles = oldRoles.Except(Roles).ToList();
            var addRoles = Roles.Except(oldRoles).ToList();

            var rsDelete = await _userManager.RemoveFromRolesAsync(User, deleteRoles);
            if (!rsDelete.Succeeded)
            {
                foreach (var error in rsDelete.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            var rsAdd = await _userManager.AddToRolesAsync(User, addRoles);
            if (!rsAdd.Succeeded)
            {
                foreach (var error in rsAdd.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            return RedirectToPage();
        }
    }
}
