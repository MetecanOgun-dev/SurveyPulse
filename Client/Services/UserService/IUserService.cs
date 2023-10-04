using SurveyPulse.Shared;
using SurveyPulse.Shared.Dtos;
using SurveyPulse.Shared.ResponseModels;

namespace SurveyPulse.Client.Services.UserService
{
    public interface IUserService
    {
        Task<AppUser> GetUserByUsername();
        Task<ApiResponse> EditUser(string userId, UserUpdateModel model);
    }
}
