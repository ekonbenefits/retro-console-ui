module Menu

#load "RunLoop.fsx"
#load "Console.fsx"

open System

let colorApp = ConsoleColor.Blue
let colorsTitle = ConsoleColor.DarkRed, ConsoleColor.White
let colorsMenu = ConsoleColor.Green, ConsoleColor.White
let colorsHighlight = ConsoleColor.White, ConsoleColor.DarkRed
let colorsSelected = ConsoleColor.White, ConsoleColor.Red

let show title (choices: string seq) =
    let choice = ref 0
    let maxChoice = (Seq.length choices) - 1
    let circular maximum value =
        if value > maximum then 0 else value
    let draw () : int option =
        Console.hideCursor()
        choice := match RunLoop.lastKey() with
                  | Some Console.Key.UpArrow -> max 0 (!choice - 1)
                  | Some Console.Key.DownArrow -> min maxChoice (!choice + 1)
                  | Some Console.Key.Tab -> circular maxChoice (!choice + 1)
                  | ______________ -> !choice
        
        Console.setBgColor colorApp
        Console.clear()
        Console.setPosition(4, 5)
        Console.setColors colorsTitle
        Console.write("            " + title + "            ")
        choices 
            |> Seq.iteri (fun i item -> 
                        Console.setPosition(4, 6+i);
                        Console.setColors (if i = !choice then colorsHighlight else colorsMenu)
                        Console.write("            " + item + "            ")
                        )
            
        match RunLoop.lastKey() with
        | Some Console.Key.Enter -> Some(!choice)
        | _____________________ -> None
    RunLoop.run draw

