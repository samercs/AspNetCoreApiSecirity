using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ApiWithSecurity.Entity;
using ApiWithSecurity.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ApiWithSecurity.Controllers
{
    [ApiController]
    public class TokenController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public TokenController(IConfiguration configuration, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [Route("api/token")]
        [HttpPost]
        public async Task<IActionResult> Create(LoginViewModel model)
        {
            var loginResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, true);
            if (loginResult.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.UserName);
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.UserName)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtBearerKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["JwtIssuer"],
                    audience: _configuration["JwtAudience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(int.Parse(_configuration["JwtTokenExpire"])),
                    signingCredentials: creds);

                return Ok(new {token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            return BadRequest();
        }
    }
}