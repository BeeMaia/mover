using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mover.Auth.Repositories;

namespace Mover.Auth;

public class SeedData
{
    public static async Task EnsureSeedData(IServiceScope scope, IConfiguration configuration)
    {
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await context.Database.MigrateAsync();

        var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var alice = await userMgr.FindByNameAsync("alice");

        if (alice == null)
        {
            alice = new IdentityUser
            {
                UserName = "alice",
                Email = "AliceSmith@email.com",
                EmailConfirmed = true,
                Id = Guid.NewGuid().ToString(),
                PhoneNumber = "1234567890"
            };

            var result = userMgr.CreateAsync(alice, "Pass123$").Result;

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
        }

        var bob = await userMgr.FindByNameAsync("bob");

        if (bob == null)
        {
            bob = new IdentityUser
            {
                UserName = "bob",
                Email = "BobSmith@email.com",
                EmailConfirmed = true,
                Id = Guid.NewGuid().ToString(),
                PhoneNumber = "1234567890",
            };

            var result = await userMgr.CreateAsync(bob, "Pass123$");

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
        }
    }
}
