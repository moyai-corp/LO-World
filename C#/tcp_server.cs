using System.Net;
using System.Net.Sockets;
using System.Text;

namespace learn;

public static class tcp_server {
    
    public static int Main() {
        StartServer();
        return 0;
    }

    private static void StartServer() {
        IPHostEntry host = Dns.GetHostEntry("127.0.0.1");
        IPAddress ipAddress = host.AddressList[0];
        IPEndPoint localEndPoint = new(ipAddress, 8080);
        try {
            Socket listener = new(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(localEndPoint);
            listener.Listen(10);
            Socket handler = listener.Accept();
            string? data = null;
            while (true) {
                byte[] bytes = new byte[1024];
                int bytesRec = handler.Receive(bytes);
                data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                if (data.IndexOf("Hello, World!", StringComparison.Ordinal) > -1) {
                    break;
                }
            }
            Console.WriteLine("Text received : {0}", data);
            handler.Send(Encoding.ASCII.GetBytes(data));
            handler.Shutdown(SocketShutdown.Both);
            handler.Close();
        } catch (Exception e) {
            Console.WriteLine(e.ToString());
        }
        Console.ReadKey();
    }
    
}