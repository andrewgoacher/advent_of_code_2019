module AdventOfCode2019.Challenges.Day5
open AdventOfCode2019.IntComputer

let solve_part_1 arr=
    let _,outputs = Array.copy arr |> run 0 1
    outputs.[0]

let solve_part_2 arr input=
    let _,outputs = Array.copy arr |> run 0 input
    outputs.[0]

let solve file=
    let orig_memory = (load_memory_from_file file [|','|])
    printfn "Day 5";
    let part1 = (orig_memory |> solve_part_1)
    if part1 <> 15097178 then failwith "\tPart 1 doesn't not have required result" else printfn "\tPart 1: %i" part1
    let part2 = (orig_memory |> solve_part_2)
    printfn "\tPart 2: %i" (part2 5)