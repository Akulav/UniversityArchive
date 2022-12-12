defmodule Broker do
  require Logger

  @doc """
  Starts accepting connections on the given `port`.
  """
  def accept(port) do
    {:ok, socket} = :gen_tcp.listen(port,
                      [:binary, active: false, reuseaddr: true])
    Logger.info "Accepting connections on port #{port}"
    list = socket
    loop_acceptor(socket, list)
  end

  defp loop_acceptor(socket, list) do
    {:ok, client} = :gen_tcp.accept(socket)
    list = list ++ [client]
    if (length(list)<1) do
    {:ok, pid} = Task.Supervisor.start_child(Broker.TaskSupervisor, fn -> serve(client, list) end)
    else
      {:ok, pid} = Task.Supervisor.start_child(Broker.TaskSupervisor, fn -> broad(client, list) end)
    end
    :ok = :gen_tcp.controlling_process(client, pid)
    loop_acceptor(socket, list)
  end

  defp serve(socket, list) do
    try do
    tweet = read_line(socket)
    write_line(tweet,socket)
    GenServer.cast(:Broadcast, {:receive,list},tweet)
    serve(socket)


  rescue
    RuntimeError -> IO.puts "there was an error"
  end

  end

  defp broad(socket, list) do
    try do
    socket
    |> read_line()
    |> write_line(socket)
    serve(socket)
  rescue
    RuntimeError -> IO.puts "there was an error"
  end

  end

  defp read_line(socket) do
    {:ok,rcv} = :gen_tcp.recv(socket, 0)
    #{:ok,data} = Poison.decode(rcv)
    IO.inspect(rcv)
    rcv
  end

  defp write_line(line, socket) do
    :gen_tcp.send(socket, line)
  end


end
