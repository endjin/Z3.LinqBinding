namespace Z3.LinqBinding
{
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface ITheoremGlobalRewriter
    {
        IEnumerable<LambdaExpression> Rewrite(IEnumerable<LambdaExpression> constraints);
    }
}