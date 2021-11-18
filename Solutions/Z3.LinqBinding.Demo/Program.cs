namespace Z3.LinqBindingDemo
{
    using System;
    using System.Diagnostics;

    using Z3.LinqBinding;
    using Z3.LinqBinding.Sudoku;

    public static class Program
    {
        private static Stopwatch stopwatch = Stopwatch.StartNew();

        private static void Main(string[] args)
        {
            Console.WriteLine("==== Missionaries & Cannibals ====");

            using (var ctx = new Z3Context())
            {
                // ctx.Log = Console.Out; // see internal logging

                var theorem =
                    from t in  MissionariesAndCannibals.Create(ctx, 50)
                    where t.MissionaryAndCannibalCount == 3
                    where t.SizeBoat == 2
                    select t;

                var sw = Stopwatch.StartNew();

                MissionariesAndCannibals? result = theorem.Solve();
                sw.Stop();

                Console.WriteLine(result);
                Console.WriteLine($"Time to solution: {sw.Elapsed.TotalMilliseconds} ms");
                Console.WriteLine();

                sw = Stopwatch.StartNew();
                var minimal = theorem.Optimize(Optimization.Minimize, t => t.Length);
                sw.Stop();

                Console.WriteLine(minimal);
                Console.WriteLine($"Time to optimised solution: {sw.Elapsed.TotalMilliseconds} ms");
                Console.WriteLine();
            }

            Console.WriteLine("==== t.x ^ t.y using Anonymous Types ====");

            using (var ctx = new Z3Context())
            {
                // ctx.Log = Console.Out; // see internal logging

                var theorem = from t in ctx.NewTheorem(new { x = default(bool), y = default(bool) })
                              where t.x ^ t.y
                              select t;

                var result = theorem.Solve();

                Console.WriteLine(result);
            }

            Console.WriteLine("==== t.x ^ t.y using ValueTuples ====");

            using (var ctx = new Z3Context())
            {
                // ctx.Log = Console.Out;

                var theorem = from t in ctx.NewTheorem<(bool x, bool y)>()
                              where t.x ^ t.y
                              select t;

                var result = theorem.Solve();

                Console.WriteLine(result);
            }

            Console.WriteLine("==== t.x ^ t.y using Custom Theorem ====");

            using (var ctx = new Z3Context())
            {
                // ctx.Log = Console.Out; // see internal logging

                var theorem = from t in ctx.NewTheorem(new RTheorem<bool, bool>())
                              where t.X ^ t.Y
                              select t;

                var result = theorem.Solve();
                Console.WriteLine(result);
            }

            Console.WriteLine("==== Bart's example from TechEd Europe 2012 ====");
            
            using (var ctx = new Z3Context())
            {
                // ctx.Log = Console.Out;

                var theorem = from t in ctx.NewTheorem<Symbols<int, int, int>>()
                              where t.X1 - t.X2 >= 1
                              where t.X1 - t.X2 <= 3
                              where t.X1 == (2 * t.X3) + t.X2
                              select t;

                var result = theorem.Solve();

                Console.WriteLine(result);
            }

            Console.WriteLine("==== Bart's example from TechEd Europe 2012 using ValueTuples ====");
            
            using (var ctx = new Z3Context())
            {
                // ctx.Log = Console.Out;

                var theorem = from t in ctx.NewTheorem<(int x, int y, int z)>()
                              where t.x - t.y >= 1
                              where t.x - t.y <= 3
                              where t.x == (2 * t.z) + t.y
                              select t;

                var result = theorem.Solve();

                Console.WriteLine(result);
            }

            Console.WriteLine("====  Example using Symbols<T1, T2> ====");

            using (var ctx = new Z3Context())
            {
                // ctx.Log = Console.Out; // see internal logging

                var theorem = from t in ctx.NewTheorem<Symbols<int, int>>()
                              where t.X1 < t.X2 + 1
                              where t.X1 > 2
                              where t.X1 != t.X2
                              select t;

                var result = theorem.Solve();

                Console.WriteLine(result);
            }

            Console.WriteLine("====  SudokuTheorem Example ====");

            using (var ctx = new Z3Context())
            {
                var theorem = from t in SudokuTheorem.Create(ctx)
                              where t.Cell13 == 2 && t.Cell16 == 1 && t.Cell18 == 6
                              where t.Cell23 == 7 && t.Cell26 == 4
                              where t.Cell31 == 5 && t.Cell37 == 9
                              where t.Cell42 == 1 && t.Cell44 == 3
                              where t.Cell51 == 8 && t.Cell55 == 5 && t.Cell59 == 4
                              where t.Cell66 == 6 && t.Cell68 == 2
                              where t.Cell73 == 6 && t.Cell79 == 7
                              where t.Cell84 == 8 && t.Cell87 == 3
                              where t.Cell92 == 4 && t.Cell94 == 9 && t.Cell97 == 2
                              select t;

                var result = theorem.Solve();

                Console.WriteLine(result);
            }

            Console.WriteLine("====  SudokuTheorem Example from https://sandiway.arizona.edu/sudoku/examples.html ====");

            using (var ctx = new Z3Context())
            {
                var theorem = from t in SudokuTheorem.Create(ctx)
                              where t.Cell14 == 2 && t.Cell15 == 6 && t.Cell17 == 7 && t.Cell19 == 1
                              where t.Cell21 == 6 && t.Cell22 == 8 && t.Cell25 == 7 && t.Cell28 == 9
                              where t.Cell31 == 1 && t.Cell32 == 9 && t.Cell36 == 4 && t.Cell37 == 5
                              where t.Cell41 == 8 && t.Cell42 == 2 && t.Cell44 == 1 && t.Cell48 == 4
                              where t.Cell53 == 4 && t.Cell54 == 6 && t.Cell56 == 2 && t.Cell57 == 9
                              where t.Cell62 == 5 && t.Cell66 == 3 && t.Cell68 == 2 && t.Cell69 == 8
                              where t.Cell73 == 9 && t.Cell74 == 3 && t.Cell78 == 7 && t.Cell79 == 4
                              where t.Cell82 == 4 && t.Cell85 == 5 && t.Cell88 == 3 && t.Cell89 == 6
                              where t.Cell91 == 7 && t.Cell93 == 3 && t.Cell95 == 1 && t.Cell96 == 8

                              select t;

                var result = theorem.Solve();
                Console.WriteLine(result);
            }

            /* 
            // Bart's Oil Barrel example
            using (var ctx = new Z3Context())
            {
                ctx.Log = Console.Out;

                var theorem = from t in ctx.NewTheorem(new { vz = default(double), sa = default(double) })
                                where 0.3 * t.sa +
                                    0.4 * t.vz > -2000 &&
                                    0.4 * t.sa +
                                    0.2 * t.vz >= 1500 &&
                                    0.2 * t.sa +
                                    0.3 * t.vz > +500
                                where 0 <= t.sa &&
                                    t.sa <= 9000 &&
                                    0 <= t.vz &&
                                    t.vz <= 6000
                                //orderby 20 * t.sa + 15 * t.vz
                                select t;

                var result = theorem.Solve();

                //{ vz = 0, sa = 422212465065984 }

                Console.WriteLine(result);
            }
            
            // All samples

            using (var ctx = new Z3Context())
            {
                // ctx.Log = Console.Out; // see internal logging

                Print(from t in ctx.NewTheorem(new { x = default(bool) })
                      where t.x && !t.x
                      select t);

                Print(from t in ctx.NewTheorem(new { x = default(bool), y = default(bool) })
                      where t.x ^ t.y
                      select t);

                Print(from t in ctx.NewTheorem(new { x = default(int), y = default(int) })
                      where t.x < t.y + 1
                      where t.x > 2
                      select t);

                Print(from t in ctx.NewTheorem<Symbols<int, int>>()
                      where t.X1 < t.X2 + 1
                      where t.X1 > 2
                      where t.X1 != t.X2
                      select t);

                Print(from t in ctx.NewTheorem<Symbols<int, int, int, int, int>>()
                      where t.X1 - t.X2 >= 1
                      where t.X1 - t.X2 <= 3
                      where t.X1 == (2 * t.X3) + t.X5
                      where t.X3 == t.X5
                      where t.X2 == 6 * t.X4
                      select t);

                Print(from t in ctx.NewTheorem<Symbols<int, int>>()
                      where Z3Methods.Distinct(t.X1, t.X2)
                      select t);

                Print(from t in SudokuTheorem.Create(ctx)
                      where t.Cell13 == 2 && t.Cell16 == 1 && t.Cell18 == 6
                      where t.Cell23 == 7 && t.Cell26 == 4
                      where t.Cell31 == 5 && t.Cell37 == 9
                      where t.Cell42 == 1 && t.Cell44 == 3
                      where t.Cell51 == 8 && t.Cell55 == 5 && t.Cell59 == 4
                      where t.Cell66 == 6 && t.Cell68 == 2
                      where t.Cell73 == 6 && t.Cell79 == 7
                      where t.Cell84 == 8 && t.Cell87 == 3
                      where t.Cell92 == 4 && t.Cell94 == 9 && t.Cell97 == 2
                      select t);
            }*/

            Console.ReadKey();
        }

        private static void Print<T>(Theorem<T> t) where T : class
        {
            Console.WriteLine(t);
            var res = t.Solve();
            Console.WriteLine(res == null ? "none" : res.ToString());
            Console.WriteLine();
        }
    }
}