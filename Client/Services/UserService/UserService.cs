using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using SurveyPulse.Shared;
using SurveyPulse.Shared.Dtos;
using SurveyPulse.Shared.ResponseModels;
using System.Net.Http.Headers;
using System.Text;

namespace SurveyPulse.Client.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public UserService(HttpClient httpClient, ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
            _authenticationStateProvider = authenticationStateProvider;
        }
        public async Task<AppUser> GetUserByUsername()
        {
            string token = await _localStorageService.GetItemAsync<string>("authToken");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var authenticationState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var authUser = authenticationState.User;
            string username = authUser.Identity.Name.ToString();

            var response = await _httpClient.GetAsync($"api/user/getuser?username={username}");

            if (response.IsSuccessStatusCode)
            {
                var jsonUser = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<AppUser>(jsonUser);

                return user;
            }
            else
            {
                throw new Exception("user cannot found");
            }
        }
        public async Task<ApiResponse> EditUser(string userId ,UserUpdateModel model)
        {
            string token = await _localStorageService.GetItemAsync<string>("authToken");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var userJSON = JsonConvert.SerializeObject(model);
            var response = await _httpClient.PutAsync($"api/user/edit?userId={userId}", new StringContent(userJSON, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
                return new ApiResponse { Success = true, AdditionalInfo = "User update successfully." };
            else
                return new ApiResponse { Success = false, AdditionalInfo = "User update fail." };
        }
    }
}
