using System;

namespace PredicateCombinator.Internal
{
    class StatementTree<T> : IPredicateBranch<T>
    {
        private Func<T, bool> _func;
        
        public StatementTree(Func<T, bool> func)
        {
            _func = func;
        }

        public Func<T, bool> Build()
        {
            return _func;
        }
    }
}