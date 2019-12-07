module Day5
open IntComputer

let private solve_part_1 arr=
    let memory,outputs = Array.copy arr |> run 0 1
    outputs.[0]

let solve file=
    let orig_memory = (load_memory_from_file file [|','|])
    printfn "Day 5";
    let part1 = (orig_memory |> solve_part_1)
    printfn "\tPart 2: %i" part1
    //printfn "\tPart 1: %i" part1
    //if part1 <> 3931283 then failwith "Incorrect value for part 1!"
    //let part2 = (orig_memory |> solve_part_2)
    //printfn "\tPart 2: %i" part2
    //if part2 <> 6979 then failwith "Incorrect value for part 2!"