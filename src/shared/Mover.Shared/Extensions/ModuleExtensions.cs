using System.Reflection;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Mover.Shared.Interfaces;

namespace Mover.Shared.Extensions;

public static class ModuleExtensions
{
    private static readonly IList<IModule> RegisteredModules = new List<IModule>();

    public static WebApplicationBuilder RegisterModules(this WebApplicationBuilder builder)
    {
        var modules = DiscoverModules();
        foreach (var module in modules
                     .Where(m => m.IsEnabled)
                     .OrderBy(m => m.Order))
        {
            module.RegisterModule(builder);
            RegisteredModules.Add(module);
        }

        return builder;
    }

    public static WebApplication MapEndpoints(this WebApplication app)
    {
        foreach (var module in RegisteredModules)
        {
            module.MapEndpoints(app);
        }

        return app;
    }

    public static WebApplication MapDispatchers(this WebApplication app)
    {
        foreach (var module in RegisteredModules)
        {
            module.MapDispatchers(app);
        }

        return app;
    }

    private static IEnumerable<IModule> DiscoverModules()
    {
        var entryAssembly = Assembly.GetEntryAssembly();
        return entryAssembly?
            .GetTypes()
            .Where(p => p.IsClass && p.IsAssignableTo(typeof(IModule)))
            .Select(Activator.CreateInstance)
            .Cast<IModule>() ?? Enumerable.Empty<IModule>();
    }
}
