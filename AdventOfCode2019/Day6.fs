module Day6

open System.IO

let solve_part_1 origin arr =
    Orbit.createOrbitsFrom arr |> Orbit.countOrbitsFrom "COM"

let solve_part_2 orbits=
    let orbits = Orbit.createOrbitsFrom orbits |> Orbit.getPathFrom "YOU" "SAN"
    (Array.length orbits)-1

let solve file =
    let array = File.ReadAllLines file |> Array.toList
    printfn "Day 6"
    let part1 = (array |> solve_part_1 "COM")
    if part1 <> 621125 then failwith "\tPart 1 doesn't not have required result"
    else printfn "\tPart 1: %i" part1
    let part2 = (array |> solve_part_2)
    if part2 <> 550 then failwith "\tPart 2 doesn't not have required result"
    else printfn "\tPart 2: %i" part2
