module AdventOfCode2019.Challenges.Day3

open AdventOfCode2019
open AdventOfCode2019.Geometry
open System.IO

let private (|Up|Down|Left|Right|) (vec: string) =
    let (dir, amount) = (vec.[0], float vec.[1..])
    match dir with
    | 'U' -> Up amount
    | 'D' -> Down amount
    | 'L' -> Left amount
    | 'R' -> Right amount
    | x -> failwith (sprintf "What is this shit? %c" x)

let private parseLine (acc: Line list * Point) (item: string) =
    let (arr, last) = acc

    let new_line =
        match item with
        | Up u ->
            { a = last
              b = (Point.add last 0. u)
              dir = item }
        | Down d ->
            { a = last
              b = (Point.add last 0. -d)
              dir = item }
        | Left l ->
            { a = last
              b = (Point.add last -l 0.)
              dir = item }
        | Right r ->
            { a = last
              b = (Point.add last r 0.)
              dir = item }

    (List.append arr [ new_line ], new_line.b)

let private get_lines (str: string) =
    let (list, _) =
        str.Split ","
        |> Array.fold parseLine
               ([],
                { x = 0.
                  y = 0. })
    list

type Intersection =
    { linea: Line
      lineb: Line
      point: Point }

let private optionalIntersection linea lineb =
    let intersection = Line.getIntersectionPoint linea lineb
    match intersection with
    | None -> None
    | Some i ->
        Some
            { linea = linea
              lineb = lineb
              point = i }

let private checkIntersection (item: Line) (list: Line list) =
    list
    |> List.map (fun i -> optionalIntersection item i)
    |> List.choose id

let private foldIntersection (acc: Intersection list) (item: Line) (lines: Line list) =
    let items = checkIntersection item lines
    if List.length items = 0 then acc
    else items |> List.append acc

let getIntersections str1 str2 =
    let lines1 = get_lines str1
    let lines2 = get_lines str2

    lines1 |> List.fold (fun acc i -> (foldIntersection acc i lines2)) []

let solve_part_1 str1 str2 =
    let origin =
        { x = 0.
          y = 0. }

    let distances =
        getIntersections str1 str2
        |> List.map (fun i -> abs (Point.getManhattanDistance i.point origin))
        |> List.sort

    int distances.[0]

let rec private walkLines lines line point distance =
    match lines with
    | h :: rest ->
        match line = h with
        | true ->
            let new_b =
                { a = h.a
                  b = point
                  dir = "Unknown" }

            let new_distance = (distance + (Line.unitLength new_b))
            new_distance
        | false ->
            let new_distance = (distance + (Line.unitLength h))
            walkLines rest line point new_distance
    | [] ->
        distance

let intersectionPath linesa linesb intersection =
    (walkLines linesa intersection.linea intersection.point 0.)
    + (walkLines linesb intersection.lineb intersection.point 0.)

let solvePart2 str1 str2 =
    let lines1 = get_lines str1
    let lines2 = get_lines str2

    let intersections = lines1 |> List.fold (fun acc i -> (foldIntersection acc i lines2)) []

    let distances =
        intersections
        |> List.map (intersectionPath lines1 lines2)
        |> List.sort
    int distances.[0]

let solve file =
    let data = File.ReadAllLines file
    let part1 = (fun () -> solve_part_1 data.[0] data.[1])
    let part2 = (fun () -> solvePart2 data.[0] data.[1])
    Common.solve 3 (part1, 403) (part2, 4158)
