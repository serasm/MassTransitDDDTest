using System.Reflection;

namespace Users.Infrastructure.Processing.MassTransitExtensions;

public static class GetTypesExtensions
{
    private static IEnumerable<Type> GetAllTypes() => AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes());
    
    public static IEnumerable<Type> FindAllInplementationTypesByType(this Type searchedImplementedType)
    {
        var searchedTypes = GetAllTypes().Where(type =>
            (searchedImplementedType.IsAssignableFrom(type) || type.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == searchedImplementedType)) && !type.IsAbstract &&
            !type.IsInterface).ToList();

        return searchedTypes;
    }
}