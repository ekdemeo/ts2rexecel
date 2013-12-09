namespace ts2rexcel

open Core

type RexcelConverter() = 

    member this.FromTs(lines: seq<string>) =
        processTasks lines
