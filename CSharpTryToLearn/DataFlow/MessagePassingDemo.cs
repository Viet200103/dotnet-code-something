using System.Threading.Tasks.Dataflow;

namespace CSharpTryToLearn.DataFlow;

public class MessagePassingDemo
{

    static async Task AsyncSendReceive(BufferBlock<int> bufferBlock)
    {
        for (int i = 0; i < 10; i++)
        {
            await bufferBlock.SendAsync(i);
        }

        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine(await bufferBlock.ReceiveAsync());
        }
    }

    static async Task BufferBlockSendReceive()
    {
        var bufferBlock = new BufferBlock<int>();

        for (int i = 0; i < 3; i++)
        {
            bufferBlock.Post(i);
        }

        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine(bufferBlock.Receive());
        }

        for (int i = 0; i < 3; i++)
        {
            bufferBlock.Post(i);
        }

        while (bufferBlock.TryReceive(out int value))
        {
            Console.WriteLine(value);
        }
        
        var post01 = Task.Run(async () =>
        {
            bufferBlock.Post(0);
            bufferBlock.Post(1);
        });

        var receive = Task.Run(() =>
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(bufferBlock.Receive());
            }
        });

        var post2 = Task.Run(() =>
        {
            bufferBlock.Post(2);
        });
        
        await Task.WhenAll(post01, receive, post2);
    }

    public static void Run()
    {
        
    }
}