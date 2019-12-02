namespace adventofcode
open System.IO
module Day1=
    let private fuel_for_mass mass= (floor (mass/3.))-2.
    let rec private  total_fuel_for_mass mass=
        let fuel=fuel_for_mass mass
        if fuel>0. then mass + total_fuel_for_mass fuel else mass
    let private accumulate_mass acc mass=acc+fuel_for_mass (float mass)
    let private accumulate_all acc mass=acc + total_fuel_for_mass (accumulate_mass 0. mass)
    let private solve file fn=file|>File.ReadAllLines|>Array.fold fn 0. |> int
    let solve_part_1 file=solve file accumulate_mass
    let solve_part_2 file=solve file accumulate_all