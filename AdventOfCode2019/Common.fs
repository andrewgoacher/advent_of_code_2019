module AdventOfCode2019.Common
let (|Int|_|) (str:string) =
   let mutable result = 0
   match System.Int32.TryParse(str, &result) with
   | true -> Some(result)
   | _ -> None

let wrapPerf fn=
    let watch  = System.Diagnostics.Stopwatch.StartNew()
    let result = fn()
    let elapsed = watch.ElapsedMilliseconds
    (result,elapsed)

let solve day part1 part2=
    printfn "Day %d" day
    let (part1, expected1) = part1
    let result = part1()
    if result <> expected1 then failwithf "\tIncorrect part 1\n\t\texpected %A actual %A" expected1 result else printfn "\tPart 1: %A" result
    let (part2, expected2) = part2
    let result = part2()
    if result <> expected2 then failwithf "\tIncorrect part 2\n\t\texpected %A actual %A" expected2 result else printfn "\tPart 2: %A" result
    
let resolveRoundingError f=
    let epsilon = 1e-10
    abs f < epsilon