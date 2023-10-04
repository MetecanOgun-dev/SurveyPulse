using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveyPulse.Server.Data;
using SurveyPulse.Shared;
using SurveyPulse.Shared.Dtos;
using SurveyPulse.Shared.ResponseModels;

namespace SurveyPulse.Server.Controllers.IdentityControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public UserController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);
            if (user is not null)
            {
                return Ok(user);
            }
            return BadRequest("user cannot find");
        }
        [HttpPut("Edit")]
        public async Task<IActionResult> EditUser(string userId, [FromBody] UserUpdateModel userUpdateModel)
        {
            if (userId is null)
                return BadRequest("User cannot found");
            if (userUpdateModel is null)
                return BadRequest("Invalid data");

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user is not null)
            {                
                user.FirstName = userUpdateModel.FirstName;
                user.LastName = userUpdateModel.LastName;
                user.Email = userUpdateModel.Email;
                user.PhoneNumber = userUpdateModel.PhoneNumber;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                    return Ok(new ApiResponse { Success = true, AdditionalInfo = "User updated successfully" });
                else
                    return BadRequest(new ApiResponse { Success = false, AdditionalInfo = "User update failed" });
            }
            else
                return NotFound("User not found");
        }
    }
}
