using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using SurveyPulse.Service.Models;
using SurveyPulse.Service.Services.EmailService;
using SurveyPulse.Service.Services.EmailService.SystemMailGenerators;
using SurveyPulse.Shared;
using SurveyPulse.Shared.IdentityModels;
using SurveyPulse.Shared.ResponseModels;
using System.Text;

namespace SurveyPulse.Server.Controllers.IdentityControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _EmailService;
        private readonly RoleManager<AppRole> _roleManager;

        public RegisterController(UserManager<AppUser> userManager, IConfiguration configuration, IEmailService emailService, RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
            _EmailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newUser = new AppUser
            {
                Email = registerModel.Email,
                UserName = registerModel.Username,
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName,
            };

            var result = await _userManager.CreateAsync(newUser, registerModel.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e1 => e1.Description);
                return StatusCode(StatusCodes.Status403Forbidden,
                    new ApiResponse { Success = false, Errors = errors });
            }
            else
            {
                await _userManager.AddToRoleAsync(newUser, "User");

                //Send Validation Mail
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                var encodeToken = Encoding.UTF8.GetBytes(token);
                var emailValidationToken = WebEncoders.Base64UrlEncode(encodeToken);
                var confirmationLink = $"{_configuration["ApplicationUrl"]}/api/validateemail?email={newUser.Email}&emailValidationToken={emailValidationToken}";
                var emailContentVariables = new Dictionary<int, string> { { 0, newUser.Email }, { 1, confirmationLink! } };
                var message = new Message(new string[] { newUser.Email }, "Welcome to SurveyPulse", new SystemMailGenerator(SystemEmailTemplates.Welcome, emailContentVariables).MailContent);
                await _EmailService.SendEmail(message);

                var response = new ApiResponse { Success = true, AdditionalInfo = "Registration successful. Please check your inbox to validate your email." };
                return StatusCode(StatusCodes.Status201Created, response);
               
            }
        }
    }
}
