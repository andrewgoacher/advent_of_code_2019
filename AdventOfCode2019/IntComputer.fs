module AdventOfCode2019.IntComputer
open System.IO
let private split_operands (num:int)=
    let u = num%10
    let t = ((num/10)%10)*10
    let h = (num/100)%10
    let th = (num/1000)%10
    let tth = (num/10000)%10
    (u+t,h,th,tth)

let get_value (mem:Memory) mode data=
    match mode with 
    | 0 -> mem.[data]
    | 1 -> data
    | _ -> failwith (sprintf "Unknown mode: %i" mode)

let parse_op (mem:Memory) (pc:ProgramCounter)=
    let (op, mode1, mode2, mode3) = split_operands mem.[pc]
    let get_value_mem = get_value mem
    let (op,increment) = match op with
             | 1 -> Add(get_value_mem mode1 mem.[pc+1],get_value_mem mode2 mem.[pc+2],get_value_mem mode3 (pc+3)), 4
             | 2 -> Mul(get_value_mem mode1 mem.[pc+1],get_value_mem mode2 mem.[pc+2],get_value_mem mode3 (pc+3)), 4
             | 3 -> Store((*get_value_mem mode1 *)mem.[pc+1]), 2 
             | 4 -> Retrieve((*get_value_mem mode1 *) mem.[pc+1]),2
             | 5 -> JumpIfTrue(get_value_mem mode1 mem.[pc+1], get_value_mem mode2 mem.[pc+2]), 3
             | 6 -> JumpIfFalse(get_value_mem mode1 mem.[pc+1], get_value_mem mode2 mem.[pc+2]),3
             | 7 -> LessThan(get_value_mem mode1 mem.[pc+1], get_value_mem mode2 mem.[pc+2], mem.[pc+3]), 4
             | 8 -> Equals(get_value_mem mode1 mem.[pc+1], get_value_mem mode2 mem.[pc+2], mem.[pc+3]), 4
             | 99 -> Halt,0
             | _ -> failwith (sprintf "Unknown token: %i from %i" op mem.[pc])
    (op, pc+increment)

let loadMemoryFromFile file delim=
    let contents = File.ReadAllText file
    contents.Split delim |>
    Array.map int

let add_mem (mem:Memory) lhs rhs i=
    mem.[i] <- lhs + rhs
    mem

let mul_mem (mem:Memory) lhs rhs i=
    mem.[i] <- lhs * rhs
    mem

let rec private run_internal (pc:ProgramCounter) (input:int) (outputs:int list) (memory:Memory)=
    let (op, counter) = parse_op memory pc
    let output = outputs
    match op with
    | Add (l,r,i) -> run_internal counter input output (add_mem memory l r i) 
    | Mul (l,r,i) ->  run_internal counter input output (mul_mem memory l r i) 
    | Store addr ->
        memory.[addr] <- input
        run_internal counter input output memory
    | Retrieve addr ->
        run_internal counter input (memory.[addr] :: output) memory
    | JumpIfTrue (condition, addr) ->
        let c = if condition > 0 then addr else counter
        run_internal c input output memory
    | JumpIfFalse (condition, addr) ->
        let c = if condition = 0 then addr else counter
        run_internal c input output memory
    | LessThan (lhs,rhs,addr) -> 
        let v = if lhs < rhs then 1 else 0
        memory.[addr] <- v
        run_internal counter input output memory
    | Equals (lhs,rhs,addr) ->
        let v = if lhs = rhs then 1 else 0
        memory.[addr] <- v
        run_internal counter input output memory
    | Halt -> (memory, output)
    | _ -> failwith "Unknown additional operand"

let run (pc:ProgramCounter) (input:int) (memory:Memory)=
    run_internal pc input [] memory
