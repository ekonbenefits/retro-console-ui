module RunLoop

#load "Console.fsx"

let mutable private mutlastKey = None 

let lastKey () = mutlastKey

let rec private runloopHelper key func = 
    mutlastKey <- key
    let ret = func()
    match ret with
    | None ->
        let readKey = Console.readKey()
        match readKey with
        | Console.Key.Escape -> Console.resetColor(); None 
        | _________________ -> runloopHelper (Some(readKey)) func
    | Some _ -> ret

let run (func: unit -> _ option) = runloopHelper None func