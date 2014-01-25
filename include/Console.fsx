module Console

open System

type Key = ConsoleKey

let hideCursor () =
    Console.CursorVisible <- false

let setBgColor bg =
    Console.BackgroundColor <- bg
let setFgColor fg =
        Console.ForegroundColor <- fg

let setColors (bg, fg) =
    setBgColor bg
    setFgColor fg
    
let resetColor () =
     Console.ResetColor()
     
let clear () =
    Console.Clear()
    
let readKey () =
    Console.ReadKey(false).Key    
    
let write (x:string) =
     Console.Write(x)

let setPosition (x, y) =
    Console.SetCursorPosition(x,y)

