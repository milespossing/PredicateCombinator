namespace PredicateBuilder
{
    public interface IPredicateBuilder<T>
    {
        IFluentPredicateBuilderInitial<T> BuildPredicate();
    }
}