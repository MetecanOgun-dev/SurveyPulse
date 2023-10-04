using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using SurveyPulse.Shared;
using SurveyPulse.Shared.ResponseModels;
using System.Text;

namespace SurveyPulse.Server.Controllers.IdentityControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidateEmailController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public ValidateEmailController(UserManager<AppUser> userManager, IConfiguration configuration) 
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> ValidateEmail(string email, string emailValidationToken)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return StatusCode(StatusCodes.Status404NotFound,
                    new ApiResponse { Success = false, Errors = new List<string> { "User not found" }, AdditionalInfo = "User not found" });

            var decodedToken = WebEncoders.Base64UrlDecode(emailValidationToken);
            string token = Encoding.UTF8.GetString(decodedToken);
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e1 => e1.Description);
                return StatusCode(StatusCodes.Status400BadRequest,
                        new ApiResponse { Success = false, Errors = errors });
            }
            else
            {
                return Redirect($"{_configuration["ApplicationUrl"]}/login/?validation=success");
            }
        }
    }
}
