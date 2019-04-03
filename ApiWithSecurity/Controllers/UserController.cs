using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApiWithSecurity.Entity;
using ApiWithSecurity.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace ApiWithSecurity.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var newUser = new ApplicationUser
            {
                Name = model.Name,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserName = model.Email
            };
            var createResult = await _userManager.CreateAsync(newUser, model.Password);
            if (createResult.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                return Ok(new
                {
                    user.Name,
                    user.Id,
                    user.Email,
                    user.PhoneNumber
                });
            }
            return BadRequest();
        }

        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            var userEmail = User.Claims.FirstOrDefault(i=> i.Type == ClaimTypes.NameIdentifier).Value;

            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                return BadRequest();
            }
            var changeResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!changeResult.Succeeded)
            {
                return BadRequest();
            }
            return Ok("Password has been changed");

        }
    }
}