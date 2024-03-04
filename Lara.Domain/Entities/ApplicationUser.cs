using Microsoft.AspNetCore.Identity;

namespace Lara.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
}