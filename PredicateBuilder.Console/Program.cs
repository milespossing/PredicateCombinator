using System;
using System.Linq;

namespace PredicateBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new PredicateBuilder<string>();
            var strings = new[] {"Test 1", "Test 2", "Test3", "Test 4 Exclude"};
            var predicate1 = builder.BuildPredicate()
                .WithPredicate(s => s.Contains("Exclude"))
                .Or(s => s.Contains('3')).AsFunc();
            var enumerable = strings.Where(predicate1).ToList();
            foreach (var s in enumerable)
            {
                Console.WriteLine(s);
            }
        }
    }
}