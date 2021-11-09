namespace Z3.LinqBinding.Sudoku
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