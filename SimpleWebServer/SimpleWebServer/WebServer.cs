using System.Net;
using System.Text;
using System.Text.Unicode;

namespace SimpleWebServer;

public class WebServer 
{
    
    private HttpListener _listener;
    private string _baseFolder;

    public WebServer(string uriPrefix, string baseFolder)
    {
        _baseFolder = baseFolder;
        _listener = new HttpListener();
        _listener.Prefixes.Add(uriPrefix);
    }

    public async Task Start()
    {

        _listener.Start();

        while (_listener.IsListening)
        {
            try
            {
                var context = await _listener.GetContextAsync();
                await Task.Run(() => ProcessRequest(context));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }

    private async Task ProcessRequest(HttpListenerContext context)
    {
        try
        {
            string? fileName = Path.GetFileName(context.Request.RawUrl) ?? "";
            string path = Path.Combine(_baseFolder, fileName);
            byte[] msg;
            if (!File.Exists(path))
            {
                Console.WriteLine("Resource not found: "  + path);
                context.Response.StatusCode = (int) HttpStatusCode.NotFound;
                msg = Encoding.UTF8.GetBytes("Resource not found: " + path);
            }
            else
            {
                context.Response.StatusCode = (int) HttpStatusCode.OK;
                msg = File.ReadAllBytes(path);
            }
            
            context.Response.ContentLength64 = msg.Length;
            await using Stream s = context.Response.OutputStream;
            await s.WriteAsync(msg, 0, msg.Length);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }


    public void Stop()
    {
        _listener.Stop();
    }
}