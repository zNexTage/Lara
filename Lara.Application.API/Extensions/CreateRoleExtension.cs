using Microsoft.AspNetCore.Identity;

namespace Lara.Application.API.Extesions;

public static class CreateRoleExtension
{
    /// <summary>
    /// Cria os papéis na aplicação.
    /// </summary>
    /// <param name="webApplication"></param>
    public static async void AddRoles(this WebApplication webApplication)
    {
        using var scope = webApplication.Services.CreateScope();
        var services = scope.ServiceProvider;

        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        if (roleManager.Roles.Any())
        {
            return;
        }

        var roles = new List<string>()
        {
            "ADMIN",
            "CUSTOMER"
        };

        foreach (var role in roles) 
        {
            await roleManager.CreateAsync(new IdentityRole(role){ NormalizedName = role.ToLower()});
        }
    }
    
}