using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskProject.WebApi.DTOs;

namespace TaskProject.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private IConfiguration _config;

        public AuthenticationController(UserManager<IdentityUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ApiUser apiUser)
        {
            var m = _userManager.Users.ToList();
            var user = await _userManager.FindByNameAsync(apiUser.Username);
            var isCorrect = _userManager.CheckPasswordAsync(user, apiUser.Password);
            if (isCorrect.Result)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                //var claims = new[]
                //{
                //    new Claim(ClaimTypes.NameIdentifier,user.Username),
                //    new Claim(ClaimTypes.Role,user.Role)
                //};
                var claims = new[]
                {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                };
                var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], claims: claims, expires: DateTime.Now.AddMinutes(15), signingCredentials: credentials);


                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            return Ok();
        }
    }
}
