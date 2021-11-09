namespace Z3.LinqBinding
{
    using System.Linq.Expressions;

    public interface ITheoremPredicateRewriter
    {
        MethodCallExpression Rewrite(MethodCallExpression call);
    }
}