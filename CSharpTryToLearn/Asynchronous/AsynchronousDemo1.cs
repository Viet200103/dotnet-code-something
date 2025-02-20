using System.Text.RegularExpressions;

namespace CSharpTryToLearn.Asynchronous;

public class AsynchronousDemo1
{

    class Button
    {
        public Func<object, object, Task>? Clicked { get; internal set; }
    }

    class DamageResult
    {
        public int Damage { get; set; }
    }

    class User
    {
        public bool IsEnabled { get; set; }
        
        public int Id { get; set; }
    }

    public class Program
    {
        private static readonly Button s_downloadButton = new();
        private static readonly Button s_calculateButton = new();
        
        private static readonly HttpClient s_httpClient = new();

        private static readonly IEnumerable<string> s_urlList =
        [
            "https://learn.microsoft.com",
            "https://learn.microsoft.com/aspnet/core",
            "https://learn.microsoft.com/azure",
            "https://learn.microsoft.com/azure/devops",
            "https://learn.microsoft.com/dotnet",
            "https://learn.microsoft.com/dotnet/desktop/wpf/get-started/create-app-visual-studio",
            "https://learn.microsoft.com/education",
            "https://learn.microsoft.com/shows/net-core-101/what-is-net",
            "https://learn.microsoft.com/enterprise-mobility-security",
            "https://learn.microsoft.com/gaming",
            "https://learn.microsoft.com/graph",
            "https://learn.microsoft.com/microsoft-365",
            "https://learn.microsoft.com/office",
            "https://learn.microsoft.com/powershell",
            "https://learn.microsoft.com/sql",
            "https://learn.microsoft.com/surface",
            "https://dotnetfoundation.org",
            "https://learn.microsoft.com/visualstudio",
            "https://learn.microsoft.com/windows",
            "https://learn.microsoft.com/maui"
        ];

        private static void Calculate()
        {
            static DamageResult CalculateDamageDone()
            {
                return new DamageResult()
                {

                };
            }

            s_calculateButton.Clicked += async (o, e) =>
            {
                var damageResult = await Task.Run(() => CalculateDamageDone());
                DisplayImage(damageResult);
            };
        }

        private static void DisplayImage(DamageResult damage)
        {
            Console.WriteLine(damage.Damage);
        }


        private static void Download(string url)
        {
            s_downloadButton.Clicked += async (o, e) =>
            {
                var stringData = await s_httpClient.GetStringAsync(url);
                DoSomethingWithData(stringData);
            };
        }

        private static void DoSomethingWithData(object stringData)
        {
            Console.WriteLine("Displaying data: ", stringData);
        }

        private static async Task<User> GetUserAsync(int userId)
        {
            // code something
            return await Task.FromResult(new User() { Id = userId });
        }

        private static async Task<IEnumerable<User>> GetUsersAsync(IEnumerable<int> userIds)
        {
            var getUserTasks = new List<Task<User>>();
            foreach (var userId in userIds)
            {
                getUserTasks.Add(GetUserAsync(userId));
            }
            return await Task.WhenAll(getUserTasks);
        }

        private static async Task<User[]> GetUsersAsyncByLINQ(IEnumerable<int> userIds)
        {
            var getUserTasks = userIds.Select(id => GetUserAsync(id)).ToArray();
            return await Task.WhenAll(getUserTasks);
        }

        private static async Task<int> GetDotNetCount(string url)
        {
            var html = await s_httpClient.GetStringAsync(url);
            return Regex.Matches(html, @"\.NET").Count;
        }

        public static async Task Run()
        {
            Console.WriteLine("Application started.");
            Console.WriteLine("Counting '.NET' phrase in websites...");

            int total = 0;

            foreach (var url in s_urlList)
            {
                var result = await GetDotNetCount(url);
                Console.WriteLine($"{url}: {result}");
                total += result;
            }
            Console.WriteLine("Total: " + total);
            
            Console.WriteLine("Retrieving User objects with list of IDs...");
            IEnumerable<int> ids = new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 0};
            var users = await GetUsersAsync(ids);
            foreach (User? user in users)
            {
                Console.WriteLine($"{user.Id}: {user.IsEnabled}");
            }
            
            Console.WriteLine("Retrieving User objects with list of IDs...");
        }
    }
}