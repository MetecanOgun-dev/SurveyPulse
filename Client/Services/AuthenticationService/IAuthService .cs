using SurveyPulse.Shared.IdentityModels;
using SurveyPulse.Shared.ResponseModels;

namespace SurveyPulse.Client.Services.AuthenticationService
{
    public interface IAuthService
    {
        Task<ApiResponse> Register(RegisterModel registerModel);
        Task<LoginResponse> Login(LoginModel loginModel);
        Task Logout();
    }
}
