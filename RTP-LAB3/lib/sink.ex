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
        #IO.inspect(decoded_tweet)
        GenServer.cast(:Tcpsender, {:receive,decoded_tweet})

    {:noreply, 0}
      end

      def send_forced() do
        GenServer.cast(:Sink, {:receive,"kill"})
      end

end
