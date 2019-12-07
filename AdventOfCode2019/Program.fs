// Learn more about F# at http://fsharp.org

open System
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
         (fun () -> (Day5.solve "data/day_5.txt"))
    ]

    let parsed_result = match result with
                        | Int i -> i
                        | _ -> -1
        

    match parsed_result with
    | -1 -> days |> 
            List.mapi (fun i f -> 
                let (_,time) = perf f
                printfn "Time for Day %i is %ims\n" (i+1) time) |>
            ignore
    | 0 -> failwith "We don't count from programmer 1 here!"
    | x when x <= List.length days ->
        let (_,time) = perf days.[x-1]
        printfn "Time for Day %i is %ims\n" (x) time
    | _ -> printfn "Unknown option"  
    0 // return an integer exit code
