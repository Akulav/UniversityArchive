defmodule Sink do
    use GenServer

    def start_link(index) do
        GenServer.start_link(__MODULE__, 0, name: :Sink)
    end

    def init(0) do
      {:ok, 0}
    end

    #when called just add the new tweet to the
      def handle_cast({:receive, decoded_tweet}, pid) do

      if decoded_tweet=="kill" do
        Genserver.cast(:Mongoc, decoded_tweet)
        
      end

      new_decoded_tweet = [decoded_tweet]

      

      if length(new_decoded_tweet) >=1 do

        GenServer.cast(:Timeout, {:pid, pid}) #Start timer only after the first item is in batch.

        if length(new_decoded_tweet) <4 do
            new_decoded_tweet = new_decoded_tweet ++ [decoded_tweet]
        end
      end

      GenServer.cast(:Sink, {:receive, new_decoded_tweet})

      if length(decoded_tweet) >= 4 do
        Genserver.cast(:Mongoc, decoded_tweet)
      end
    {:noreply, 0}
      end

      def send_forced() do
        Genserver.cast(:Sink, {:receive,"kill"})
      end

end
