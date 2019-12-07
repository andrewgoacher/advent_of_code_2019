module ``Completed Challenges``
    open NUnit.Framework
    open FsUnit

    [<Test>]
    let ``Day 1: Part 1 - Single mass calculation`` ()=
        Day1.fuel_for_mass 12. |> should equal 2.
        Day1.fuel_for_mass 100756. |> should equal 33583.

    [<Test>]
    let ``Day 1: Part 1 - Multiple mass calculation`` ()=
        Day1.solve_part_1 [|1969.;100756.;|] |> should equal 34237.

    [<Test>]
    let ``Day 1: Part 2 - Single mass calculation``()=
        Day1.total_fuel_for_mass 1969. |> should equal 2935.
        Day1.total_fuel_for_mass 100756. |> should equal 151102.

    [<Test>]
    let ``Day 1: Part 2 - Multiple mass calculation``()=
        Day1.solve_part_2 [|1969.;14.;|] |> should equal 968.