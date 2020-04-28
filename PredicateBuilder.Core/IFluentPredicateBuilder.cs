using System;
using System.Linq.Expressions;

namespace PredicateBuilder
{
    public interface IFluentPredicateBuilderInitial<T>
    {
        IFluentPredicateBuilder<T> WithPredicate(Func<T,bool> predicate);
    }

    public interface IFluentPredicateBuilder<T>
    {
        IFluentPredicateBuilder<T> And(Func<T,bool> predicate);
        IFluentPredicateBuilder<T> Or(Func<T,bool> predicate);
        IFluentPredicateBuilder<T> AndGroup(Action<IFluentPredicateBuilderInitial<T>> build);
        IFluentPredicateBuilder<T> OrGroup(Action<IFluentPredicateBuilderInitial<T>> build);
        Func<T,bool> AsFunc();
        Predicate<T> AsPredicate();
    }
}