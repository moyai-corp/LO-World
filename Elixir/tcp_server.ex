defmodule TcpServer do
  use GenServer

  def accept(socket) do
    {:ok, client_socket} = :gen_tcp.accept(socket)
    :gen_tcp.send(client_socket, "Hello, world!")
    :gen_tcp.close(client_socket)
  end

  def init(state) do
    {:ok, socket} = :gen_tcp.listen(8080, [:binary,
                                           reuseaddr: true])
    {:ok, %{state | socket: socket}}
    if !:ok do
      IO.puts("Error while trying to listen on port 8080")
      exit(:shutdown)
    end
    for _ <- 1..10 do spawn(fn -> accept(socket) end) end
    Process.sleep(:infinity)
  end
end

TcpServer.init(%{socket: nil})
