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
                    | Up u -> {a=last;b=(add_to_point last 0. u);dir=item}
                    | Down d -> {a=last;b=(add_to_point last 0. -d);dir=item}
                    | Left l -> {a=last;b=(add_to_point last -l 0.);dir=item}
                    | Right r -> {a=last;b=(add_to_point last r 0.);dir=item}

    (List.append arr [new_line], new_line.b)

let private get_lines (str:string)=
    let (list,_) = str.Split "," |>
                   Array.fold parse_line ([], {x=0.;y=0.;})
    list

type Intersection = {linea:Line;lineb:Line;point:Point}

let private optional_intersection linea lineb=
    let intersection = get_intersection_point linea lineb
    match intersection with
    | None -> None
    | Some i -> 
        Some {linea=linea;lineb=lineb;point=i}

let private check_intersection (item:Line) (list:Line list)=
    list |>
    List.map (fun i -> optional_intersection item i) |>
    List.choose id

let private fold_intersection (acc:(Intersection list)) (item:Line) (lines:Line list)=
    let items = check_intersection item lines
    if List.length items = 0 then acc
    else
        items |>
        List.append acc

let get_intersections str1 str2=
    let lines1 = get_lines str1
    let lines2 = get_lines str2

    lines1 |>
    List.fold (fun acc i -> (fold_intersection acc i lines2)) []

let solve_part_1 str1 str2=
    let origin = {x=0.;y=0.;}
    let distances = get_intersections str1 str2 |>
                    List.map (fun i -> abs (get_manhattan_distance i.point origin)) |>
                    List.sort
    distances.[0]

let rec private walk_lines lines line point distance=
    match lines with
    | h :: rest -> 
        match line = h with
        | true -> 
            let new_b = {a=h.a;b=point;dir="Unknown"}
            let new_distance = (distance + (unit_length new_b))
            new_distance
        | false -> 
            let new_distance = (distance + (unit_length h))
            walk_lines rest line point new_distance
    | [] -> 
        distance

let intersection_path linesa linesb intersection=
    (walk_lines linesa intersection.linea intersection.point 0.)+ (walk_lines linesb intersection.lineb intersection.point 0.)

let solve_part_2 str1 str2=
    let lines1 = get_lines str1
    let lines2 = get_lines str2

    let intersections = lines1 |>
                        List.fold (fun acc i -> (fold_intersection acc i lines2)) []
    let distances = intersections |> List.map (intersection_path lines1 lines2) |> List.sort
    distances.[0]

let solve file=
    let data = File.ReadAllLines file
    printfn "Day 3\n"
    let part1 = solve_part_1 data.[0] data.[1]
    printfn "\t Part 1: %f" part1
    let part2 = solve_part_2 data.[0] data.[1]
    printfn "\t Part 2: %f" part2
