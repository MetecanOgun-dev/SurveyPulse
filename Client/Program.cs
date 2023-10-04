using Blazored.LocalStorage;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SurveyPulse.Client;
using SurveyPulse.Client.Helpers;
using SurveyPulse.Client.Services.AuthenticationService;
using SurveyPulse.Client.Services.SurveyService;
using SurveyPulse.Client.Services.UserService;
using Syncfusion.Blazor;
using TextCopy;

var builder = WebAssemblyHostBuilder.CreateDefault(args);


builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSyncfusionBlazor(); 
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjczMjI1NUAzMjMzMmUzMDJlMzBnN2xPZTVpeTRIVmt2S0NwK1dTTnk2YXdZYS8rS2xOWHF5Z29zbjd0aGtFPQ==");


builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredToast();

builder.Services.InjectClipboard();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ISurveyService, SurveyService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
