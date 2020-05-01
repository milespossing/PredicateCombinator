using System;

namespace PredicateCombinator.Internal
{
    static class ExtensionMethods
    {
        public static PredicateCombinator LogicalComplement(this PredicateCombinator combinator)
        {
            switch (combinator)
            {
                case PredicateCombinator.And:
                    return PredicateCombinator.Or;
                case PredicateCombinator.Or:
                    return PredicateCombinator.And;
                default:
                    throw new ArgumentOutOfRangeException(nameof(combinator), combinator, null);
            }
        }
    }
}