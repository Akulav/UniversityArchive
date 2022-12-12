defmodule Worker do
    use GenServer

    #link this with that thingy...
    def start_link(index) do
        worker = "Worker"<>Integer.to_string(index)
        GenServer.start_link(__MODULE__, 0, name: String.to_atom(worker))
    end

    def handle_cast({:receive, tweet}, 0) do
      #decode JSON

          {:ok, decoded_tweet} = Poison.decode(tweet.data)

           # I actually like pipes..... functions are self-explanatory
           score = decoded_tweet
           |> prepare_string()
           |> get_final_score()

          #send sentiment score + the tweet itself
          GenServer.cast(:Sink, {:receive, decoded_tweet})
        {:noreply, 0}
      end

      #must be here or it dies IDK
    def init(0) do
        {:ok, 0}
      end

      #WIP - prepare the string for  calculations. Remove punctuation and extra spaces
      def prepare_string(text) do
        text["message"]["tweet"]["text"]
        |> String.replace([".", ":", "!", "?", ","], "")
        |>String.split(" ", trim: true)
    end

    #get sum_of_sentiments / number of words
    def get_final_score(text) do
      text
      |> Enum.reduce(0, fn word, sum -> Sentimentlist.get(word) + sum end)
      |> Kernel./(length(text))
    end

end
