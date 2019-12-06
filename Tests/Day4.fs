module Day4
open NUnit.Framework
open FsUnit
open Day4

[<Test>]
let ``Small list gives expected result``()=
    Day4.solve_part_1 [000000] |> should equal 1
    Day4.solve_part_1 [111111;223450;123789] |> should equal 1
    Day4.solve_part_1 [111111;222222;333333;444444;555555;666666;777777;888888;999999;000000;] |> should equal 10
    Day4.solve_part_1 [111111;223456;123789] |> should equal 2
    Day4.solve_part_1 [110111;223451;123789] |> should equal 0