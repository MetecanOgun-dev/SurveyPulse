using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using SurveyPulse.Client.Services.AuthenticationService;
using SurveyPulse.Shared.IdentityModels;

namespace SurveyPulse.Client.Pages
{
    public partial class Register : ComponentBase
    {
        [Inject]
        public IAuthService _AuthServiceService { get; set; }
        [Inject]
        public NavigationManager _NavigationManager { get; set; }
        [Inject]
        public IToastService _ToastService { get; set; }

        private RegisterModel registerModel = new RegisterModel();
        private bool ShowErrors;
        private bool isRegistrationSuccess;
        private IEnumerable<string> Errors;
        private string registerSuccessMesage;
        private async Task HandleRegistiration()
        {
            ShowErrors = false;

            var result = await _AuthServiceService.Register(registerModel);

            if (result.Success)
            {
                _ToastService.ShowSuccess(result.AdditionalInfo!);
                _NavigationManager.NavigateTo("/");
            }
            else
            {
                Errors = result.Errors!;
                ShowErrors = true;
            }
        }
    }
}
