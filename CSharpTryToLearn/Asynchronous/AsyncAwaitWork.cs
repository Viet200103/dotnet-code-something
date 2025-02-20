using System.Net;
using System.Net.Sockets;

namespace CSharpTryToLearn.AsynchronousTry;

public class AsyncAwaitWork
{
    public static void Run()
    {
    }

    public void CopyToStream(Stream source, Stream destination)
    {
        var buffer = new byte[0x1000];
        int numRead;
        while ((numRead = source.Read(buffer, 0, buffer.Length)) != 0)
        {
            destination.Write(buffer, 0, numRead);
        }
    }

    public IAsyncResult BeginCopyToStream(
        Stream source,
        Stream destination,
        AsyncCallback? callback,
        object state
    )
    {
        var asyncResult = new MyAsyncResult(state);
        var buffer = new byte[0x1000];

        Action<IAsyncResult?> readWriteLoop = null!;

        readWriteLoop = iar =>
        {
            try
            {
                for (bool isRead = iar == null;; isRead = !isRead)
                {
                    if (isRead)
                    {
                        iar = source.BeginRead(buffer, 0, buffer.Length, static readResult =>
                        {
                            if (!readResult.CompletedSynchronously)
                            {
                                ((Action<IAsyncResult?>)readResult.AsyncState!).Invoke(readResult);
                            }
                        }, readWriteLoop);
                        if (!iar.CompletedSynchronously)
                        {
                            return;
                        }
                    }
                    else
                    {
                        int numRead = source.EndRead(iar!);
                        if (numRead == 0)
                        {
                            asyncResult.Complete(null);
                            callback?.Invoke(asyncResult);
                            return;
                        }

                        iar = destination.BeginWrite(buffer, 0, numRead, writeResult =>
                        {
                            if (!writeResult.CompletedSynchronously)
                            {
                                try
                                {
                                    destination.EndWrite(writeResult);
                                    readWriteLoop.Invoke(null);
                                }
                                catch (Exception e2)
                                {
                                    asyncResult.Complete(e2);
                                    callback?.Invoke(asyncResult);
                                }
                            }
                        }, null);
                        if (!iar.CompletedSynchronously)
                        {
                            return;
                        }
                        destination.EndWrite(iar);
                    }
                }
            }
            catch (Exception e)
            {
               asyncResult.Complete(e);
               callback?.Invoke(asyncResult);
            }
        };
        
        readWriteLoop(null);

        return asyncResult;
    }

    public void EndCopyStreamToStream(IAsyncResult asyncResult)
    {
        if (asyncResult is not MyAsyncResult ar)
        {
            throw new ArgumentException(null, nameof(asyncResult));
        }

        ar.Wait();
    }

    private sealed class MyAsyncResult : IAsyncResult
    {
        private bool _isCompleted;
        private int _completedSynchronously;
        private ManualResetEvent? _event;
        private Exception? _error;

        public MyAsyncResult(object? state) => AsyncState = state;

        public object? AsyncState { get; }

        public void Complete(Exception? error)
        {
            lock (this)
            {
                _isCompleted = true;
                _error = error;
                _event?.Set();
            }
        }

        public void Wait()
        {
            WaitHandle? h = null;

            lock (this)
            {
                if (_isCompleted)
                {
                    if (_error is not null)
                    {
                        throw _error;
                    }

                    return;
                }

                h = _event ??= new ManualResetEvent(false);
            }

            h.WaitOne();
            if (_error is not null)
            {
                throw _error;
            }
        }

        public WaitHandle AsyncWaitHandle
        {
            get
            {
                lock (this)
                {
                    return _event ??= new ManualResetEvent(_isCompleted);
                }
            }
        }

        public bool CompletedSynchronously
        {
            get
            {
                lock (this)
                {
                    if (_completedSynchronously == 0)
                    {
                        _completedSynchronously = _isCompleted ? 1 : -1;
                    }

                    return _completedSynchronously == 1;
                }
            }
        }

        public bool IsCompleted
        {
            get
            {
                lock (this)
                {
                    return _isCompleted;
                }
            }
        }
    }
}