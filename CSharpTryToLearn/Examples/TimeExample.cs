
namespace CSharpTryToLearn.Examples;

public class TimeExample
{
    public static void Run()
    {
        // Console.WriteLine(new TimeSpan(2, 30, 0));
        // Console.WriteLine(TimeSpan.FromHours(2.5));
        // Console.WriteLine(TimeSpan.FromDays(-2.5));

        // Console.WriteLine("------------------------------------------------");

        // Console.WriteLine(TimeSpan.FromHours(2) + TimeSpan.FromMinutes(20));
        // Console.WriteLine(TimeSpan.FromDays(10) - TimeSpan.FromSeconds(1));

        // Console.WriteLine("------------------------------------------------");

        // TimeSpan nearlyTenDays = TimeSpan.FromDays(10) - TimeSpan.FromSeconds(1);
        //
        // Console.WriteLine(nearlyTenDays.TotalDays);
        // Console.WriteLine(nearlyTenDays.TotalHours);
        // Console.WriteLine(nearlyTenDays.TotalMinutes);
        // Console.WriteLine(nearlyTenDays.TotalMilliseconds);

        // Console.WriteLine("------------------------------------------------");
        // Console.WriteLine("--------------------DateTime--------------------");
        //
        // DateTime dt = new DateTime(2000, 2, 3, 10, 20, 30);
        //
        // Console.WriteLine(dt.Year);
        // Console.WriteLine(dt.Month);
        // Console.WriteLine(dt.Day);
        // Console.WriteLine(dt.DayOfWeek);
        // Console.WriteLine(dt.DayOfYear);
        // Console.WriteLine(dt.Hour);
        // Console.WriteLine(dt.Minute);
        // Console.WriteLine(dt.Second);
        // Console.WriteLine(dt.Millisecond);
        // Console.WriteLine(dt.TimeOfDay); // TimeSpan
        //

        // Console.WriteLine("-------------------------------------------------------------");
        // Console.WriteLine("--------------------DateTime & Time Zones--------------------");

        // DateTime dt1 = new DateTime(2000, 1, 1, 10, 20, 30, DateTimeKind.Local);
        // DateTime dt2 = new DateTime(2000, 1, 1, 10, 20, 30, DateTimeKind.Utc);
        // Console.WriteLine(dt1 == dt2);
        // DateTime local = DateTime.Now;
        // DateTime utc = local.ToUniversalTime();
        // Console.WriteLine(local == utc);

        // DateTime d = new DateTime(2015, 12, 12);
        // DateTime utc = DateTime.SpecifyKind(d, DateTimeKind.Utc);
        // Console.WriteLine(utc);

        // TimeZoneInfo zone = TimeZoneInfo.Local;
        // Console.WriteLine(zone.StandardName);
        // Console.WriteLine(zone.DaylightName);

        // DateTime dt1 = new DateTime(2025, 4, 9);
        // DateTime dt2 = new DateTime(2025, 3, 1);
        // Console.WriteLine(zone.IsDaylightSavingTime(dt1));
        // Console.WriteLine(zone.IsDaylightSavingTime(dt2));
        // Console.WriteLine(zone.GetUtcOffset(dt1));
        // Console.WriteLine(zone.GetUtcOffset(dt2));

        // TimeZoneInfo we = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
        // Console.WriteLine(we.Id);
        // Console.WriteLine(we.BaseUtcOffset);
        // Console.WriteLine(we.SupportsDaylightSavingTime);

        // Console.WriteLine("-------------------------------------------------------------");
        // Console.WriteLine("-----------------------------Process-------------------------");

        // ProcessStartInfo psi = new ProcessStartInfo
        // {
        //     FileName = "cmd.exe",
        //     Arguments = "/c ipconfig /all",
        //     RedirectStandardOutput = true,
        //     UseShellExecute = false
        // };
        //
        // Process? p = Process.Start(psi);
        // if (p != null)
        // {
        //     string result = p.StandardOutput.ReadToEnd();
        //     Console.WriteLine(result);
        // }
        // else
        // {
        //     Console.WriteLine("Process was not started.");
        // }
        
        // Console.WriteLine("-------------------------------------------------------------");
        // Console.WriteLine("---------------------------Collection------------------------");
        //
        // string s = "Hello";
        //
        // IEnumerator rator = s.GetEnumerator();
        // while (rator.MoveNext())
        // {
        //     char c = (char) rator.Current;
        //     Console.Write(c + ".");
        // }
        //
    }
}