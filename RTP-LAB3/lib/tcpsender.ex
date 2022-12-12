defmodule Tcpsender do
  use GenServer

  def connect(port) do
    {:ok, socket} = :gen_tcp.connect('localhost', port, [:binary, active: false])
    # loop_acceptor(socket)
  end

  def send(text, socket) do
    :gen_tcp.send(socket, text)
  end

  def start_link(index) do
    GenServer.start_link(__MODULE__, 0, name: :Tcpsender)
  end

  def init(0) do
    {:ok, 0}
  end

  def handle_cast({:receive, text}, 0) do
    {:ok, socket} = :gen_tcp.connect('212.28.73.222', 25565, [:binary, active: false])
    encoded_tweet = Poison.encode!(text)

    #lungime = String.length(encoded_tweet)
    #batches = lungime / 50
    #IO.puts(batches)


    #list_data = List.flatten(Regex.scan(~r/.../, encoded_tweet))

    #Enum.each(list_data, fn(s) -> :gen_tcp.send(socket, s) end)
    #:gen_tcp.send(socket, "END")

    #IO.puts(List.first(list_data))
    IO.puts(encoded_tweet)
    :gen_tcp.send(socket, encoded_tweet)
    #Socket.Stream.send(socket, encoded_tweet)
    #Process.sleep(5000)
    {:noreply, 0}
  end


end
