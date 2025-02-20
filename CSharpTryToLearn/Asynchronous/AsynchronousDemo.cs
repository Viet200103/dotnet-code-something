using System.Diagnostics;
using System.Net;

namespace CSharpTryToLearn.AsynchronousTry;

public class AsynchronousDemo
{
    static async Task<string> DownloadStringWithRetries(string url)
    {
        using (var client = new HttpClient())
        {
            var nextDelay = TimeSpan.FromSeconds(1);
            for (int i = 0; i != 3; ++i)
            {
                try
                {
                    return await client.GetStringAsync(url);
                }
                catch 
                {
                    
                }
                await Task.Delay(nextDelay);
                nextDelay += nextDelay;
            }

            return await client.GetStringAsync(url);
        }   
    }

    static async Task<string> DownloadStringWithTimeout(string url)
    {
        using (var client = new HttpClient())
        {
            var downloadTask = client.GetStringAsync(url);
            var timeoutTask = Task.Delay(TimeSpan.FromSeconds(3));
            
            var completedTask = await Task.WhenAny(downloadTask, timeoutTask);
            if (completedTask == timeoutTask)
            {
                return null;
            }

            return await downloadTask;
        }
    }

    static async Task ThrowNotImplementedException()
    {
        throw new NotImplementedException();
    }

    static async Task ThrowInvalidOperationException()
    {
        throw new NotImplementedException();
    }

    static async Task ObserveOneException()
    {
        var task1 = ThrowNotImplementedException();
        var task2 = ThrowInvalidOperationException();

        try
        {
            await Task.WhenAll(task1, task2);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    static async Task ObserveAllExceptionAsync()
    {
        var task1 = ThrowInvalidOperationException();
        var task2 = ThrowNotImplementedException();
        
        Task allTasks = Task.WhenAll(task1, task2);
        try
        {
            await allTasks;
        }
        catch
        {
            AggregateException? allExceptions = allTasks.Exception?.Flatten();
            if (allExceptions != null)
            {
                Console.WriteLine(allExceptions);
            }
        }
    }

    static async Task<int> FirstRespondingUrlAsync(string url)
    {
        var httpClient = new HttpClient();
        
        Task<byte[]> downloadTaskA = httpClient.GetByteArrayAsync(url);
        Task<byte[]> downloadTaskB = httpClient.GetByteArrayAsync(url);
        
        Task<byte[]> completedTask = await Task.WhenAny(downloadTaskA, downloadTaskB);
        
        byte[] data = await completedTask;
        
        return data.Length;
    }

    static async Task<int> DelayAndReturnAsync(int seconds)
    {
        await Task.Delay(TimeSpan.FromSeconds(seconds));
        return seconds;
    }

    static async Task AwaitAndProcessAsync(Task<int> task)
    {
        var result = await task;
        Trace.WriteLine(result);
    }

    static async Task ProcessTaskAsync()
    {
        Task<int> taskA = DelayAndReturnAsync(2);
        Task<int> taskB = DelayAndReturnAsync(3);
        Task<int> taskC = DelayAndReturnAsync(1);

        var tasks = new[] { taskA, taskB, taskC };

        var taskList = new List<Task>();
        foreach (var task in tasks)
        {
            taskList.Add(task);
        }
        
        await Task.WhenAll(taskList);
    }
    
    static async Task ProcessTaskAsync2()
    {
        Task<int> taskA = DelayAndReturnAsync(2);
        Task<int> taskB = DelayAndReturnAsync(3);
        Task<int> taskC = DelayAndReturnAsync(1);
        
        var tasks = new[] { taskA, taskB, taskC };

        var processingTasks = tasks.Select(async t =>
        {
            var result = await t;
            Trace.WriteLine(result);
        }).ToArray();

        await Task.WhenAll(processingTasks);
    }
    
    // no good solution for hanlde exception with async void
    // convert to async task, and handle exception
    sealed class AsyncVoidHandleException 
    {

        async void Excute(object parameter)
        {
            
            await ExcuteTask(parameter);
        }

        public async Task ExcuteTask(object parameter)
        {
            // Asynchronous command implmenetation go here
        }
    }


    static Task<String> DownloadStringTaskAsync(WebClient client, Uri address)
    {
        var tcs = new TaskCompletionSource<String>();
        
        DownloadStringCompletedEventHandler handler = null;

        handler = (sender, e) =>
        {
            client.DownloadStringCompleted -= handler;
            if (e.Cancelled)
            {
                tcs.TrySetCanceled();
            }
            else if (e.Error != null)
            {
                tcs.TrySetException(e.Error);   
            }
            else
            {
                tcs.TrySetResult(e.Result);
            }
        };
        
        client.DownloadStringCompleted += handler;
        client.DownloadStringAsync(address);
        
        throw new NotImplementedException();
    }
    
    public static void Run()
    {
        
    }
}