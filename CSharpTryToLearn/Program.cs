using System.Reflection;

namespace CSharpTryToLearn;

class Program
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
        
        foreach (var method in methods)
        {
            Console.WriteLine($"Invoking {method.DeclaringType?.Name}.{method.Name}()");
            method.Invoke(null, null);
        }
    }
}