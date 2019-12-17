module AdventOfCode2019.Geometry.Point

open AdventOfCode2019

let add point x y =
    { x = point.x + x
      y = point.y + y }
let addPoints point1 point2=
    {x=point1.x + point2.x
     y=point1.y + point2.y}
    
let subtractPoints point1 point2=
    {x = point1.x - point2.x
     y=point1.y - point2.y}
    
let multiplyPoint point v=
    {x = point.x * v
     y = point.y * v}
    
let cross point1 point2=
    point1.x * point2.y - point1.y * point2.x
    
let getManhattanDistance point1 point2 =
    abs (point1.x - point2.x) + abs (point1.y - point2.y)
