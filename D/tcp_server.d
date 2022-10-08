import std.socket;
import std.stdio;
import std.string;

void main() {
    auto addr = getAddress("127.0.0.1", 8080)[0];
    auto sock = new TcpSocket();
    sock.bind(addr);
    sock.listen(5);

    while (sock.isAlive()) {
        auto client = sock.accept();
        writeln("Connection accepted");
        client.send("Hello, world!");
    }
}