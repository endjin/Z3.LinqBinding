namespace Z3.LinqBinding
{
    using System;

    public class TheoremGlobalRewriterAttribute : Attribute
    {
        public TheoremGlobalRewriterAttribute(Type rewriterType)
        {
            this.RewriterType = rewriterType;
        }

        public Type RewriterType { get; }
    }
}