using System;

namespace PredicateBuilder.Internal
{
    interface IPredicateTree<T>
    {
        PredicateCombinator Combinator { get; }
        void AddPredicate(IPredicateTree<T> predicate);
        Func<T, bool> Build();
    }
}