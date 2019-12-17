module Orbit

let private spltData (data: string) =
    let split = data.Split(")")
    (split.[0]), split.[1]

let private createOrbits (data: string list) =
    data |> List.map spltData

type OrbitNode =
    { Name: string
      Parent: string option
      Children: string [] }

type OrbitNodes = OrbitNode []

let tryFindNode key nodes =
    let potential = nodes |> Array.tryFindIndex (fun i -> i.Name = key)
    match potential with
    | None -> None
    | Some i -> Some nodes.[i]
    | _ -> failwith "Should not contain more than one potential"

let tryUpdateNode node nodes =
    let potential = nodes |> Array.tryFindIndex (fun i -> i.Name = node.Name)
    match potential with
    | None -> Array.append [| node |] nodes
    | Some i ->
        nodes.[i] <- node
        nodes

let createNode key value parent =
    { Name = key
      Parent = parent
      Children = value }
let private addNode (nodes: OrbitNodes) (data: string * string): OrbitNodes =
    let (key, value) = data

    let parent =
        match tryFindNode key nodes with
        | None -> createNode key [||] None
        | Some node -> node

    let child =
        match tryFindNode value nodes with
        | None -> createNode value [||] (Some parent.Name)
        | Some n ->
            match n.Parent with
            | None ->
                {n with Parent = Some parent.Name}
            | Some p ->
                failwithf "Cannot create a child from %s -> %s\n%A\n%A" key value p n

    let parent = { parent with Children = Array.append [| child.Name |] parent.Children }
    tryUpdateNode parent nodes |> tryUpdateNode child

let private collect orbits =
    orbits |> List.fold addNode [||]

let createOrbitsFrom (data: string list) =
    (createOrbits data) |> collect

let rec countOrbits total orbits node data =
    match node.Children with
    | [||] -> total
    | children ->
        let orbits = orbits + 1
        let children = children |> Array.map (fun a -> tryFindNode a data) |> Array.choose id
        match Array.length children with
        | 0 -> total
        | _ -> total +  (Array.map (fun a -> countOrbits orbits orbits a data) children |> Array.sum)

let countOrbitsFrom key data =
    match tryFindNode key data with
    | None -> 0
    | Some o -> countOrbits 0 0 o data