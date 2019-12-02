namespace adventofcode

open System.IO
module Day1=

    let get_lines = File.ReadAllLines
    let map_to_numbers = Array.map float
    let fuel_for_mass mass= (floor (mass/3.))-2.
    let get_total_fuel = Array.map fuel_for_mass
    let is_positive n = n > 0.
    let rec fuel_for_mass_with_fuel mass=
        let fuel = fuel_for_mass mass
        if is_positive (fuel_for_mass mass) then
            mass + fuel_for_mass_with_fuel fuel
        else mass
    let get_total_adjusted_fuel = Array.map fuel_for_mass >> Array.map fuel_for_mass_with_fuel
    let solve_part_1 = get_lines >> map_to_numbers >> get_total_fuel >> Array.sum >> int
    let solve_part_2 = get_lines >> map_to_numbers >> get_total_adjusted_fuel >> Array.sum >> int
