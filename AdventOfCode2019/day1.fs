namespace adventofcode
open System.IO
module Day1=
    let fuel_for_mass mass= (floor (mass/3.))-2.
    let rec total_fuel_for_mass mass=
        let fuel=fuel_for_mass mass
        if fuel>0. then mass + total_fuel_for_mass fuel else mass
    let private accumulate_mass acc (mass:float)=acc+fuel_for_mass (float mass)
    let private accumulate_all acc (mass:float)=acc + total_fuel_for_mass (accumulate_mass 0. mass)
    let private collect data fn =data |> Array.fold fn 0. |> int

    let solve_part_1 (data:float[])=collect data accumulate_mass
    let solve_part_2 (data:float[])=collect data accumulate_all

    let solve file=
        printfn "Day 1";
        let part1 = (file |> File.ReadAllLines |> Array.map float |> solve_part_1)
        let part2 = (file |> File.ReadAllLines |> Array.map float |> solve_part_2)
        printfn "\tPart 1: %i" part1
        if part1 <> 3282935 then failwith "Incorrect value for part 1!"
        printfn "\tPart 2: %i" part2
        if part2 <> 4921542 then failwith "Incorrect value for part 2!"