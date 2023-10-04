using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SurveyPulse.Server.CustomTokenProviders;
using SurveyPulse.Server.Data;
using SurveyPulse.Service.Models;
using SurveyPulse.Service.Services.EmailService;
using SurveyPulse.Shared;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var emailConfiguration = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
var provider = "Developement"; // "Production"

builder.Services.AddPooledDbContextFactory<ApplicationDbContext>(options => _ = provider switch
{
    "Developement" => options.UseSqlServer(
        builder.Configuration.GetConnectionString("TestConnection"),
        x => x.MigrationsAssembly("SurveyPulse.MigrationsDevelopement")
        ),
    "Production" => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection"),
    x => x.MigrationsAssembly("SurveyPulse.Migrations")
    ),
    _ => throw new Exception($"Unsupported provider: {provider}")
});

builder.Services.AddScoped(p => p.GetRequiredService<IDbContextFactory<ApplicationDbContext>>().CreateDbContext());

builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = true;
    options.Tokens.EmailConfirmationTokenProvider = "SurveyPulseEmailValidation";
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddTokenProvider<EmailValidationTokenProvider<AppUser>>("SurveyPulseEmailValidation")
.AddDefaultTokenProviders();

builder.Services.Configure<EmailConfirmationTokenProviderOptions>(opt =>
     opt.TokenLifespan = TimeSpan.FromDays(7));

builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
   opt.TokenLifespan = TimeSpan.FromDays(1));

builder.Services.Configure<PasswordHasherOptions>(opt =>
    opt.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV2);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecurityKey"]!))
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("Policy", builder =>
    builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

builder.Services.AddControllers(options => { options.AllowEmptyInputInBodyModelBinding = true; });

builder.Services.AddSwaggerGen();

builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

builder.Services.AddSingleton(emailConfiguration);

builder.Services.AddScoped<IEmailService, EmailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.UseCors("Policy");
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");
using (var scope = app.Services.CreateScope())
{
    await DbAdminSeeder.AdminAsync(scope.ServiceProvider);
}
app.Run();
