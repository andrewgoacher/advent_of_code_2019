module AdventOfCode2019.Challenges.Day1

open AdventOfCode2019
open System.IO

let fuelForMass mass = (floor (mass / 3.)) - 2.

let rec totalFuelForMass mass =
    let fuel = fuelForMass mass
    if fuel > 0.0 then mass + totalFuelForMass fuel
    else mass

let private accumulateMass acc (mass: float) = acc + fuelForMass (float mass)
let private accumulateAll acc (mass: float) = acc + totalFuelForMass (accumulateMass 0. mass)

let private collect data fn =
    data
    |> Array.fold fn 0.
    |> int

let solvePart1 (data: float []) = collect data accumulateMass
let solvePart2 (data: float []) = collect data accumulateAll

let solve file =
    let data =
        file
        |> File.ReadAllLines
        |> Array.map float

    let part1 = (fun () -> data |> solvePart1)
    let part2 = (fun () -> data |> solvePart2)
    Common.solve 1 (part1, 3282935) (part2, 4921542)
