namespace ThreadInCSharp.ThreadTry;

public class Printer
{
    public void PrintNumbers()
    {
        Console.WriteLine($"{Thread.CurrentThread.Name} is executing print numbers.");
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine($"Second thread: {i}");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }
}