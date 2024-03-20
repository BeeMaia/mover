using Microsoft.AspNetCore.Identity;
using Mover.Auth.Factories;
using Mover.Auth.ViewModels;

namespace Mover.Auth;

public static class AuthEndpoints
{
    public static async Task<IResult> HandleLoginAsync(TokenFactory tokenFactory, SignInManager<IdentityUser> signInManager, LoginVM model, IConfiguration configuration, CancellationToken cancellationToken)
    {
        var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
        if (result.Succeeded)
        {
            var tokenString = tokenFactory.CreateToken(model.Email, configuration);
            if (tokenString is null)
            {
                return Results.BadRequest("Invalid login attempt.");
            }

            return Results.Ok(new { Token = tokenString });
        }
        else
        {
            return Results.BadRequest("Invalid login attempt.");
        }
    }

    public static async Task<IResult> HandleRegisterAsync(UserManager<IdentityUser> userManager, RegisterVM model, CancellationToken cancellationToken)
    {
        var user = new IdentityUser { UserName = model.Email, Email = model.Email };
        var result = await userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            return Results.Ok(new { Message = "Registration successful" });
        }

        return Results.BadRequest(result.Errors);
    }
}
