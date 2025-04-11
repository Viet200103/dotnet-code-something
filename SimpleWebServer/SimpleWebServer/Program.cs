// See https://aka.ms/new-console-template for more information

using SimpleWebServer;


internal class Program
{
    public static async Task Main(string[] args)
    {
        var server = new WebServer("http://localhost:51111/", @"d:\webroot");
        try
        {
            await server.Start();
            Console.WriteLine("Server started");
            Console.ReadKey();
        }
        finally
        {
            server.Stop();
        }
    }
}

