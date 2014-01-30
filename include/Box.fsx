module Box

#load "RunLoop.fsx"
#load "Console.fsx"

type private promptItem = { position:(int * int); text:string; update:(string->unit) option; defaultValue:string option}
let private sayItem:promptItem = { position = (0,0); text =""; update = None; defaultValue=None;}

let private topBox = "╔═╗"
let private midBox = "║ ║"
let private botBox = "╚═╝"

let private boxColors = Console.Color.DarkMagenta, Console.Color.White

type DisplayBox () =
    let mutable items: promptItem list = List.empty
    member this.Say (x, y) text =
        items <- { sayItem with position=(x,y); text=text; } :: items
    member this.Prompt (x, y) text update defaultValue =
        items <- { position=(x,y); text=text; update=Some(update); defaultValue=Some(defaultValue) } :: items
    member this.Display() = 
        let max = items |> List.map (fun x-> x.text.Length) |> List.max
        Console.setColors boxColors
        let rowStart =6
        let colStart =5
        Console.say (colStart,  rowStart) (topBox.[0].ToString()) 1
        Console.say (colStart + 1, rowStart) (String.replicate (max + 2) (topBox.[1].ToString())) 1
        Console.say (colStart + max + 3,  rowStart) (topBox.[2].ToString()) 1
        for i, item in items |> List.rev |> List.mapi (fun i x -> i+1,x)  do
            Console.say (colStart,  rowStart+i) (midBox.[0].ToString()) 1
            Console.say (colStart+1,  rowStart+i) " " 1
            Console.say (colStart+2,  rowStart+i) item.text (max + 2)
            Console.say (colStart+2+max,  rowStart+i) " " 1
            Console.say (colStart+3+max,  rowStart+i) (midBox.[2].ToString()) 1
            
            
            
            

let start () = DisplayBox()
