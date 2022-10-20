using System.Net;
using System.Net.Sockets;
using System.Text;

namespace learn;

public static class tcp_client {
    
    public static int Main() {
        StartClient();
        return 0;
    }

    private static void StartClient() {
        byte[] bytes = new byte[1024];
        try {
            IPHostEntry host = Dns.GetHostEntry("127.0.0.1");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint remoteEp = new(ipAddress, 8080);
            if (remoteEp is null) {
                throw new ArgumentNullException(nameof(remoteEp));
            }
            Socket sender = new(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try {
                sender.Connect(remoteEp);
                Console.WriteLine("Connected to {0}", sender.RemoteEndPoint);
                sender.Send(Encoding.ASCII.GetBytes("Hello, World!"));
                sender.Receive(bytes);
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
            } catch (ArgumentNullException ane) {
                Console.WriteLine("ArgumentNullException : {0}", ane);
            } catch (SocketException se) {
                Console.WriteLine("SocketException : {0}", se);
            } catch (Exception e) {
                Console.WriteLine("Unexpected exception : {0}", e);
            }
        } catch (Exception e) {
            Console.WriteLine(e.ToString());
        }
    }
    
}