module AdventOfCode2019.Common
let (|Int|_|) (str:string) =
   let mutable result = 0
   match System.Int32.TryParse(str, &result) with
   | true -> Some(result)
   | _ -> None

let perf fn=
    let watch  = System.Diagnostics.Stopwatch.StartNew()
    let result = fn()
    let elapsed = watch.ElapsedMilliseconds
    (result,elapsed)

