import 'dart:io';

void main() {
  Future<ServerSocket> server = ServerSocket.bind('127.0.0.1', 8080);
  server.then((ServerSocket server) {
    server.listen((Socket socket) {
      socket.listen((List<int> data) {
        socket.write('Hello, world!');
        socket.close();
      });
    });
  });
}
