Imports System.Net.Sockets
Imports System.Text

Module MainModule
    Dim _client As TcpClient
    Sub Main()
        Try
            _client = New TcpClient("127.0.0.1", 8080)
            Using networkStream As NetworkStream = _client.GetStream()
                While True
                    Dim toSend() As Byte = Encoding.ASCII.GetBytes(Console.ReadLine())
                    networkStream.Write(toSend, 0, toSend.Length)
                    Dim toReceive(100000) As Byte
                    Dim length As Integer = networkStream.Read(toReceive, 0, toReceive.Length)
                    Dim text As String = Encoding.ASCII.GetString(toReceive, 0, length)
                    Console.WriteLine(text)
                    Console.WriteLine()
                End While
            End Using
        Catch ex As Exception
            Console.WriteLine(ex)
        End Try
        Console.ReadLine()
    End Sub
End Module