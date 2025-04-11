using System.Net;
using System.Text;

namespace SimpleWebServer;

public class SimpleHttpServer : IDisposable
{
    readonly HttpListener _listener = new HttpListener();

    public SimpleHttpServer()
    {
        ListenAsync().Wait();
    }

    async Task ListenAsync()
    {
        _listener.Prefixes.Add("http://localhost:51111/MyApp/");
        _listener.Start();

        while (_listener.IsListening)
        {
            try
            {
                var context = await _listener.GetContextAsync();

                string msg = "You asked for: " + context.Request.RawUrl;
                context.Response.StatusCode = (int)HttpStatusCode.OK;

                byte[] buffer = Encoding.UTF8.GetBytes(msg);
                context.Response.ContentLength64 = buffer.Length;

                using var output = context.Response.OutputStream;
                await output.WriteAsync(buffer, 0, buffer.Length);
                await output.FlushAsync(); // flush đảm bảo toàn bộ dữ liệu được gửi
            }
            catch (HttpListenerException ex)
            {
                Console.WriteLine("Listener stopped: " + ex.Message);
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unhandled error: " + ex.Message);
            }
        }
    }
    
    public void Dispose()
    {
        _listener.Close();
    }
}
