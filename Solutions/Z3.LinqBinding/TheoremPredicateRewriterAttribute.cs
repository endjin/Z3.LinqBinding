namespace Z3.LinqBinding
{
    using System;

    public class TheoremPredicateRewriterAttribute : Attribute
    {
        public TheoremPredicateRewriterAttribute(Type rewriterType)
        {
            this.RewriterType = rewriterType;
        }

        public Type RewriterType { get; }
    }
}