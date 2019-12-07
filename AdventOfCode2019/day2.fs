module Day2
open IntComputer

let private swap (a:int) (b:int) (memory:Memory)=
    memory.[1] <- a
    memory.[2] <- b
    memory

let solve_part_1 file =
    let memory = (load_memory_from_file file [|','|]) |> swap 12 2 |> run 0
    memory.[0]

let rec private solve_for_noun file n v0=
    let rec solve_for_verb v1=
        let memory = (load_memory_from_file file [|','|]) |> swap n v1 |> run 0
        if memory.[0]=19690720 then Some (n,v1)
        else if v1>99 then None else solve_for_verb (v1+1)
    match solve_for_verb v0 with
    | Some a -> Some (a)
    | None -> if n >99 then None else solve_for_noun file (n+1) v0

let solve_part_2 file=
    match solve_for_noun file 1 1 with
    | Some (n,v) -> (n*100)+v
    | None -> failwith "Unknown shit happened"

let solve file=
    printfn "Day 2";
    let part1 = (file |> solve_part_1)
    printfn "\tPart 1: %i" part1
    if part1 <> 3931283 then failwith "Incorrect value for part 1!"
    let part2 = (file |> solve_part_2)
    printfn "\tPart 2: %i" part2
    if part2 <> 6979 then failwith "Incorrect value for part 2!"