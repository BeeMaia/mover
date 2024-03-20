using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Mover.Shared;

namespace Mover.Auth.Factories;

public class TokenFactory
{
    private readonly UserManager<IdentityUser> userManager;

    public TokenFactory(UserManager<IdentityUser> userManager)
    {
        this.userManager = userManager;
    }

    public async Task<string?> CreateToken(string username, IConfiguration configuration)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>(Constants.Secrets.MoverJwtKey)!);
        var identity = await userManager.FindByNameAsync(username);
        if (identity == null)
        {
            return null;
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.NameIdentifier, identity.Id)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = configuration[Constants.Options.Jwt_Issuer],
            Audience = configuration[Constants.Options.Jwt_Audience],
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
