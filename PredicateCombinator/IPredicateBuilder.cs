namespace PredicateCombinator
{
    public interface IPredicateBuilder<T>
    {
        IFluentPredicateBuilderInitial<T> BuildPredicate();
    }
}