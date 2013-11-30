// Constans
let TAB = "\t"
let NEWLINE = "\n"
let EMPTY = "";

// Types
type Task(taskName : string, person: string, date: string, hours: string) = 
        member this.TaskName = taskName  
        member this.Person = person
        member this.Date = System.DateTime.Parse(date)
        member this.Hours = System.Double.Parse(hours)

        override x.Equals(y) = match y with
            | :? Task as y -> x.TaskName.Equals(y.TaskName)
            | _ -> false

type TaskGroup(tasks: seq<Task>) = 
        let task = Seq.head tasks
        let sortedTasks = Seq.sortBy (fun (x:Task) -> x.Date) tasks
    
        member this.TaskName = task.TaskName  
        member this.Person = task.Person
        member this.StartDate = (sortedTasks |> Seq.head).Date
        member this.EndDate = (sortedTasks |> Seq.last).Date
        member this.Hours = tasks |> Seq.sumBy (fun (x:Task) -> x.Hours) 
        member this.Print = 
            this.TaskName + TAB + 
            EMPTY     + TAB + 
            EMPTY        + TAB + 
            TAB + TAB + TAB + TAB + TAB + 
            this.StartDate.ToString("yyyy-MM-dd") + TAB + 
            this.EndDate.ToString("yyyy-MM-dd")   + TAB +
            this.Hours.ToString() + TAB + 
            this.Person

let parseTask (line:string) = 
    let cells = line.Split TAB.[0]
    in Task(cells.[6], cells.[1],  cells.[0], cells.[2])

let tasks lines = seq { for line in lines -> parseTask line }

let groupTasks tasks = tasks |> Seq.groupBy (fun (x:Task) -> x.TaskName)  |> Seq.map (fun (key, tasks) -> TaskGroup(tasks))

let processTasks lines = groupTasks <| tasks lines |> Seq.map (fun (x:TaskGroup) -> x.Print)

let readLines filePath = System.IO.File.ReadLines(filePath, System.Text.Encoding.UTF8);;

let writeLines lines filePath = System.IO.File.WriteAllLines(filePath, lines)


[<EntryPoint>]
let main argv = 
    let output = (processTasks <| readLines argv.[0])
    //printfn "%s" (output  |> Seq.reduce (+))
    writeLines (Seq.toArray output) argv.[1]
    printfn "%s" "done."
 
    0