namespace ts2rexcel

module Core = 

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
        let TAB = "\t"
        let NEWLINE = System.Environment.NewLine
        let EMPTY = System.String.Empty
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



   let parseTask (line:string) = let cells = line.Split '\t' in Task(cells.[6], cells.[1],  cells.[0], cells.[2])
    let tasks lines = seq { for line in lines -> parseTask line }
    let groupTasks tasks = tasks |> Seq.groupBy (fun (x:Task) -> x.TaskName)  |> Seq.map (fun (key, tasks) -> TaskGroup(tasks))
    let processTasks lines = groupTasks <| tasks lines |> Seq.map (fun (x:TaskGroup) -> x.Print)
   

   


