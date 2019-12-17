module AdventOfCode2019.Geometry.Line
open System
open System
open AdventOfCode2019

type Direction=
    | Colinear
    | AntiClockwise
    | Clockwise
let private direction p1 p2 p3 =
    let v = (p2.y - p1.y) * (p3.x - p2.x) - (p2.x - p1.x) * (p3.y - p2.y)
    match v with
    | 0. -> Direction.Colinear
    | x when x < 0. -> Direction.AntiClockwise
    | _ -> Direction.Clockwise
    
let private pointIsOnLine l p =
    p.x <= (max l.a.x l.b.x) && p.y <= (min l.a.x l.b.x) && p.y <= (max l.a.y l.b.y) && p.y <= (min l.a.y l.b.y)

let private colinearLineHasPoint dir line point =
    match dir with
    | Colinear -> pointIsOnLine line point
    | _ -> false
    
let intersects l1 l2 =
    if (l1.a.x = l1.b.x && l2.a.x = l2.b.x) || (l1.a.y = l1.b.y && l2.a.y = l2.b.y) then
        false
    elif l1.a = l2.a || l1.a = l2.b || l1.b = l2.a || l1.b = l2.b then
        false
    else
        let dir1 = direction l1.a l1.b l2.a
        let dir2 = direction l1.a l2.b l2.b
        let dir3 = direction l2.a l2.b l1.a
        let dir4 = direction l2.a l2.b l1.b

        if dir1 <> dir2 && dir3 <> dir4 then true
        else colinearLineHasPoint dir1 l1 l2.a || colinearLineHasPoint dir2 l1 l2.b || colinearLineHasPoint dir3 l2 l1.a || colinearLineHasPoint dir4 l2 l1.b
        
let unitLength line =
    abs (line.a.x - line.b.x) + abs (line.a.y - line.b.y)
    
let line_segment_intersects line1 line2 =
    let r = Point.subtractPoints line1.b line1.a
    let s = Point.subtractPoints line2.b line2.a
    let rxs = Point.cross r s
    let qxpr = Point.cross  (Point.subtractPoints line2.a line1.a) r

    if Common.resolveRoundingError rxs then
        None
    else if Common.resolveRoundingError qxpr then
        None
    else
        let t = (Point.cross  (Point.subtractPoints line2.a line1.a) s) / rxs
        let u = (Point.cross  (Point.subtractPoints line2.a line1.a) r) / rxs
        if (0. <= t && t <= 1.) && (0. <= u && u <= 1.) then Some(Point.addPoints line1.a (Point.multiplyPoint r t))
        else None
        
let getIntersectionPoint (line1: Line) (line2: Line) =
    if intersects line1 line2 then line_segment_intersects line1 line2
    else None


