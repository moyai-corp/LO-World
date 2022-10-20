package main

import net "shared:github.com/restartfu/odin-net"

main :: proc () {
    l, ok := net.tcp_listen(":8080")
    if !ok{
        panic("couldn't start listening")
    }
    for{
        conn, ok := net.accept(l)
        if !ok{
            return
        }
        net.write_string(conn, "Hello World!")
    }
}