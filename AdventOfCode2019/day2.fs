namespace adventofcode

open System.IO
module Day2=
    let (|Add|Mul|Halt|) n=
        match n with
        | 1 -> Add
        | 2 -> Mul
        | 99 -> Halt
        | _ -> failwith "Unexpected token"

    let get_list file=
        let contents = File.ReadAllText file
        contents.Split ","

    let map_to_numbers = Array.map int

    let add_op (list:int[]) a b idx=(idx, (list.[a]+list.[b]))
    let mul_op (list:int[]) a b idx=(idx,(list.[a]*list.[b]))

    let parse_op (list:int[]) i=
        match list.[i] with
        | Add -> Some(add_op list list.[(i+1)] list.[(i+2)] list.[(i+3)])
        | Mul -> Some(mul_op list list.[(i+1)] list.[(i+2)] list.[(i+3)])
        | Halt -> None

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

    let swap_core (a:int) (b:int) (list:int[])=
        list.[1] <- a
        list.[2] <- b
        list

    let swap = swap_core 12 2

    let get_new_list file swap_fn=
        get_list file |>
        map_to_numbers |>
        swap_fn |>
        step_0

    let solve_part_1 file =
        let list = get_new_list file swap
        list.[0]

    let rec solve_for_noun file n v0=
        let rec solve_for_verb v1=
            let list = get_new_list file (swap_core n v1)
            if list.[0]=19690720 then Some (n,v1)
            else if v1>99 then None else solve_for_verb (v1+1)
        match solve_for_verb v0 with
        | Some a -> Some (a)
        | None -> if n >99 then None else solve_for_noun file (n+1) v0

    let solve_part_2 file=
        match solve_for_noun file 1 1 with
        | Some (n,v) -> (n*100)+v
        | None -> failwith "Unknown shit happened"
            