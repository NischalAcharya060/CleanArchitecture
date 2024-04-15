using Domain.StudentCRUD;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Presentation.StudentCRUD.Controllers
{
    
    
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new AppUser { UserName = model.Email, Email = model.Email };

            // Check if the specified role exists
            var roleExists = await _roleManager.RoleExistsAsync(model.Role);
            if (!roleExists)
            {
                // If the role doesn't exist, return error
                return BadRequest("Invalid role specified.");
            }

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                // Assign the specified role to the user
                await _userManager.AddToRoleAsync(user, model.Role);



                return Ok("User registered successfully.");
            }

            return BadRequest(result.Errors);
        }

        [HttpGet]
        [Route("GetUser")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok("User deleted successfully.");
            }
            return BadRequest(result.Errors);
        }

        [HttpPut]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser(string userId, RegisterViewModel registerViewModel)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            user.Email = registerViewModel.Email;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok("User updated successfully.");
            }
            else
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(errors);
            }
        }

        [HttpPut]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword(string userId, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            // Change the user password
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            if (result.Succeeded)
            {
                return Ok("Password changed successfully.");
            }
            else
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(errors);
            }
        }





        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
