[<AutoOpen>]
module AdventOfCode2019.Types

type OrbitNode =
    { Name: string
      Parent: string option
      Children: string [] }

type OrbitNodes = OrbitNode []

type Memory = int []

type ProgramCounter = int

type Operand =
    | Add of lhs: int * rhs: int * addr: int
    | Mul of lhs: int * rhs: int * addr: int
    | Store of pos: int
    | Retrieve of pos: int
    | JumpIfTrue of condition: int * addr: int
    | JumpIfFalse of condition: int * addr: int
    | LessThan of lhs: int * rhs: int * addr: int
    | Equals of lhs: int * rhs: int * addr: int
    | Halt

type Point =
    { x: float
      y: float }

type Line =
    { a: Point
      b: Point
      dir: string }
