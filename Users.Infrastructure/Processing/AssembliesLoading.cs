using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Users.Infrastructure.Processing;

public static class AssembliesLoading
{
    public static void LoadAssemblies(this IServiceCollection collection)
    {
        var assemblyNames = Assembly.GetEntryAssembly().GetReferencedAssemblies()
            .Where(x => !x.Name.StartsWith("Microsoft.") && !x.Name.StartsWith("System."));

        foreach (var assemblyName in assemblyNames)
        {
            var ass = Assembly.Load(assemblyName);
        }
    }
}