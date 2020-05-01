using System;
using System.Collections.Generic;
using System.Linq;

namespace PredicateCombinator.Internal
{
    class PredicatePredicateTree<T> : IPredicateTree<T>
    {
        private List<IPredicateBranch<T>> _children = new List<IPredicateBranch<T>>();
        
        public PredicatePredicateTree(PredicateCombinator combinator)
        {
            Combinator = combinator;
        }

        public PredicatePredicateTree(PredicateCombinator combinator, IPredicateTree<T> firstChild) : this(combinator)
        {
            AddPredicate(firstChild);
        }
        
        public PredicateCombinator Combinator { get; }
        public void AddPredicate(IPredicateBranch<T> predicate)
        {
            _children.Add(predicate);
        }

        public Func<T, bool> Build()
        {
            switch (Combinator)
            {
                case PredicateCombinator.And:
                    return t => _children.All(c => c.Build()(t));
                case PredicateCombinator.Or:
                    return t => _children.Any(c => c.Build()(t));
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}