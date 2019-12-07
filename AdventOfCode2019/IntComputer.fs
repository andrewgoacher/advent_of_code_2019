module IntComputer
open System.IO

type Memory = int[]
type ProgramCounter = int

type Operand=
| Add of lhs:int * rhs:int * addr:int
| Mul of lhs:int * rhs:int * addr:int
| Store of pos:int
| Retrieve of pos:int
| Halt

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
             | 1 -> Add(get_value_mem mode1 mem.[pc+1],get_value_mem mode2 mem.[pc+2],mem.[pc+3]), 4
             | 2 -> Mul(get_value_mem mode1 mem.[pc+1],get_value_mem mode2 mem.[pc+2],mem.[pc+3]), 4
             | 99 -> Halt,0
             | _ -> failwith (sprintf "Unknown token: %i" mem.[pc])
    (op, pc+increment)

let load_memory_from_file file delim=
    let contents = File.ReadAllText file
    contents.Split delim |>
    Array.map int

let add_mem (mem:Memory) lhs rhs i=
    mem.[i] <- lhs + rhs
    mem

let mul_mem (mem:Memory) lhs rhs i=
    mem.[i] <- lhs * rhs
    mem

let rec run (pc:ProgramCounter) (memory:Memory)=
    let (op, counter) = parse_op memory pc
    match op with
    | Add (l,r,i) -> run counter (add_mem memory l r i)
    | Mul (l,r,i) ->  run counter (mul_mem memory l r i)
    | Halt -> memory
    | _ -> failwith "Unknown additional operand"
