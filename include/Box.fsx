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

module Box

#load "RunLoop.fsx"
#load "Console.fsx"

type private promptItem = { position:(int * int); text:string; defaultValue:string option; value: string ref;}
let private sayItem:promptItem = { position = (0,0); text =""; defaultValue=None; value = ref ""}

let private topBox = "╔═╗"
let private midBox = "║ ║"
let private botBox = "╚═╝"

let private boxColors = Console.Color.DarkMagenta, Console.Color.White
let private inputColors = Console.Color.DarkRed, Console.Color.White

type DisplayBox () =
    let mutable items: promptItem list = List.empty
    member this.Say (row, col) text =
        items <- { sayItem with position=(row,col); text=text; } :: items
    member this.Prompt (x, y) text defaultValue =
        let item = { position=(x,y); text=text; defaultValue=Some(defaultValue); value=ref defaultValue}
        items <- item :: items
        item.value
    member this.Display() =
        let draw () =
          let max = items |> List.map (fun x-> x.text.Length) |> List.max
          Console.setColors boxColors
          let rowStart =6
          let colStart =5
          Console.say (rowStart,  colStart) (topBox.[0].ToString()) 1
          Console.say (rowStart,colStart + 1) (String.replicate (max + 2) (topBox.[1].ToString())) 1
          Console.say (rowStart, colStart + max + 3) (topBox.[2].ToString()) 1
          for i, item in items |> List.rev |> List.mapi (fun i x -> i+1,x)  do
                Console.say (rowStart+i,colStart) (midBox.[0].ToString()) 1
                Console.say (rowStart+i,colStart+1) " " 1
                Console.say (rowStart+i,colStart+2) item.text (max + 2)
                Console.say (rowStart+i, colStart+2+max) " " 1
                Console.say (rowStart+i, colStart+3+max) (midBox.[2].ToString()) 1
          let afterItems =(items |> List.length) + 1

          Console.say (rowStart + afterItems, colStart) (botBox.[0].ToString()) 1
          Console.say (rowStart + afterItems, colStart + 1) (String.replicate (max + 2) (botBox.[1].ToString())) 1
          Console.say ( rowStart + afterItems, colStart + max + 3) (botBox.[2].ToString()) 1
          match RunLoop.lastKey() with
          | Some Console.Key.Enter -> Some(1)
          | _____________________ -> None
        RunLoop.run draw




let start () = DisplayBox()
