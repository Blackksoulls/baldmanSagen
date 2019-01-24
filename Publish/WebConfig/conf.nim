import asynchttpserver, asyncdispatch
import libs/regex
import json
import browsers

var server = newAsyncHttpServer()

proc getMatch(s: string, p: Regex): string =
    var m: RegexMatch
    doAssert s.match(p, m)
    result = s[m.group(0)[0]]

proc main(req: Request) {.async.} =
    case req.reqMethod:
        of HttpGet:
            case req.url.path:
                of "/":
                    var content = readFile("./prompt.html")
                    await req.respond(Http200, content)
                of "/libs/tailwind.min.css":
                    var content = readFile("./libs/tailwind.min.css")
                    await req.respond(Http200, content)
                else:
                    await req.respond(Http300, """<!DOCTYPE html><html><head><meta http-equiv="refresh" content="0; url=http://127.0.0.1:4242/"/></head></html>>""")
        of HttpPost:
            case req.url.path:
                of "/validate":
                    var preset = req.body.getMatch(re".*preset=([0-9])&.*")
                    var join = req.body.getMatch(re".*joingame=([0-9]*)s&.*")
                    var act = req.body.getMatch(re".*actionight=([0-9]*)s&.*")
                    var nf1 = req.body.getMatch(re".*nf1=([0-9]*)s&.*")
                    var nf2 = req.body.getMatch(re".*nf2=([0-9]*)s&.*")
                    var quick = req.body.getMatch(re".*quick=([0-9]*)s&.*")
                    var death = req.body.getMatch(re".*death=([0-9]*)s&.*")
                    var remind = req.body.getMatch(re".*fresh=([0-9]*)s&.*")
                    var vote = req.body.getMatch(re".*voteday=([0-9]*)s.*")
                    
                    var json = %* {
                        "join_time": join & "000",
                        "night_action_time": act & "000",
                        "night_phase_1_time": nf1 & "000",
                        "night_phase_2_time": nf2 & "000",
                        "preset": preset,
                        "day_vote_time": vote & "000",
                        "quick_action_time": quick & "000",
                        "wait_after_death_time": death & "000",
                        "refresh_remaining_time": remind & "000",
                    }
                    var f = open("game-settings.json", fmWrite)
                    write(f, json.pretty())
                    var content = readFile("./success.html")
                    waitFor req.respond(Http200, content)
                    quit(0)
                else:
                    var json = %*{"res":"access denied"}
                    await req.respond(Http403, json.pretty())
        else:
            await req.respond(Http403, "Access Denied!")

openDefaultBrowser("http://127.0.0.1:4242")

waitFor server.serve(Port(4242), main)