using System;

namespace PredicateBuilder
{
    public class PredicateBuilder<T> : IPredicateBuilder<T>
    {
        public IFluentPredicateBuilderInitial<T> BuildPredicate()
        {
            return new FluentPredicateBuilder<T>();
        }
    }
}