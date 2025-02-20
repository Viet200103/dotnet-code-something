namespace CSharpTryToLearn.Asynchronous;

public class AsyncWithThread
{
    public static void Run()
    {
        Thread thread1 = new Thread(Call);
        thread1.Name = "Thread1";

        thread1.Start();

        Console.ReadLine();
    }

    private static void Call()
    {
        ReadFile();
        CallAPI();
        SendEmail();
    }

    private static async void ReadFile()
    {
        Console.WriteLine("Start ReadFile");
        await Task.Delay(5000);
        Console.WriteLine("End ReadFile");
    }

    private static async void CallAPI()
    {
        Console.WriteLine("Start CallAPI");
        await Task.Delay(10000);
        Console.WriteLine("End CallAPI");
    }

    private static async void SendEmail()
    {
        Console.WriteLine("Start SendEmail");
        await Task.Delay(3000);
        Console.WriteLine("End SendEmail");
    }
}