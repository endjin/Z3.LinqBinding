namespace Z3.LinqBinding
{
    using Microsoft.Z3;

    using System.Reflection;

    public class Environment
    {
        public Expr Expr { get; set; }

        public Dictionary<MemberInfo, Environment> Properties { get; set; } = new Dictionary<MemberInfo, Environment>();

        public Boolean IsArray { get; set; }
    }
}