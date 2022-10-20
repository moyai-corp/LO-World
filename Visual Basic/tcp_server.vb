Imports System.Net
Imports System.Net.Sockets
Imports System.Text

Module MainModule
    Dim _server As TcpListener
    
    Sub Main()
        Try
            _server = New TcpListener(IPAddress.Parse("127.0.0.1"), 8080)
            _server.Start()
            Threading.ThreadPool.QueueUserWorkItem(AddressOf NewClient)
        Catch ex As Exception
            Console.WriteLine(ex)
        End Try
        Console.ReadLine()
    End Sub

    Private Sub NewClient(state As Object)
        Try
            Using client As TcpClient = _server.AcceptTcpClient()
                Threading.ThreadPool.QueueUserWorkItem(AddressOf NewClient)
                Using networkStream As NetworkStream = client.GetStream()
                    While True
                        Dim toReceive(100000) As Byte
                        Dim length As Integer = networkStream.Read(toReceive, 0, toReceive.Length)
                        Dim text As String = Encoding.ASCII.GetString(toReceive, 0, length)
                        Console.WriteLine(text)
                        Console.WriteLine()
                        Dim toSend() As Byte = Encoding.ASCII.GetBytes("Message Received...")
                        networkStream.Write(toSend, 0, toSend.Length)
                    End While
                End Using
            End Using
        Catch ex As Exception
            Console.WriteLine(ex)
        End Try
    End Sub
End Module
