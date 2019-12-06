module Day4

open System.Text.RegularExpressions

let rec has_no_decreasing_numbers num_list prev current=
    match num_list with
    | h :: rest -> 
        match current with
        | None -> has_no_decreasing_numbers rest None (Some h)
        | Some c ->
            match prev with
            | None -> has_no_decreasing_numbers rest current (Some h)
            | Some p -> if c < p then false else (has_no_decreasing_numbers rest current (Some h))
    | [] -> match current with
            | None -> false
            | Some c ->
                match prev with
                | None -> false
                | Some p -> c >= p

let (|Match|_|) pattern input =
    let m = Regex.Match(input, pattern) in
    if m.Success then Some(true) else None

let contains_double value = 
    match value with
        | Match "11+|22+|33+|44+|55+|66+|77+|88+|99+|00+" _ -> true
        | _ -> false

let filter_doubles (num)=
    num |> contains_double

let solve_part_1 items=
    items
    |> List.map (fun i -> (string i).PadLeft(6).PadRight(6))
    |> List.filter contains_double 
    |> List.filter (fun f -> has_no_decreasing_numbers (f.ToCharArray() |> Array.toList) None None)
    |> List.length

let solve min max=
    let range = [min..max]
    let part1 = solve_part_1 range
    printf "Day 4:\n"
    if part1 <> 2814 then failwith "\tPart 1 has incorrect answer" else printf "\tPart 1: %i" part1
    