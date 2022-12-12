defmodule Client do
  require Logger

  def connect(port) do
    {:ok, socket} = :gen_tcp.connect('212.28.73.222', port, [:binary, active: false])
    IO.inspect(socket)
    :gen_tcp.send(socket, "abc")
    # loop_acceptor(socket)
  end



  defp read_line(socket) do
    {:ok, data} = :gen_tcp.recv(socket, 0)
    data
  end

  defp write_line(line, socket) do
    :gen_tcp.send(socket, line)
  end

  defp loop_acceptor(socket) do
    {:ok, client} = :gen_tcp.accept(socket)
    IO.inspect(client)
    :gen_tcp.send(client, "abc")
    # serve(client)
    # loop_acceptor(socket)
  end

  defp serve(socket) do
    socket
    |> read_line()
    |> write_line(socket)

    serve(socket)
  end

end
