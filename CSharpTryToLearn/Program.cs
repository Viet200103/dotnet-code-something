using System.Reflection;

namespace CSharpTryToLearn;

public static class Program
{
    
    [AttributeUsage(AttributeTargets.Method)]
    public class RunnableEntryAttribute : Attribute
    {
        
    }
    
    static void Main(string[] args)
    {
        var assembly = Assembly.GetExecutingAssembly();
        
        var methods = assembly
            .GetTypes()
            .SelectMany(type => type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
            .Where(method => method.GetCustomAttributes(typeof(RunnableEntryAttribute), false).Any());

        var methodInfos = methods as MethodInfo[] ?? methods.ToArray();
        if (methodInfos.Count() > 1)
        {
            foreach (var methodInfo in methodInfos)
            {
                Console.WriteLine($"Invoking {methodInfo.DeclaringType?.Name}.{methodInfo.Name}()");
            }
            throw new ApplicationException("More than one method found");
        }
        
        foreach (var method in methodInfos)
        {
            Console.WriteLine($"Invoking {method.DeclaringType?.Name}.{method.Name}()");
            method.Invoke(null, null);
        }
    }
}