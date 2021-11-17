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
            // Solving Canibals & Missionaires

            using (var ctx = new Z3Context())
            {
                // ctx.Log = Console.Out; // see internal logging

                var can = new MissionariesAndCannibals { NbMissionaries = 3, SizeBoat = 2, Length = 50 };

                var theorum = can.Create(ctx);

                var startTime = stopwatch.Elapsed;

                var result = theorum.Solve();

                var endTime = stopwatch.Elapsed;

                Console.WriteLine(result);
                Console.WriteLine();
                Console.WriteLine($"Time to solve: {endTime - startTime}");
            }

            // Basic Usage
            using (var ctx = new Z3Context())
            {
                // ctx.Log = Console.Out; // see internal logging

                var theorem = from t in ctx.NewTheorem(new { x = default(bool), y = default(bool) })
                              where t.x ^ t.y
                              select t;

                var result = theorem.Solve();
                Console.WriteLine(result);
            }

            // One of Bart's examples from TechEd Europe 2012
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

            // One of Bart's examples from TechEd Europe 2012 using ValueTuples
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

            // Bart's Oil Barrel example
           /* using (var ctx = new Z3Context())
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
            }*/

            // Example using ValueTuples
            using (var ctx = new Z3Context())
            {
                // ctx.Log = Console.Out;

                var theorem = from t in ctx.NewTheorem<(bool x, bool y)>()
                              where t.x ^ t.y
                              select t;

                var result = theorem.Solve();

                Console.WriteLine(result);
            }

            using (var ctx = new Z3Context())
            {
                // ctx.Log = Console.Out; // see internal logging

                var theorem = from t in ctx.NewTheorem(new RTheorem<bool, bool>())
                              where t.X ^ t.Y
                              select t;

                var result = theorem.Solve();
                Console.WriteLine(result);
            }

            // Advanced Usage
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

            // Sudoku Extension Usage (Z3.LinqBinding.Sudoku)
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
            }

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