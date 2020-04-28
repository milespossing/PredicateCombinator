using System;

namespace PredicateBuilder.Internal
{
    class StatementTree<T> : IPredicateTree<T>
    {
        private Func<T, bool> _func;
        
        public StatementTree(Func<T, bool> func)
        {
            _func = func;
        }
        
        public PredicateCombinator Combinator => PredicateCombinator.And;
        public void AddPredicate(IPredicateTree<T> predicate)
        {
            throw new NotImplementedException();
        }

        public Func<T, bool> Build()
        {
            return _func;
        }
    }
}