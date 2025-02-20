using System.Diagnostics;

namespace CSharpTryToLearn.AsynchronousTry;

public class AsynchronousDemo2
{
    private static HttpClient s_client = new HttpClient()
    {
        MaxResponseContentBufferSize = 1_000_000
    };

    private static IEnumerable<string> s_urlList = new string[]
    {
        "https://learn.microsoft.com",
        "https://learn.microsoft.com/aspnet/core",
        "https://learn.microsoft.com/azure",
        "https://learn.microsoft.com/azure/devops",
        "https://learn.microsoft.com/dotnet",
        "https://learn.microsoft.com/dynamics365",
        "https://learn.microsoft.com/education",
        "https://learn.microsoft.com/enterprise-mobility-security",
        "https://learn.microsoft.com/gaming",
        "https://learn.microsoft.com/graph",
        "https://learn.microsoft.com/microsoft-365",
        "https://learn.microsoft.com/office",
        "https://learn.microsoft.com/powershell",
        "https://learn.microsoft.com/sql",
        "https://learn.microsoft.com/surface",
        "https://learn.microsoft.com/system-center",
        "https://learn.microsoft.com/visualstudio",
        "https://learn.microsoft.com/windows",
        "https://learn.microsoft.com/maui"
    };

    public static async Task SumPageSizesAsync()
    {
        var stopwatch = Stopwatch.StartNew();
        
        IEnumerable<Task<int>> downloadTasksQuery = 
            from url in s_urlList
            select ProcessUrlAsync(url, s_client);
        
        List<Task<int>> downloadTasks = downloadTasksQuery.ToList();

        int total = 0;
        while (downloadTasks.Any())
        {
            Task<int> finishedTask = await Task.WhenAny(downloadTasks);
            downloadTasks.Remove(finishedTask);
            total += await finishedTask;
        }
        
        stopwatch.Stop();
        
        Console.WriteLine($"\nTotal bytes returned:    {total:#,#}");
        Console.WriteLine($"Elapsed time:              {stopwatch.Elapsed}\n");
    }

    static async Task<int> ProcessUrlAsync(string url, HttpClient client)
    {
        byte[] content = await client.GetByteArrayAsync(url);
        Console.WriteLine($"{url, -60} {content.Length, 10:#.#}");
        return content.Length;
    }
    
    public class Program
    {

        public static void Run()
        {
            Task.WaitAll(SumPageSizesAsync());
        }
    }
}