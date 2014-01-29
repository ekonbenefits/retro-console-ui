module Console

open System
open System.IO

type Key = ConsoleKey
type Color = ConsoleColor

type Buff = {
    mutable Char: char
    mutable Fg: ConsoleColor
    mutable Bg: ConsoleColor
    }
    
let clearBuff () = Array2D.init Console.WindowHeight Console.WindowWidth (fun x y -> {Char = ' '; Bg = Console.BackgroundColor ; Fg = Console.ForegroundColor})

let mutable private buffer : Buff [,] =  clearBuff()
let mutable bufX = 0
let mutable bufY = 0
let mutable bufBg = Console.BackgroundColor
let mutable bufFg = Console.ForegroundColor

let hideCursor () =
    Console.CursorVisible <- false

let setBgColor bg =
    Console.BackgroundColor <- bg
    bufBg <- bg

let setFgColor fg =
    Console.ForegroundColor <- fg
    bufFg <- fg

let setColors (bg, fg) =
    setBgColor bg
    setFgColor fg
    
let resetColor () =
    Console.ResetColor()
    bufBg <- Console.BackgroundColor
    bufFg <- Console.ForegroundColor
     
let clear () =
    Console.Clear()
    buffer <- clearBuff()
    
let readKey () =
    Console.ReadKey(false).Key    

let setPosition (x, y) =
    Console.SetCursorPosition(x,y)
    bufX <- x
    bufY <- y

let write (text:string) =
    let len = text.Length-1
    let bufEnd =  len + bufX
    let bufSlice = buffer.[bufY..bufY, bufX..bufEnd]
    for x in 0..len do
        let c = text.[x]
        let b = bufSlice.[0, x]
        let sliceX = bufX + x                    
        if  b.Bg <> bufBg 
                || b.Fg <> bufFg
                || b.Char <> c then
            Console.Write(c)
            buffer.[bufY, sliceX].Char <- c
            buffer.[bufY, sliceX].Bg <- bufBg
            buffer.[bufY, sliceX].Bg <- bufBg
        else
            Console.CursorLeft <- sliceX + 1

let say (x, y) (text:string) width =
    setPosition (x,y)
    let paddedText = text.PadRight(width)
    write paddedText

