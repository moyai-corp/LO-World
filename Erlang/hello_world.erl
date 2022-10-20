-module(hello_world).
-compile(export_all).

start() ->
    io:format("Hello, World!\n").