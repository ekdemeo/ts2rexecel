namespace ts2rexcel

open ts2rexcel.Core
module Console =

let readLines filePath = System.IO.File.ReadLines(filePath, System.Text.Encoding.UTF8)
let writeLines lines filePath = System.IO.File.WriteAllLines(filePath, lines)

[<EntryPoint>]
let main argv = 
    if (argv.Length <> 2) then 
        printf "Usage: ts2rexecel.exe inputfile outputfile"
        0
    else 
        let output = (ts2rexcel.Core.processTasks <| readLines argv.[0])
        writeLines (Seq.toArray output) argv.[1]
        printfn "%s" "done."
        0