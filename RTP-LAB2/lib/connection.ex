defmodule Connection do

  # infinite loop. Send constantly tweets to the router
  def recv() do
    receive do
      tweet-> Router.route(tweet)
    end
    recv()
  end

  def connect() do
    pid = spawn_link(__MODULE__, :recv, [])
    {:ok, _pid1} = EventsourceEx.new("localhost:4000/tweets/2", stream_to: pid)
    {:ok, _pid2} = EventsourceEx.new("localhost:4000/tweets/1", stream_to: pid)
    {:ok, self()}
  end
end
