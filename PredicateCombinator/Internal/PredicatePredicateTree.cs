using System;
using System.Collections.Generic;
using System.Linq;

namespace PredicateBuilder.Internal
{
    class PredicatePredicateTree<T> : IPredicateTree<T>
    {
        private List<IPredicateTree<T>> _children = new List<IPredicateTree<T>>();
        
        public PredicatePredicateTree(PredicateCombinator combinator)
        {
            Combinator = combinator;
        }

        public PredicatePredicateTree(PredicateCombinator combinator, IPredicateTree<T> firstChild) : this(combinator)
        {
            AddPredicate(firstChild);
        }
        
        public PredicateCombinator Combinator { get; }
        public void AddPredicate(IPredicateTree<T> predicate)
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