using System.Diagnostics;

namespace ThreadInCSharp;

public class ProcessTry
{

    public static void Run()
    {
        int no = 1;
        string info;

        var runningProcs = from proc in Process.GetProcesses(".")
            orderby proc.Id
            select proc;

        foreach (var proc in runningProcs)
        {
            info = $"#{no++}. PID: {proc.Id}\t Name: {proc.ProcessName}";
            Console.WriteLine(info);
        }
        Console.ReadLine();
    }
}