using System.Text;

namespace CSharpTryToLearn.Asynchronous;

public class FileWithAsyncExample
{
    static async Task ProcessReadAsync()
    {
        try
        {
            string filePath = "temp.txt";
            if (File.Exists(filePath) != false)
            {
                string text = await File.ReadAllTextAsync(filePath);
                Console.WriteLine(text);
            }
            else
            {
                Console.WriteLine("File not found : {filePath}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }


    async Task<string> ReadTextAsync()
    {
        using var sourceStream =
            new FileStream(
                "temp.txt", FileMode.Open, FileAccess.Read, FileShare.Read,
                bufferSize: 4096, useAsync: true
            );

        var sb = new StringBuilder();

        byte[] buffer = new byte[0x1000];
        int numRead;
        while ((numRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
        {
            string text = Encoding.Unicode.GetString(buffer, 0, numRead);
            sb.Append(text);
        }

        return sb.ToString();
    }

    public async Task ProcessMultipleWritesAsync()
    {
        IList<FileStream> sourceStreams = new List<FileStream>();

        try
        {
            string folder = Directory.CreateDirectory("tempFolder").Name;
            IList<Task> writeTaskList = new List<Task>();

            for (int index = 0; index <= 10; ++index)
            {
                string fileName = $"file-{index:00}.txt";
                string filePath = $"{folder}/{fileName}";

                string text = $"In file {index}{Environment.NewLine}";
                byte[] encodedText = Encoding.Unicode.GetBytes(text);

                var sourceStream =
                    new FileStream(
                        filePath, FileMode.Create, FileAccess.Write, FileShare.None,
                        bufferSize: 4096, useAsync: true
                    );
                
                Task writeTask = sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
                sourceStreams.Add(sourceStream);
                
                writeTaskList.Add(writeTask);
            }
            
            await Task.WhenAll(writeTaskList);
        }
        finally
        {
            foreach (var fileStream in sourceStreams)
            {
                fileStream.Close();
            }
        }
    }
}