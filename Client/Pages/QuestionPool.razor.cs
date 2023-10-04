using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SurveyPulse.Shared;


namespace SurveyPulse.Client.Pages
{
    public partial class QuestionPool : ComponentBase
    {
        //private bool IsUserInRole {  get; set; }
        public List<Question> Questions { get; set; }
        //[Inject]
        //public UserManager<AppUser> _userManager { get; set; }
        //[Inject]
        //IHttpContextAccessor _contextAccessor { get; set; }

        protected override async Task OnInitializedAsync()
        {
            //var user = _contextAccessor.HttpContext.User;
            //IsUserInRole = (await _userManager.IsInRoleAsync(_userManager.FindByNameAsync(user.Identity.Name).Result, "Admin"));

           
        }
    }
}
