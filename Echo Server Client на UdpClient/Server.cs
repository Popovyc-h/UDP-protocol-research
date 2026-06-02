using System.Net.Sockets;
using System.Text;

namespace ConsoleApp2;

internal class Program
{
    static async Task Main(string[] args)
    {
        var server = new UdpClient(5005);

        while (true)
        {
            var result = await server.ReceiveAsync();

            string message = Encoding.UTF8.GetString(result.Buffer);

            Console.WriteLine($"{result.RemoteEndPoint} -> {message}");

            await server.SendAsync(result.Buffer, result.RemoteEndPoint);
        }
    }
}
