#defmodule CustomStruct do
#  use Mongo
#  @fields [:a, :b, :c, :id]
#  @enforce_keys @fields
#  defstruct @fields

#  defimpl Mongo.Encoder do
#    def encode(%{a: a, b: b, id: id}) do
#      %{
#        _id: id,
#        a: a,
#        b: b,
#        custom_encoded: true
#     }
#   end
#  end
#end