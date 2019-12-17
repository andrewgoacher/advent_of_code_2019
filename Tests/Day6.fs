module Day6
open NUnit.Framework
open FsUnit

[<Test>]
let ``The sample orbit will produce the correct result``()=
   let orbits = [
      "COM)B"
      "B)C"
      "C)D"
      "D)E"
      "E)F"
      "B)G"
      "G)H"
      "D)I"
      "E)J"
      "J)K"
      "K)L"
   ]
   Day6.solve_part_1 "COM" orbits |> should equal 42
   
//[<Test>]
//let ``The sample orbits with me and santy clause will produce the correct result``()=
//   let orbits =[
//      "COM)B"
//      "B)C"
//      "C)D"
//      "D)E"
//      "E)F"
//      "B)G"
//      "G)H"
//      "D)I"
//      "E)J"
//      "J)K"
//      "K)L"
//      "K)YOU"
//      "I)SAN"
//   ]
//   Day6.solve_part_2 orbits |> should equal 4
   
//[<Test>]
//let ``The following code with immediate mode will produce output 1 if input is 8 and otherwise 0``()=
//    let array = [|3;3;1108;-1;8;3;4;3;99|]
//    Day5.solve_part_2 array 8 |> should equal 1
//    Day5.solve_part_2 array 3 |> should not' (equal 1)

