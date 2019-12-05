module Day3
open Geometry
open System.IO
let private (|Up|Down|Left|Right|) (vec:string)=
    let (dir,amount) = (vec.[0],float vec.[1..])
    match dir with
    | 'U' -> Up amount
    | 'D' -> Down amount
    | 'L' -> Left amount
    | 'R' -> Right amount
    | x -> failwith (sprintf "What is this shit? %c" x)

let private parse_line (acc:Line list * Point) (item:string)=
    let (arr, last) = acc
    let new_line = match item with
                    | Up u -> {a=last;b=(add_to_point last 0. u)}
                    | Down d -> {a=last;b=(add_to_point last 0. -d)}
                    | Left l -> {a=last;b=(add_to_point last -l 0.)}
                    | Right r -> {a=last;b=(add_to_point last r 0.)}
    (new_line :: arr, new_line.b)

let private get_lines (str:string)=
    let (list,_) = str.Split "," |>
                   Array.fold parse_line ([], {x=0.;y=0.;})
    list

let private check_intersection (item:Line) (list:Line list)=
    list |>
    List.map (fun i -> get_intersection_point i item) |>
    List.choose id

let fold_intersection (acc:(Point list)) (item:Line) (lines:Line list)=
    let items = check_intersection item lines
    if List.length items = 0 then acc
    else List.append items acc

let get_intersections str1 str2=
    let lines1 = get_lines str1
    let lines2 = get_lines str2

    lines1 |>
    List.fold (fun acc i -> (fold_intersection acc i lines2)) []

let solve_part_1 str1 str2=
    let origin = {x=0.;y=0.;}
    let distances = get_intersections str1 str2 |>
                    List.map (fun i -> abs (get_manhattan_distance i origin)) |>
                    List.sort 
    distances.[0]

let solve file=
    let data = File.ReadAllLines file
    printfn "Day 3\n"
    let part1 = solve_part_1 data.[0] data.[1]
    printfn "\t Part 1: %f" part1

    