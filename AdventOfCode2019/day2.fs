namespace adventofcode

open System.IO
module Day2=
    let (|Add|Mul|Halt|) n=
        match n with
        | 1 -> Add n
        | 2 -> Mul n
        | 99 -> Halt n

    let get_list file=
        let contents = File.ReadAllText file
        contents.Split ","

    let map_to_numbers = Array.map int

    let add_op (list:int[]) a b idx=(idx, (list.[a]+list.[b]))
    let mul_op (list:int[]) a b idx=(idx,(list.[a]*list.[b]))

    let parse_op (list:int[]) i=
        match list.[i] with
        | Add _ -> Some(add_op list list.[(i+1)] list.[(i+2)] list.[(i+3)])
        | Mul _ -> Some(mul_op list list.[(i+1)] list.[(i+2)] list.[(i+3)])
        | Halt _ -> None

    let map_item index index_to_map (old_val:int) (new_val:int)=
        if index=index_to_map then
            new_val
        else old_val

    let new_list (list:int[]) t=
        let (idx, new_val) = t
        list |>
        Array.mapi (fun i x -> (map_item i idx x new_val))

    let rec step (list:int[]) i=
        let op = parse_op list i
        match op with
        | Some t -> step (new_list list t) (i+4)
        | None -> list

    let rec step_0 list=step list 0

    let swap list=
        list |>
        Array.mapi (fun i x -> match i with
                                | 1 -> 12
                                | 2 -> 2
                                | _ -> x)

    let get_new_list = get_list >> map_to_numbers >> swap >> step_0
    let solve_part_1 file =
        let list = get_new_list file
        list.[0]