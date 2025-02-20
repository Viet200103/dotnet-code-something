using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TcpServerDemo;

class Program
{
    static void ProcessMessage(object parm)
    {
        string data;
        int count;
        try
        {
            TcpClient client = parm as TcpClient ?? throw new InvalidOperationException();
            Byte[] bytes = new Byte[256];
            NetworkStream stream = client.GetStream();
            while ((count = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                data = Encoding.ASCII.GetString(bytes, 0, count);
                Console.WriteLine($"Received: {data} at {DateTime.Now:t}");
                data = $"{data.ToUpper()}";
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
                stream.Write(msg, 0, msg.Length);
                Console.WriteLine($"Sent: {data}");
            }
            client.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("{0}", e.Message);
            Console.WriteLine("Wait message...");
        }
    }

    static void ExecuteServer(string host, int port)
    {
        int Count = 0;
        TcpListener server = null;
        try
        {
            Console.Title = "Server Application";
            IPAddress localAddr = IPAddress.Parse(host);
            server = new TcpListener(localAddr, port);
            server.Start();
            Console.WriteLine(new string('*', 40));
            Console.WriteLine("Waiting for a connection...");
            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine($"Number of clients: {++Count}");
                Console.WriteLine(new string('*', 40));
                Thread thread = new Thread(ProcessMessage);
                thread.Start(client);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            server.Stop();
            Console.WriteLine("Server is stopped");
            Console.ReadKey();
        }
    }

    public static void Main(string[] args)
    {
        string host = "127.0.0.1";
        int port = 13000;
        ExecuteServer(host, port);
    }
}