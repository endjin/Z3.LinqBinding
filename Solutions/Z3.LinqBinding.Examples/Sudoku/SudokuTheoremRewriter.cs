namespace Z3.LinqBinding.Examples.Sudoku
{
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public class SudokuTheoremRewriter : ITheoremGlobalRewriter
    {
        public IEnumerable<LambdaExpression> Rewrite(IEnumerable<LambdaExpression> constraints)
        {
            return constraints;
        }
    }
}