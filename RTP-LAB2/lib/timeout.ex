 defmodule Timeout do
  use GenServer

  def start_link(index) do
    GenServer.start_link(__MODULE__, 0, name: :Timeout)
  end


  def init(0) do
    {:ok, 0}
  end

  def handle_cast({:pid, pid}, 0) do

    Process.sleep(2000)

    Sink.send_forced()
    {:noreply, 0}

  end


end
