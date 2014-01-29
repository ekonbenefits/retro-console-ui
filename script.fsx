#load "include/program.fsx"

Program.start()
let result = Menu.show "Test" ["One"; "Two"; "Three"]

let box = Box.start ()
box.Say (4,5) "************* TEST ************"
box.Display()