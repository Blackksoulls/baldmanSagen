import asynchttpserver, asyncdispatch
import json
import browsers

var server = newAsyncHttpServer()

proc main(req: Request) {.async.} =
    case req.reqMethod:
        of HttpGet:
            case req.url.path:
                of "/":
                    await req.respond(Http200, "Hello World!")
                else:
                    await req.respond(Http300, """<!DOCTYPE html><html><head><meta http-equiv="refresh" content="0; url=http://0.0.0.0:4242/"/></head></html>>""")
        of HttpPost:
            case req.url.path:
                of "/validate":
                    var json = %* {"res":"it work"}
                    await req.respond(Http200, json.pretty())
        else:
            await req.respond(Http403, "Access Denied!")

openDefaultBrowser("http://0.0.0.0:4242")

waitFor server.serve(Port(4242), main)