module Day3

open NUnit.Framework
open FsUnit
open Day3

[<Test>]
let ``for a small sample set, the number of intersections should be correct``()=
    let intersections = get_intersections "R8,U5,L5,D3" "U7,R6,D4,L4"
    List.length intersections |> should equal 2

[<Test>]
let ``for a small sample set, the distances should be correct``()=
    solve_part_1 "R8,U5,L5,D3" "U7,R6,D4,L4" |> should equal 6.
    solve_part_1 "R75,D30,R83,U83,L12,D49,R71,U7,L72" "U62,R66,U55,R34,D71,R55,D58,R83" |> should equal 159.
    solve_part_1 "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51" "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7" |> should equal 135.
