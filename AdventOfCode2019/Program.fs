// Learn more about F# at http://fsharp.org

open System
open adventofcode

[<EntryPoint>]
let main argv =
    // todo: Add some unit testing to make sure I don't break previous ones
    printfn "Day 1: part 1: %i" (Day1.solve_part_1 "data/day_1.txt") // 3282935 
    printfn "Day 1: part 2: %i" (Day1.solve_part_2 "data/day_1.txt") // 4921542
    printfn "Day 2: part 1: %i" (Day2.solve_part_1 "data/day_2.txt") // 3931283
    0 // return an integer exit code
