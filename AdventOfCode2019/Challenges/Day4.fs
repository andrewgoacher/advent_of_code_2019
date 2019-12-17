module AdventOfCode2019.Challenges.Day4

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

let rec true_double_filter prev current has_double has_more num_list=
    match num_list with
    | h :: rest ->
        match current with
        | None -> true_double_filter None (Some h) false false rest
        | Some c ->
            match prev with
            | None -> true_double_filter current (Some h) false false rest
            | Some p ->
                if p = c then
                    if has_double then (true_double_filter current (Some h) false true rest)
                    elif has_more then (true_double_filter current (Some h) false true rest)
                    else (true_double_filter current (Some h) true false rest)
                else
                    if has_double then true
                    elif has_more then (true_double_filter current (Some h) false false  rest)
                    else (true_double_filter current (Some h) false false  rest)
    | [] ->
       match current with
       | None -> failwith "Shouldn't get here"
       | Some c ->
           match prev with
           | None -> failwith "Shouldn't her here either"
           | Some p -> if has_double then has_double && p<>c else not has_more && p=c

let contains_double num=
    (string num).ToCharArray() |> Array.toList |> (double_filter None None)

let contains_only_double num=
    (string num).ToCharArray() |> Array.toList |> (true_double_filter None None false false)

let solve_part_1 items=
    items
    |> List.map (fun i -> (string i).PadLeft(6).PadRight(6))
    |> List.filter contains_double
    |> List.filter (fun f -> has_no_decreasing_numbers (f.ToCharArray() |> Array.toList) None None)
    |> List.length

let solve_part_2 items=
    items
    |> List.map (fun i -> (string i).PadLeft(6).PadRight(6))
    |> List.filter contains_only_double
    |> List.filter (fun f -> has_no_decreasing_numbers (f.ToCharArray() |> Array.toList) None None)
    |> List.length

let solve min max=
    let range = [min..max]
    let part1 = solve_part_1 range
    printfn "Day 4:"
    if part1 <> 2814 then failwith "\tPart 1 has incorrect answer" else printfn "\tPart 1: %i" part1
    let part2 = solve_part_2 range
    if part2 <> 1991 then failwith "\t Part 2 has incorrect answer" else  printfn "\tPart 2: %i" part2