using Microsoft.AspNetCore.Identity;
using SurveyPulse.Shared;

namespace SurveyPulse.Server.Data
{
    public static class DbAdminSeeder
    {
        public static async Task AdminAsync(IServiceProvider service)
        {
            var userManager = service.GetService<UserManager<AppUser>>();
            var admin = new AppUser
            {
                UserName = "admin",
                Email = "admin@surveypulse.com",
                FirstName = "admin",
                LastName = "admin",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };

            var userInDb = await userManager.FindByEmailAsync(admin.Email);
            if (userInDb is null)
            {
                await userManager.CreateAsync(admin, "Admin123@");
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}
