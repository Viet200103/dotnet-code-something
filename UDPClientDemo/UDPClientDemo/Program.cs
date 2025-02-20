using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPClientDemo;

class Program
{

    private static void ConnectServer(string host, int port)
    {
        UdpClient client = new(host, port);
        IPAddress address = IPAddress.Parse(host);
        IPEndPoint remoteEndpoint = new(address, port);
        string message;
        int count = 0;
        bool done = false;
        Console.Title = "UDP Client";

        try
        {
            Console.WriteLine(new string('*', 40));
            client.Connect(remoteEndpoint);
            while (!done)
            {
                message = $"Message {++count:D2}";
                byte[] sendBytes = Encoding.ASCII.GetBytes(message);
                client.Send(sendBytes, sendBytes.Length);
                Console.WriteLine("Sent: {0}", message);
                Thread.Sleep(2000);
                if (count == 10)
                {
                    done = true;
                    Console.WriteLine("Done!");
                }
            }
        }
        catch (SocketException e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            client.Close();
        }
    }
    
    static void Main(string[] args)
    {
        string host = "127.0.0.1";
        int port = 11000;
        ConnectServer(host, port);
        Console.Read();
    }
}