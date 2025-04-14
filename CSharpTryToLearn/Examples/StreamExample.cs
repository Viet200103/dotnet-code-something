namespace CSharpTryToLearn.Examples;

public class StreamExample
{

    public static void Run()
    {
        // using (Stream s = new FileStream("test.txt", FileMode.Create))
        // {
        //     Console.WriteLine("Can read: " + s.CanRead);
        //     Console.WriteLine("Can write: " + s.CanWrite);
        //     Console.WriteLine("Can seek: " + s.CanSeek);
        //     
        //     s.WriteByte(101);
        //     s.WriteByte(102);
        //     byte[] block = { 1, 2, 3, 4, 5 };
        //     s.Write(block, 0, block.Length);
        //     
        //     Console.WriteLine(s.Length);
        //     Console.WriteLine(s.Position);
        //     s.Position = 0;
        //     
        //     Console.WriteLine(s.ReadByte());
        //     Console.WriteLine(s.ReadByte());
        //     
        //     Console.WriteLine(s.Read(block, 0, block.Length));
        //     Console.WriteLine(s.Read(block, 0, block.Length));
        // }

        DriveInfo c = new DriveInfo("C");
        
        long totalSize = c.TotalSize;
        long freeBytes = c.TotalFreeSpace;
        long freeToMe = c.AvailableFreeSpace;

        Console.WriteLine($"Total size: {totalSize}");
        Console.WriteLine($"Free bytes: {freeBytes}");
        Console.WriteLine($"Free To Me: {freeToMe}");
        
        foreach (DriveInfo drive in DriveInfo.GetDrives())
        {
            Console.WriteLine(drive.Name);
            Console.WriteLine(drive.DriveType);
            Console.WriteLine(drive.RootDirectory);
            if (drive.IsReady)
            {
                Console.WriteLine(drive.VolumeLabel);
                Console.WriteLine(drive.DriveFormat);
            }
        }
    }
}