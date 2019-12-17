module Day6
open NUnit.Framework
open FsUnit
open AdventOfCode2019.Challenges

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
   
[<Test>]
let ``The sample orbits with me and santy clause will produce the correct result``()=
   let orbits =[
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
      "K)YOU"
      "I)SAN"
   ]
   Day6.solve_part_2 orbits |> should equal 4
