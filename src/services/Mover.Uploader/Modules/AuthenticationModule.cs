using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Mover.Shared;
using Mover.Shared.Interfaces;

namespace Mover.Uploader.Modules;

public sealed class AuthenticationModule : IModule
{
    public bool IsEnabled => true;

    public int Order => 0;

    public void MapDispatchers(IEndpointRouteBuilder endpoints)
    {
        // no dispatchers
    }

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        // no endpoints
    }

    public void RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration[Constants.Options.Jwt_Issuer],
                    ValidAudience = builder.Configuration[Constants.Options.Jwt_Audience],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>(Constants.Secrets.MoverJwtKey)!))
                };
            });

        builder.Services.AddAuthorization();
    }
}
