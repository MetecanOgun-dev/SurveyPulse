using Microsoft.AspNetCore.Components;
using SurveyPulse.Shared;

namespace SurveyPulse.Client.Pages
{
    public partial class Survey : ComponentBase
    {
        public List<SurveyPulse.Shared.Survey> Surveys { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Surveys = new List<SurveyPulse.Shared.Survey>();
            
        }
    }
}
