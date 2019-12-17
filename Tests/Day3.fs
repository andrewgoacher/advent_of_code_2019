module ``Day 3 - Sample Data``

open NUnit.Framework
open FsUnit
open AdventOfCode2019.Challenges

[<Test>]
let ``for a small sample set, the number of intersections should be correct``() =
    let intersections = Day3.getIntersections "R8,U5,L5,D3" "U7,R6,D4,L4"
    List.length intersections |> should equal 2

[<Test>]
let ``for a small sample set, the distances should be correct``() =
    Day3.solve_part_1 "R8,U5,L5,D3" "U7,R6,D4,L4" |> should equal 6.
    Day3.solve_part_1 "R75,D30,R83,U83,L12,D49,R71,U7,L72" "U62,R66,U55,R34,D71,R55,D58,R83" |> should equal 159.
    Day3.solve_part_1 "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51" "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7"
    |> should equal 135.

[<Test>]
let ``for a small sample set, the smallest distance should win``() =
    Day3.solvePart2 "U7,R6,D4,L4" "R8,U5,L5,D3" |> should equal 30.
    Day3.solvePart2 "R75,D30,R83,U83,L12,D49,R71,U7,L72" "U62,R66,U55,R34,D71,R55,D58,R83" |> should equal 610.
    Day3.solvePart2 "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51" "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7"
    |> should equal 410.
