defmodule Mongoc do
  use GenServer
  require Mongo

  def start_link(0) do
    GenServer.start_link(__MODULE__, 0, name: :Mongoc)
  end

  def init(0) do
    {:ok, pid} = Mongo.start_link(url: "YOUR URL HERE")
    {:ok, pid}
  end

    def handle_cast({:receive, decoded_tweet}, state) do
      
      
      Mongo.insert_many(state, "DB NAME", [decoded_tweet])
      {:noreply, 0}
    end  

end
