using Lara.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Lara.Application.API.Extesions;

public static class CreateAdminUser
{
    public static async void AddAdminUser(this WebApplication webApplication, string adminPassword)
    {
        using var scope = webApplication.Services.CreateScope();

        var services = scope.ServiceProvider;

        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        var alreadyCreated = userManager.Users.Any(u => u.Email == "admin@lara.com");

        if (alreadyCreated)
        {
            return;
        }

        var adminUser = new ApplicationUser()
        {
            Email = "admin@lara.com",
            FirstName = "Admin",
            LastName = "Silva",
            UserName = "admin@lara.com"
        };

        var resultUser = await userManager.CreateAsync(adminUser, adminPassword);

        if (!resultUser.Succeeded)
        {
            throw new Exception("Falha em criar usuário admin");
        }
        
        var resultRole = await userManager.AddToRoleAsync(adminUser, "ADMIN");
        
        if (!resultRole.Succeeded)
        {
            throw new Exception("Falha em atribuir o papel admin");
        }
    }
}