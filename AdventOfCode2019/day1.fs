namespace adventofcode

open System.IO
module Day1=

    let get_lines file=
        File.ReadAllLines file

    let map_to_numbers (lines:string[]) =
        lines |>
        Array.map float

    let fuel_for_mass mass=
        let div = mass / 3.
        let rounded = floor div
        rounded - 2.

    let get_total_fuel lines=
        lines |>
        Array.map fuel_for_mass

    let solve_part_1 = get_lines >> map_to_numbers >> get_total_fuel >> Array.sum >> int

    let (|Positive|Negative|) n = if n > 0. then Positive else Negative

    let is_positive n =
        match n with
        | Positive -> true
        | Negative -> false

    let rec fuel_for_mass_with_fuel mass=
        let fuel = fuel_for_mass mass
        match fuel with
        | Positive -> mass + fuel_for_mass_with_fuel fuel
        | Negative -> mass

    let get_total_adjusted_fuel lines=
        lines |>
        Array.map fuel_for_mass |>
        Array.map fuel_for_mass_with_fuel

    let solve_part_2 = get_lines >> map_to_numbers >> get_total_adjusted_fuel >> Array.sum >> int
