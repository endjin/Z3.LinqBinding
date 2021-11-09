namespace Z3.LinqBinding
{
    using System;

    public class TheoremGlobalRewriterAttribute : Attribute
    {
        public Type RewriterType { get; set; }
    }
}