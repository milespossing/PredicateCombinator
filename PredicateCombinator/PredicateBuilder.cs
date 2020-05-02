namespace PredicateCombinator
{
    public class PredicateBuilder<T> : IPredicateBuilder<T>
    {
        public IFluentPredicateBuilderInitial<T> Build()
        {
            return new FluentPredicateBuilder<T>();
        }
    }
}