module Common
let (|Int|_|) (str:string) =
   let mutable result = 0
   match System.Int32.TryParse(str, &result) with
   | true -> Some(result)
   | _ -> None

