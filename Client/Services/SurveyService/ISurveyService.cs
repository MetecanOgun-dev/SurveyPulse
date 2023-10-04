using SurveyPulse.Shared;
using SurveyPulse.Shared.ResponseModels;

namespace SurveyPulse.Client.Services.SurveyService
{
    public interface ISurveyService
    {
        Task<Survey> GetSurvey(Guid surveyId);
        Task<ApiResponse> PostSurvey(Survey survey);
    }
}
