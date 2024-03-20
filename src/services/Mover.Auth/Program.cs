using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mover.Auth;
using Mover.Auth.Repositories;
using Mover.Shared;
using Mover.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddCustomConfiguration();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetValue<string>(Constants.Secrets.MoverSqlConnString)));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Register Modules
builder.RegisterModules();

var app = builder.Build();

app.UseCors("CorsPolicy");

app.UseCloudEvents();
app.UseAuthentication();
app.UseAuthorization();

app.MapSubscribeHandler();

// Register endpoints
app.MapEndpoints();
app.MapDispatchers();

// Configure the HTTP request pipeline.
app.UseSwagger(s =>
{
    s.RouteTemplate = "documentation/{documentName}/documentation.json";
});
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/documentation/v1/documentation.json", "Mover Api");
    s.RoutePrefix = "documentation";
});

// Apply database migration automatically. Note that this approach is not
// recommended for production scenarios. Consider generating SQL scripts from
// migrations instead.
using (var scope = app.Services.CreateScope())
{
    await SeedData.EnsureSeedData(scope, app.Configuration);
}

app.Run();