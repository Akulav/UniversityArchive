defmodule Lab1.Application do

  use Application

  #I don't know what impl true does
  @impl true

  #will start these threads while restarting them in case of failure.
  def start(_type, _args) do
    children = [
      %{
        id: Worker0,
        start: {Worker, :start_link, [0]}
      },

      %{
        id: Worker1,
        start: {Worker, :start_link, [1]}
      },

      %{
        id: Worker2,
        start: {Worker, :start_link, [2]}
      },

      %{
        id: Worker3,
        start: {Worker, :start_link, [3]}
      },

      %{
        id: Router,
        start: {Router, :start_link, []}
      },

      %{
        id: Sink,
        start: {Sink, :start_link, [0]}
       },

       #%{
       # id: Mongoc,
       # start: {Mongoc, :start_link, [0]}
       #},

       %{
        id: Timeout,
        start: {Timeout, :start_link, [0]}
       },

      %{
        id: Connection,
        start: {Connection, :connect, []}
      },

      %{
        id: Tcpsender,
        start: {Tcpsender, :start_link, [0]}
      }

    ]

    # max number of restart 100 per 1 sec should be enough to never crash entirely, maybe...
    opts = [strategy: :one_for_one, name: Lab1.Supervisor, max_restarts: 999, max_seconds: 1]
    Supervisor.start_link(children, opts)

  end
end
