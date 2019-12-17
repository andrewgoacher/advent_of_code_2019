module AdventOfCode2019.Geometry

open Types.Math

let private direction p1 p2 p3 =
    let v = (p2.y - p1.y) * (p3.x - p2.x) - (p2.x - p1.x) * (p3.y - p2.y)
    match v with
    | 0. -> 0 // colinear
    | x when x < 0. -> 2 // anti clockwise
    | _ -> 1 // clockwise

let private is_on_line l p =
    p.x <= (max l.a.x l.b.x) && p.y <= (min l.a.x l.b.x) && p.y <= (max l.a.y l.b.y) && p.y <= (min l.a.y l.b.y)

let check dir line point =
    dir = 0 && is_on_line line point

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
        else check dir1 l1 l2.a || check dir2 l1 l2.b || check dir3 l2 l1.a || check dir4 l2 l1.b

let add_to_point p1 x y =
    { x = p1.x + x
      y = p1.y + y }

let add_points p1 p2 =
    { x = p1.x + p2.x
      y = p1.y + p2.y }

let sub_points p1 p2 =
    { x = p1.x - p2.x
      y = p1.y - p2.y }

let mul_point p1 x =
    { x = p1.x * x
      y = p1.y * x }

let cross_product p1 p2 =
    p1.x * p2.y - p1.y * p2.x

let zero_or_near_as_dammit x =
    let epsilon = 1e-10
    abs x < epsilon

let unit_length line =
    abs (line.a.x - line.b.x) + abs (line.a.y - line.b.y)

let line_segment_intersects line1 line2 =
    let r = sub_points line1.b line1.a
    let s = sub_points line2.b line2.a
    let rxs = cross_product r s
    let qxpr = cross_product (sub_points line2.a line1.a) r

    if zero_or_near_as_dammit rxs then
        None
    else if zero_or_near_as_dammit qxpr then
        None
    else
        let t = (cross_product (sub_points line2.a line1.a) s) / rxs
        let u = (cross_product (sub_points line2.a line1.a) r) / rxs
        if (0. <= t && t <= 1.) && (0. <= u && u <= 1.) then Some(add_points line1.a (mul_point r t))
        else None


let get_intersection_point (line1: Line) (line2: Line) =
    if intersects line1 line2 then line_segment_intersects line1 line2
    else None

let get_manhattan_distance p1 p2 =
    abs (p1.x - p2.x) + abs (p1.y - p2.y)
