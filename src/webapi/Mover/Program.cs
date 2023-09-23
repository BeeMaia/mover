using Mover.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Register Modules
builder.RegisterModules();

var app = builder.Build();

app.UseCors("CorsPolicy");

app.UseCloudEvents();
//app.UseAuthentication();
//app.UseAuthorization();

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

app.Run();