// Learn more about F# at http://fsharp.org

open System
open adventofcode
open Common

[<EntryPoint>]
let main argv =
    printfn "Enter number for day:"
    let result = Console.ReadLine()

    let days = [
         (fun () -> (Day1.solve "data/day_1.txt"))
         (fun () -> (Day2.solve "data/day_2.txt"))
         (fun () -> (Day3.solve "data/day_3.txt"))
         (fun () -> (Day4.solve 109165 576723))
    ]

    let parsed_result = match result with
                        | Int i -> i
                        | _ -> -1
        

    match parsed_result with
    | -1 -> days |> List.map (fun f -> f()) |> ignore
    | 0 -> failwith "We don't count from programmer 1 here!"
    | x when x <= List.length days ->
        days.[x-1]()
    | _ -> printfn "Unknown option"  
    0 // return an integer exit code
