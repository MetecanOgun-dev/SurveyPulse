using Microsoft.AspNetCore.Components;
using SurveyPulse.Client.Services.AuthenticationService;

namespace SurveyPulse.Client.Pages
{
    public partial class Logout : ComponentBase
    {
        [Inject]
        public IAuthService _AuthService { get; set; }
        [Inject]
        public NavigationManager _NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await _AuthService.Logout();
            _NavigationManager.NavigateTo("/");
        }
    }
}

