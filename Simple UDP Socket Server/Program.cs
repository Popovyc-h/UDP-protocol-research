namespace SimpleUDPSocketServer;

using System.Net;
using System.Net.Sockets;
using System.Text;

internal class Program
{
    static void Main()
    {
        // 1. Створити Socket (InterNetwork, Dgram, Udp)
        using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        // 2. Bind до IPEndPoint(IPAddress.Any, 5005)
        var serverEndpoint = new IPEndPoint(IPAddress.Any, 5005);
        socket.Bind(serverEndpoint);

        // 3. Цикл:
        while (true)
        {
            //    - ReceiveFrom (використовуйте буфер 1024)
            var buffer = new byte[1024];
            EndPoint sender = new IPEndPoint(IPAddress.Any, 0);
           int bytesReceived = socket.ReceiveFrom(buffer, ref sender);

            //    - Отримати remoteEndPoint відправника
            string message = Encoding.UTF8.GetString(buffer, 0, bytesReceived);

            //    - Вивести дані
            Console.WriteLine($"{sender} -> {message}");

            //    - SendTo відповідь назад на той самий remoteEndPoint
            string responseBytes = "Message received\n";
            var messageBytes = Encoding.UTF8.GetBytes(responseBytes);

            socket.SendTo(messageBytes, sender);
        }
    }
}
