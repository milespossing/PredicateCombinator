using System;

namespace PredicateCombinator.Internal
{
    interface IPredicateBranch<T>
    {
        Func<T, bool> Build();
    }
    
    interface IPredicateTree<T> : IPredicateBranch<T>
    {
        PredicateCombinator Combinator { get; }
        void AddPredicate(IPredicateBranch<T> predicate);
    }
}