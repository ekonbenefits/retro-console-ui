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


module Program
#load "Console.fsx"
#load "RunLoop.fsx"
#load "Menu.fsx"
#load "Box.fsx"

let colorsApp = Console.Color.DarkBlue, Console.Color.White

let start () =
    Console.setColors colorsApp
    Console.clear()
