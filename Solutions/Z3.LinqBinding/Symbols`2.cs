namespace Z3.LinqBinding
{
    /// <summary>
    /// Pre-defined enviroment with two symbols.
    /// </summary>
    /// <typeparam name="T1">Type of the first symbol.</typeparam>
    /// <typeparam name="T2">Type of the second symbol.</typeparam>
    public sealed class Symbols<T1, T2> : Symbols
    {
        /// <summary>
        /// Provides access to the first symbol.
        /// </summary>
        /// <remarks>
        /// Used within definition of theorem constraints and to print the theorem prover's result.
        /// </remarks>
#nullable disable warnings
        public T1 X1 { get; private set; }
#nullable restore warnings

        /// <summary>
        /// Provides access to the second symbol.
        /// </summary>
        /// <remarks>
        /// Used within definition of theorem constraints and to print the theorem prover's result.
        /// </remarks>
        public T2 X2 { get; private set; } = default!;
    }
}