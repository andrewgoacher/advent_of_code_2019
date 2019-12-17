module ``Day 1 Sample Data``

open NUnit.Framework
open FsUnit
open AdventOfCode2019.Challenges

[<Test>]
let ``Day 1: Part 1 - Single mass calculation``() =
    Day1.fuelForMass 12. |> should equal 2.
    Day1.fuelForMass 100756. |> should equal 33583.

[<Test>]
let ``Day 1: Part 1 - Multiple mass calculation``() =
    Day1.solvePart1 [| 1969.; 100756. |] |> should equal 34237.

[<Test>]
let ``Day 1: Part 2 - Single mass calculation``() =
    Day1.totalFuelForMass 1969. |> should equal 2935.
    Day1.totalFuelForMass 100756. |> should equal 151102.

[<Test>]
let ``Day 1: Part 2 - Multiple mass calculation``() =
    Day1.solvePart2 [| 1969.; 14. |] |> should equal 968.
