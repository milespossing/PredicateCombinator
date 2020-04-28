using System;

namespace PredicateBuilder.Internal
{
    static class ExtensionMethods
    {
        public static PredicateCombinator Reverse(this PredicateCombinator combinator)
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