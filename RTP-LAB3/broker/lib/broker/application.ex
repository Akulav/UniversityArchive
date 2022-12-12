defmodule Broker.Application do

  use Application

  @impl true
  def start(_type, _args) do

    children = [
      {Task.Supervisor, name: Broker.TaskSupervisor},
      Supervisor.child_spec({Task, fn -> Broker.accept(25565) end}, restart: :permanent),
      %{
        id: Broadcast,
        start: {Broadcast, :start_link, [0]}
      }
    ]

    opts = [strategy: :one_for_one, name: Broker.Supervisor]
    Supervisor.start_link(children, opts)
  end
end
