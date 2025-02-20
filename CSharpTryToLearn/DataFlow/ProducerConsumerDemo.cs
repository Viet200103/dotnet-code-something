using System.Threading.Tasks.Dataflow;

namespace CSharpTryToLearn.DataFlow;

public class ProducerConsumerDemo
{
    static void Produce(ITargetBlock<byte[]> target)
    {
        var rand = new Random();

        for (int i = 0; i < 10; i++)
        {
            var buffer = new byte[1024];
            rand.NextBytes(buffer);
            target.Post(buffer);
        }
        
        
        target.Complete();
    }

    static async Task<int> ConsumeAsync(ISourceBlock<byte[]> source)
    {
        int bytesProcessed = 0;

        while (await source.OutputAvailableAsync())
        {
            byte[] data = await source.ReceiveAsync();
            bytesProcessed += data.Length;
        }

        return bytesProcessed;
    }

    private static async Task Demo()
    {
        var buffer = new BufferBlock<byte[]>();
        var consumeTask = ConsumeAsync(buffer);
        Produce(buffer);
        var bytesProcessed = await consumeTask;
        
        Console.WriteLine($"Consumed {bytesProcessed} bytes");
    }
    
    public static void Run()
    {
        Task.WaitAll(Demo());
    }
}