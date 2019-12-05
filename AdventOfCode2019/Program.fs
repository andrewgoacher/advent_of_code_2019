// Learn more about F# at http://fsharp.org

open System
open adventofcode

[<EntryPoint>]
let main argv =
    // todo: Add some unit testing to make sure I don't break previous ones
    Day1.solve "data/day_1.txt" 
    //printfn "Day 2: part 1: %i" (Day2.solve_part_1 "data/day_2.txt") // 3931283
    //printfn "Day 2: part 2: %i" (Day2.solve_part_2 "data/day_2.txt") // 6979
    Day3.solve "data/day_3.txt"
    
    0 // return an integer exit code
