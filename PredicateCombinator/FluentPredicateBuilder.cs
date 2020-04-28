using System;
using System.Linq.Expressions;
using PredicateBuilder.Internal;

namespace PredicateBuilder
{
    public class FluentPredicateBuilder<T> : IFluentPredicateBuilderInitial<T>, IFluentPredicateBuilder<T>
    {
        private IPredicateTree<T> _tree;

        public FluentPredicateBuilder()
        {
            _tree = new PredicatePredicateTree<T>(PredicateCombinator.And);
        }

        public IFluentPredicateBuilder<T> WithPredicate(Func<T,bool> predicate)
        {
            _tree.AddPredicate(new StatementTree<T>(predicate));
            return this;
        }

        public IFluentPredicateBuilder<T> And(Func<T,bool> predicate)
        {
            AddPredicate(predicate, PredicateCombinator.And);
            return this;
        }

        public IFluentPredicateBuilder<T> Or(Func<T,bool> predicate)
        {
            AddPredicate(predicate, PredicateCombinator.Or);
            return this;
        }

        private void AddPredicate(Func<T, bool> predicate, PredicateCombinator combinator)
        {
            if (_tree.Combinator == combinator)
            {
                _tree.AddPredicate(new StatementTree<T>(predicate));
            }
            else
            {
                _tree = new PredicatePredicateTree<T>(_tree.Combinator.Reverse(),_tree);
                _tree.AddPredicate(new StatementTree<T>(predicate));
            }
        }

        public IFluentPredicateBuilder<T> AndGroup(Action<IFluentPredicateBuilderInitial<T>> build)
        {
            var predicate = CreateGroup(build);
            AddPredicate(predicate, PredicateCombinator.And);
            return this;
        }

        public IFluentPredicateBuilder<T> OrGroup(Action<IFluentPredicateBuilderInitial<T>> build)
        {
            var predicate = CreateGroup(build);
            AddPredicate(predicate, PredicateCombinator.Or);
            return this;
        }

        private Func<T,bool> CreateGroup(Action<IFluentPredicateBuilderInitial<T>> buildAction)
        {
            var group = new FluentPredicateBuilder<T>();
            buildAction(group);
            return group.AsFunc();
        }

        public Func<T,bool> AsFunc()
        {
            return _tree.Build();
        }

        public Predicate<T> AsPredicate()
        {
            return t => _tree.Build()(t);
        }

        public Expression<Func<T, bool>> AsExpression()
        {
            throw new NotImplementedException();
        }
    }
}