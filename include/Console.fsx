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


let mutable bufCol = 0
let mutable bufRow = 0
let mutable bufBg = Console.BackgroundColor
let mutable bufFg = Console.ForegroundColor

let clearBuff () = Array2D.init Console.WindowHeight Console.WindowWidth (fun r c -> {Char = ' '; Bg = bufBg ; Fg = bufFg})

let mutable private buffer : Buff [,] =  clearBuff()

let private output = new StreamWriter(Console.OpenStandardOutput(),Console.OutputEncoding,256, true)

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

let setPosition (row, col) =
    Console.SetCursorPosition(col,row)
    bufCol <- col
    bufRow <- row

let write (text:string) =
    let len = text.Length-1
    let bufEnd =  len + bufCol
    let bufSlice = buffer.[bufRow..bufRow, bufCol..bufEnd]
    for col in 0..len do
        let c = text.[col]
        let b = bufSlice.[0, col]
        let sliceCol = bufCol + col
        if  b.Bg <> bufBg
                || b.Fg <> bufFg
                || b.Char <> c then
            output.Write(c)
            buffer.[bufRow, sliceCol].Char <- c
            buffer.[bufRow, sliceCol].Bg <- bufBg
            buffer.[bufRow, sliceCol].Bg <- bufBg
        else
            output.Flush()
            Console.CursorLeft <- sliceCol + 1
    output.Flush()

let say (row, col) (text:string) width =
    setPosition (row, col)
    let paddedText = text.PadRight(width)
    write paddedText

let ready () =
    Console.KeyAvailable
