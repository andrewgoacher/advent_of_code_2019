module AdventOfCode2019.Orbit
let private spltData (data: string) =
    let split = data.Split(")")
    (split.[0]), split.[1]

let private createOrbits (data: string list) =
    data |> List.map spltData



let private tryFindNode key nodes =
    let potential = nodes |> Array.tryFindIndex (fun i -> i.Name = key)
    match potential with
    | None -> None
    | Some i -> Some nodes.[i]
    | _ -> failwith "Should not contain more than one potential"

let private tryUpdateNode node nodes =
    let potential = nodes |> Array.tryFindIndex (fun i -> i.Name = node.Name)
    match potential with
    | None -> Array.append [| node |] nodes
    | Some i ->
        nodes.[i] <- node
        nodes

let private createNode key value parent =
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

let rec private countOrbits total orbits node data =
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
    
let rec private getPathTo nodeA nodeB data path visited=
    match visited |> Array.tryFind (fun a -> a = nodeA) with
    | Some _ -> None
    | None ->
        let visited = Array.append visited [|nodeA|]
        match tryFindNode nodeA data with
        | None -> None
        | Some n ->
            let path = Array.append path [|n.Name|]
            if n.Name = nodeB then
                Some path
            else
                let maps = match n.Children with
                            | [||] -> [||]
                            | children -> children |> Array.map (fun c -> getPathTo c nodeB data path visited)
                let maps = match n.Parent with
                           | None -> maps
                           | Some p -> maps |> Array.append [|getPathTo p nodeB data path visited|]
                let maps = maps |> Array.choose id
                match Array.length maps with
                | 0 -> None
                | 1 -> Some maps.[0]
                | _ -> failwith "Shouldn't get here"
               
//                |> Array.choose id
//                |> Seq.collect
//                |> Array.ofSeq
    
let getPathFrom nodeA nodeB data=
    match tryFindNode nodeA data with
    | None -> failwith "Unidentified Node Name"
    | Some n ->
        let completedPaths = (match n.Parent with
                            | None -> failwith "Not in a good way here"
                            | Some p -> getPathTo p nodeB data [|n.Name|] [|n.Name|])
        match completedPaths with
        | Some o ->
            let len = Array.length o
            let len = len-2
            let slice = o.[1..len]
            slice
        | None -> failwithf "Should not have any value here other than 1\n%A" completedPaths