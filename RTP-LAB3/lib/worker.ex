defmodule Worker do
    use GenServer

    #link this with that thingy...
    def start_link(index) do
        worker = "Worker"<>Integer.to_string(index)
        GenServer.start_link(__MODULE__, 0, name: String.to_atom(worker))
    end

    def handle_cast({:receive, tweet}, 0) do
      #decode JSON

      decoded_tweet = ""

          if (!String.contains?(tweet.data, "panic")) do
            {:ok, decoded_tweet} = Poison.decode(tweet.data)
            GenServer.cast(:Sink, {:receive, decoded_tweet})
          end

           # I actually like pipes..... functions are self-explanatory
           #if (decoded_tweet!="") do
           #score = decoded_tweet
           #|> prepare_string()
           #|> get_final_score()
           #end

          #send sentiment score + the tweet itself



          #GenServer.cast(:Sink, {:receive, decoded_tweet})


        {:noreply, 0}
      end

      #must be here or it dies IDK
    def init(0) do
        {:ok, 0}
      end

      #WIP - prepare the string for  calculations. Remove punctuation and extra spaces
      def prepare_string(text) do
        try do
        text["message"]["tweet"]["text"]
        |> String.replace([".", ":", "!", "?", ","], "")
        |>String.split(" ", trim: true)
        rescue
          RuntimeError -> IO.puts("error")
        end

    end

    #get sum_of_sentiments / number of words
    def get_final_score(text) do
      text
      |> Enum.reduce(0, fn word, sum -> Sentimentlist.get(word) + sum end)
      |> Kernel./(length(text))
    end

end
