using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using SurveyPulse.Shared;
using SurveyPulse.Shared.ResponseModels;
using System.Text;
using System.Text.Json;

namespace SurveyPulse.Client.Services.SurveyService
{
    public class SurveyService : ISurveyService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public SurveyService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }
        public Task<Survey> GetSurvey(Guid surveyId)
        {
            throw new NotImplementedException();
        }
        public async Task<ApiResponse> PostSurvey(Survey survey)
        {
            var surveyJSON = JsonSerializer.Serialize(survey);
            var response = await _httpClient.PostAsync("api/survey/addsurvey", new StringContent(surveyJSON, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
                return new ApiResponse { Success = true, AdditionalInfo = "Success. " };
            return new ApiResponse { Success = false, Errors = new List<string> { "Error occured" } };
        }
    }
}
