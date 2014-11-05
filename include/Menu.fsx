module Menu

#load "RunLoop.fsx"
#load "Console.fsx"

open System

let colorShadow = ConsoleColor.Black
let colorsTitle = ConsoleColor.DarkRed, ConsoleColor.White
let colorsMenu = ConsoleColor.DarkGreen, ConsoleColor.White
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

        Console.setColors colorsTitle
        let margin = 20
        let menuWidth = 40
        let menuStart = 4
        Console.say (menuStart, margin) title  menuWidth
        choices |> Seq.iteri (fun i item ->
                            Console.setBgColor colorShadow
                            Console.say (menuStart + 1 + i,margin - 1) "" 1
                            Console.setColors (if i = !choice then colorsHighlight else colorsMenu)
                            Console.say (menuStart + 1  + i,margin) item menuWidth)
        Console.setBgColor colorShadow
        Console.say (menuStart + 1 + maxChoice + 1,margin - 1) "" menuWidth

        match RunLoop.lastKey() with
        | Some Console.Key.Enter -> Some(!choice)
        | _____________________ -> None
    RunLoop.run draw
