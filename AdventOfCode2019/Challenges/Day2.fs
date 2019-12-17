module AdventOfCode2019.Challenges.Day2

open AdventOfCode2019.Types
open AdventOfCode2019

let private swap (a: int) (b: int) (memory: Memory) =
    memory.[1] <- a
    memory.[2] <- b
    memory

let private runOnce memory initialA initialB =
    let memory, _ =
        Array.copy memory
        |> swap initialA initialB
        |> IntComputer.run 0 0
    memory.[0]

let solvePart1 arr = runOnce arr 12 2

let rec private solveForNoun arr n v0 =
    let rec solveForVerb v1 =
        let memory = runOnce arr n v1
        if memory = 19690720 then Some(n, v1)
        else if v1 > 99 then None
        else solveForVerb (v1 + 1)
    match solveForVerb v0 with
    | Some a -> Some(a)
    | None ->
        if n > 99 then None
        else solveForNoun arr (n + 1) v0

let solvePart2 file =
    match solveForNoun file 1 1 with
    | Some(n, v) -> (n * 100) + v
    | None -> failwith "Unknown shit happened"

let solve file =
    let orig_memory = (IntComputer.loadMemoryFromFile file [| ',' |])
    let part1 = (fun () -> orig_memory |> solvePart1)
    let part2 = (fun () -> orig_memory |> solvePart2)
    Common.solve 2 (part1, 3931283) (part2, 6979)
