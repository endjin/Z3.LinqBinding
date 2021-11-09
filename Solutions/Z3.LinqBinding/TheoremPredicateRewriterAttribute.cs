namespace Z3.LinqBinding
{
    using System;

    public class TheoremPredicateRewriterAttribute : Attribute
    {
        public Type RewriterType { get; set; }
    }
}