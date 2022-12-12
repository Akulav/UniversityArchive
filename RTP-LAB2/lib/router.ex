defmodule Router do
  use GenServer

  # self-explanatory
  def start_link() do
        GenServer.start_link(__MODULE__, %{index: 0,worker_cardinal: 4}, name: :Router)
  end


  # send the tweet to the handlecaster. using genserver to assure a loop for the workers
  def route(tweet) do
    GenServer.cast(:Router,{:send,tweet})
  end

  # oxygen (needed to exist)
  def init(state) do
    {:ok, state}
  end

  # send the tweet to worker.
  def handle_cast({:send, tweet}, state) do

    # calculate the index of the worker for the task using modulo
    worker = rem(state.index,state.worker_cardinal)
    GenServer.cast(String.to_atom("Worker"<>Integer.to_string(worker)) ,{:receive,tweet})

    # increase index for next worker in line
    new_state = %{index: state.index + 1, worker_cardinal: state.worker_cardinal}
    {:noreply, new_state}

  end


end
