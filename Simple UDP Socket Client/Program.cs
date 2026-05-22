using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SimpleUDPSocketClient;

internal class Program
{
    static void Main(string[] args)
    {
        try
        {
            using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            var buffer = new byte[1024];
            var serverEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5005);
            EndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            socket.ReceiveTimeout = 2000;

            while (true)
            {
                Console.Write("Enter message: ");
                string message = Console.ReadLine();

                var messageBytes = Encoding.UTF8.GetBytes(message);

                socket.SendTo(messageBytes, serverEndpoint);

                int bytesReceived = socket.ReceiveFrom(buffer, ref sender);
                string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesReceived);
                Console.WriteLine(receivedMessage);
            }
        }
        catch(SocketException ex)
        {
            Console.WriteLine("Timeout: сервер не відповів");
        }
    }
}
