module Geometry

open NUnit.Framework
open FsUnit
open AdventOfCode2019.Geometry
open AdventOfCode2019.Types

[<Test>]
let ``intersects - lines parrallel - do not intersect``() =
    let p1 =
        { x = 4.
          y = 5. }

    let p2 =
        { x = 4.
          y = 10. }

    let p3 =
        { x = 10.
          y = 3. }

    let p4 =
        { x = 10.
          y = 4. }

    let line1 =
        { a = p1
          b = p2
          dir = "" }

    let line2 =
        { a = p3
          b = p4
          dir = "" }

    Line.intersects line1 line2 |> should be False

[<Test>]
let ``intersects - lines cross - should intersect``() =
    let p1 =
        { x = 0.
          y = 0. }

    let p2 =
        { x = 10.
          y = 10. }

    let p3 =
        { x = 2.
          y = 0. }

    let p4 =
        { x = (-2.0)
          y = 4. }

    let line1 =
        { a = p1
          b = p2
          dir = "" }

    let line2 =
        { a = p3
          b = p4
          dir = "" }

    Line.intersects line1 line2 |> should be True

[<Test>]
let ``2 lines that if infinate would intersect, should not intersect``() =
    let p1 =
        { x = 0.
          y = 0. }

    let p2 =
        { x = 4.
          y = 4. }

    let p3 =
        { x = 5.
          y = 5. }

    let p4 =
        { x = 10.
          y = 0. }

    let line1 =
        { a = p1
          b = p2
          dir = "" }

    let line2 =
        { a = p3
          b = p4
          dir = "" }

    Line.intersects line1 line2 |> should be False

[<Test>]
let ``2 parallell lines will not have an intersection point``() =
    let p1 =
        { x = 4.
          y = 5. }

    let p2 =
        { x = 4.
          y = 10. }

    let p3 =
        { x = 10.
          y = 3. }

    let p4 =
        { x = 10.
          y = 4. }

    let line1 =
        { a = p1
          b = p2
          dir = "" }

    let line2 =
        { a = p3
          b = p4
          dir = "" }

    Line.getIntersectionPoint line1 line2 |> should equal None

[<Test>]
let ``2 intersecting lines will have an intersection``() =
    let p1 =
        { x = 0.
          y = 0. }
    let p2 =
        { x = 10.
          y = 10. }
    let p3 =
        { x = 2.
          y = 0. }
    let p4 =
        { x = (-2.0)
          y = 4. }

    let line1 =
        { a = p1
          b = p2
          dir = "" }
    let line2 =
        { a = p3
          b = p4
          dir = "" }

    let expected =
        { x = 1.
          y = 1. }

    let actual = Line.getIntersectionPoint line1 line2

    actual |> should not' (equal None)
    actual.Value |> should equal expected

[<Test>]
let ``if 2 lines share a point, they are not considered to be intersecting``() =
    let p1 =
        { x = 0.
          y = 0. }

    let p2 =
        { x = 10.
          y = 10. }

    let p3 =
        { x = 0.
          y = 0. }

    let p4 =
        { x = (-2.0)
          y = 4. }

    let line1 =
        { a = p1
          b = p2
          dir = "" }

    let line2 =
        { a = p3
          b = p4
          dir = "" }

    Line.intersects line1 line2 |> should be False
