using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using SurveyPulse.Client.Services.UserService;
using SurveyPulse.Shared.Dtos;

namespace SurveyPulse.Client.Pages
{
    public partial class Profile : ComponentBase
    {
        [Inject]
        private IUserService _UserService { get; set; }
        [CascadingParameter]
        public IToastService _ToastService { get; set; }
        private UserUpdateModel _userUpdateModel = new UserUpdateModel();
        protected override async Task OnInitializedAsync()
        {
            var result = await _UserService.GetUserByUsername();
            _userUpdateModel = new UserUpdateModel
            {
                UserId = result.Id.ToString(),
                FirstName = result.FirstName,
                LastName = result.LastName,
                Email = result.Email,
                PhoneNumber = result.PhoneNumber
            };
        }
        private async Task SubmitForm(string userId, UserUpdateModel userUpdateModel)
        {
            var result = await _UserService.EditUser(userId, userUpdateModel);
            if(result.Success)
                _ToastService.ShowSuccess(result.AdditionalInfo);
            else
                _ToastService.ShowError("Error ocured.");
        }
    }
}
