module Day5
open NUnit.Framework
open FsUnit

[<Test>]
let ``The following code with positional mode will produce output 1 if input is 8 and otherwise 0``()=
    let array = [|3;9;8;9;10;9;4;9;99;-1;8|]
    Day5.solve_part_2 array 8 |> should equal 1
    Day5.solve_part_2 array 3 |> should not' (equal 1)

[<Test>]
let ``The following code with immediate mode will produce output 1 if input is 8 and otherwise 0``()=
    let array = [|3;3;1108;-1;8;3;4;3;99|]
    Day5.solve_part_2 array 8 |> should equal 1
    Day5.solve_part_2 array 3 |> should not' (equal 1)
