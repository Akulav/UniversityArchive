defmodule Broker do
  use GenServer

  def start_link(index) do
      GenServer.start_link(__MODULE__, 0, name: :Broadcast)
  end

  def init(0) do
    {:ok, 0}
  end

  def handle_cast({:receive, client}, tweet) do


    if(String.contains?(tweet,"subscribe"))do
      if(!String.contains?(tweet,"un"))do
    if (length(client)==1) do

      client_list = client



  else
    client_list = client_list ++ [client]
  end

  end



    end

    if(String.contains?(tweet,"unsubscribe"))do
      List.delete(client_list, client)
    end


    if(!String.contains?(tweet,"subscribe"))do

    {:ok, whatever} = Poison.decode!(tweet)
    end
    topic_user = whatever[:user]
    topic_tweet = whatever[:tweet]



    Enum.each(client_list, fn(s) -> :gen_tcp.send(s, topic_user) end)
    Enum.each(client_list, fn(s) -> :gen_tcp.send(s, topic_tweet) end)
    GenServer.cast(:Broadcast, {:receive,client}, "")
    {:noreply, 0}
  end




end
