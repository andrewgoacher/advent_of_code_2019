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

let rec double_filter prev current num_list=
    match num_list with
    | h :: rest -> 
        match current with
        | None -> double_filter None (Some h) rest
        | Some c ->
            match prev with
            | None -> double_filter current (Some h) rest
            | Some p -> if p = c then true else (double_filter current (Some h) rest)
    | [] ->
       match current with
       | None -> failwith "Shouldn't get here"
       | Some c ->
           match prev with
           | None -> failwith "Shouldn't her here either"
           | Some p -> p = c

let contains_double num=
    (string num).ToCharArray() |> Array.toList |> (double_filter None None)

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
    