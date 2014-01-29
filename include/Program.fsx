module Program
#load "Console.fsx"
#load "RunLoop.fsx"
#load "Menu.fsx"

let colorsApp = Console.Color.Blue, Console.Color.White

let start () =
    Console.setColors colorsApp
    Console.clear()