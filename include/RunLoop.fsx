(*
   Copyright 2014 Ekon Benefits
   
   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   
   You may obtain a copy of the License at
       http://www.apache.org/licenses/LICENSE-2.0
       
   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*)

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
