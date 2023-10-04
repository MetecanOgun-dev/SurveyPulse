using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SurveyPulse.Shared;
using SurveyPulse.Shared.ResponseModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SurveyPulse.Server.Controllers.IdentityControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<AppUser> _signInManager;

        public LoginController(IConfiguration configuration, SignInManager<AppUser> signInManager)
        {
            _configuration = configuration;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Shared.IdentityModels.LoginModel loginModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(new LoginResponse
                { Success = false, Errors = new List<string> { "Error" }, AdditionalInfo = "Error" });

            var result = await _signInManager.PasswordSignInAsync(loginModel.Username!, loginModel.Password!, false, false);
            if(!result.Succeeded)
            {
                return BadRequest(new LoginResponse
                { Success = false, Errors = new List<string> { "Invalid email or password." }, AdditionalInfo = "Invalid email or password." });
            }
            
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, loginModel.Username!)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecurityKey"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expirityDays = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JWT:ExpiryInDays"]));

            var token = new JwtSecurityToken(
                _configuration["JWT:Issuer"],
                _configuration["JWT:Audience"],
                claims,
                expires : expirityDays,
                signingCredentials: credentials
            );

            return Ok(new LoginResponse { Success = true, Token = new JwtSecurityTokenHandler().WriteToken(token)});
        }

    }
}
