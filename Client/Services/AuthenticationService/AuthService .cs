using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using SurveyPulse.Client.Helpers;
using SurveyPulse.Shared.IdentityModels;
using SurveyPulse.Shared.ResponseModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace SurveyPulse.Client.Services.AuthenticationService
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }
        public async Task<ApiResponse> Register(RegisterModel registerModel)
        {
            var result = await _httpClient.PostAsJsonAsync("api/register", registerModel);
            if (result.IsSuccessStatusCode)
                return new ApiResponse { Success = true , AdditionalInfo = "Registration successful. Please check your inbox to validate your email." };
            return new ApiResponse { Success = false, Errors = new List<string> { "Error occured" } };
        }
        public async Task<LoginResponse> Login(LoginModel loginModel)
        {
            var loginAsJson = JsonSerializer.Serialize(loginModel);
            var response = await _httpClient.PostAsync("api/Login", new StringContent(loginAsJson, Encoding.UTF8, "application/json"));
            var loginResult = JsonSerializer.Deserialize<LoginResponse>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (!response.IsSuccessStatusCode)
            {
                return loginResult!;
            }
            
            await _localStorage.SetItemAsync("authToken", loginResult!.Token);
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginModel.Username!);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.Token);
            return loginResult;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
