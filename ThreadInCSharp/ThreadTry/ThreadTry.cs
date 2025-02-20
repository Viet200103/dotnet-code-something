namespace ThreadInCSharp.ThreadTry;

public class ThreadTry
{
    public static void Run()
    {
        Thread primaryThread = Thread.CurrentThread;
        primaryThread.Name = "Primary";
        
        Console.WriteLine($"{primaryThread.Name} is executing Main().");
        Printer p = new Printer();

        Thread backgroundThread = new Thread(p.PrintNumbers);
        backgroundThread.Start();

        for (int i = 0; i <= 5; i++)
        {
            Console.WriteLine($"Main thread: {i}");
            Thread.Sleep(1000);
        }
        
        Console.WriteLine("The main thread has finished.");
        Console.ReadLine();
    }
}