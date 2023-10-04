using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using SurveyPulse.Client.Services.AuthenticationService;
using SurveyPulse.Shared.IdentityModels;

namespace SurveyPulse.Client.Pages
{
    public partial class Login : ComponentBase
    {
        [Inject]
        public IAuthService _AuthServiceService { get; set; }
        [Inject]
        public NavigationManager _NavigationManager { get; set; }
        [CascadingParameter]
        public IToastService _ToastService { get; set; }
        [Parameter]
        [SupplyParameterFromQuery]
        public string validation { get; set; }
        private LoginModel loginModel = new LoginModel();
        private bool ShowErrors;
        private IEnumerable<string> Errors;
        private bool isLoginSuccess;

        protected override void OnParametersSet()
        {
            if(validation is not null && validation == "success")
            {
                _ToastService.ShowSuccess("Your email is validated. You can log in to system");
            }
        }
        private async Task HandleLogin()
        {
            ShowErrors = false;
            var result = await _AuthServiceService.Login(loginModel);
            isLoginSuccess = result.Success;

            if (result.Success)
            {
               
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
