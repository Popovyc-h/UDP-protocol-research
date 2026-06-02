using System.Net;
using System.Net.Sockets;
using System.Text;

namespace EchoClient;

internal class Program
{
    static async Task Main(string[] args)
    {
        using var client = new UdpClient();
        var serverEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5005);
        var buffer = new byte[1024];

        while (true)
        {
            Console.Write("\nВведіть повідомлення: ");
            string message = Console.ReadLine();

            var messageByte = Encoding.UTF8.GetBytes(message);
            await client.SendAsync(messageByte, serverEndpoint);

            var result = await client.ReceiveAsync();

            string str = Encoding.UTF8.GetString(result.Buffer);

            Console.WriteLine($">> Echo: {str}");
        }
    }
}
