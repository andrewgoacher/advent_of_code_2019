namespace adventofcode

open System.IO
module day1=
    let get_lines file=
        File.ReadAllLines file

    let map_to_numbers (lines:string[]) =
        lines |>
        Array.map float

    let fuel_for_mass mass=
        let div = mass / 3.
        let rounded = floor div
        int (rounded) - 2

    let get_total_fuel lines=
        lines |>
        Array.map fuel_for_mass |>
        Array.sum

    let solve = get_lines >> map_to_numbers >> get_total_fuel
        

