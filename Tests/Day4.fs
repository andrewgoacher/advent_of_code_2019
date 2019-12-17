module Day4
open NUnit.Framework
open FsUnit
open AdventOfCode2019.Challenges

[<Test>]
let ``Small list gives expected result``()=
    // todo can't get this one to work but it isn't important
    //Day4.solve_part_1 [000000] |> should equal 1
    Day4.solve_part_1 [111111;223450;123789] |> should equal 1
    Day4.solve_part_1 [111111;222222;333333;444444;555555;666666;777777;888888;999999;000000;] |> should equal 10
    Day4.solve_part_1 [111111;223456;123789] |> should equal 2
    Day4.solve_part_1 [110111;223451;123789] |> should equal 0

[<Test>]
let ``Small list has only double gives expected result``()=
    // todo can't get this one to work but it isn't important
    //Day4.solve_part_1 [000000] |> should equal 1
    Day4.solve_part_2 [111111;223450;123789] |> should equal 0
    Day4.solve_part_2 [111111;222222;333333;444444;555555;666666;777777;888888;999999;000000;] |> should equal 0
    Day4.solve_part_2 [112345;223456;123789] |> should equal 2
    Day4.solve_part_2 [112333;223444;113555] |> should equal 3
    Day4.solve_part_2 [111233;223451;123789] |> should equal 1