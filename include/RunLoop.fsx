module RunLoop

#load "Console.fsx"

let mutable private mutlastKey = None 

let lastKey () = mutlastKey

let private readSomeKey () = Some(Console.readKey())

let private repeatKeyTest key =
    mutlastKey = key && Console.ready()

let rec private lastKeyHelper key =
    if repeatKeyTest key  then
        lastKeyHelper <| readSomeKey()
    else
        key

let rec private runloopHelper key repeatKeyList func = 
    let newKeyList = if repeatKeyTest key then
                        key::repeatKeyList
                      else
                        List.empty
    mutlastKey <- key
    let ret = func()
    match ret with
    | None ->
        let readKey =   if List.length newKeyList > 5 then
                            lastKeyHelper <| readSomeKey()
                        else
                            readSomeKey()

        match readKey with
        | Some(Console.Key.Escape) -> Console.resetColor(); None 
        | __________________ -> runloopHelper readKey newKeyList func
    | Some _ -> ret

let run (func: unit -> _ option) = runloopHelper None List.empty func