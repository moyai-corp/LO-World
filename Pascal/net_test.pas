program TestApp;
 
uses
    blcksock;
     
var
    sock: TTCPBlockSocket;
     
procedure Main();
var
    buffer: String = '';
begin
    sock := TTCPBlockSocket.Create;
     
    sock.Connect('66.79.183.71', '80');
    // Was there an error?
    if sock.LastError <> 0 then
    begin
        writeLn('Could not connect to server.');
        halt(1);
    end;
    // Send a HTTP request
    sock.SendString('GET /blog/ HTTP/1.1'#13#10'Host: www.daniel15.com'#13#10#13#10);
     
    // Keep looping...
    repeat
        buffer := sock.RecvPacket(2000);
        write(buffer);
    // ...until there's no more data.
    until buffer = '';
end;
 
 
begin
    Main();
end.
