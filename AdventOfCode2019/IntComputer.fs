module IntComputer
open System.IO

type Memory = int[]
type ProgramCounter = int

type Operand=
| Add of lhs:int * rhs:int * addr:int
| Mul of lhs:int * rhs:int * addr:int
| Halt

let parse_op (mem:Memory) (pc:ProgramCounter)=
    let op = match mem.[pc] with
             | 1 -> Add(mem.[pc+1],mem.[pc+2],mem.[pc+3])
             | 2 -> Mul(mem.[pc+1],mem.[pc+2],mem.[pc+3])
             | 99 -> Halt
             | _ -> failwith (sprintf "Unknown token: %i" mem.[pc])
    (op, pc+4)

let load_memory_from_file file delim=
    let contents = File.ReadAllText file
    contents.Split delim |>
    Array.map int

let add_mem (mem:Memory) lhs rhs i=
    mem.[i] <- mem.[lhs] + mem.[rhs]
    mem

let mul_mem (mem:Memory) lhs rhs i=
    mem.[i] <- mem.[lhs] * mem.[rhs]
    mem

let rec run (pc:ProgramCounter) (memory:Memory)=
    let (op, counter) = parse_op memory pc
    match op with
    | Add (l,r,i) -> run counter (add_mem memory l r i)
    | Mul (l,r,i) ->  run counter (mul_mem memory l r i)
    | Halt -> memory
