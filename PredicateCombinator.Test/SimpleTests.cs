using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace PredicateCombinator.Test
{
    public class SimpleTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SimplePredicateWorks()
        {
            var strings = new List<string>()
            {
                "good",
                "good",
                "bad",
                "good",
                "good"
            };
            var fluent = new PredicateBuilder<string>();
            var func = fluent.BuildPredicate().WithPredicate(s => s.Equals("good")).AsFunc();
            var result = strings.Where(func);
            Assert.AreEqual(4, result.Count());
            Assert.True(result.All(s => s == "good"));
        }

        [Test]
        public void AndPredicateWorks()
        {
            var strings = new List<string>()
            {
                "good1",
                "good2",
                "bad",
                "good1",
                "good2"
            };
            var fluent = new PredicateBuilder<string>();
            var func = fluent.BuildPredicate().WithPredicate(s => s.Contains("good")).And(s => s.Contains("2")).AsFunc();
            var result = strings.Where(func);
            Assert.AreEqual(2, result.Count());
            Assert.True(result.All(s => s == "good2"));
        }

        [Test]
        public void OrPredicateWorks()
        {
            var strings = new List<string>()
            {
                "good1",
                "good2",
                "bad",
                "good1",
                "good2"
            };
            var fluent = new PredicateBuilder<string>();
            var func = fluent.BuildPredicate().WithPredicate(s => s.Contains("1")).Or(s => s.Contains("2")).AsFunc();
            var result = strings.Where(func);
            Assert.AreEqual(4, result.Count());
            Assert.True(result.All(s => s.Contains("good")));
        }

        [Test]
        public void AndOrPredicateWorks()
        {
            var strings = new List<string>()
            {
                "a_good1",
                "a_good2",
                "bad1",
                "c_good1",
                "c_good2"
            };
            var fluent = new PredicateBuilder<string>();
            var func = fluent.BuildPredicate().WithPredicate(s => s.Contains("1")).And(s => s.Contains("good")).Or(s => s.StartsWith('c')).AsFunc();
            var result = strings.Where(func);
            Assert.AreEqual(3, result.Count());
            Assert.True(result.All(s => s.Contains("good")));
            Assert.False(result.Any(s => s.Contains("bad")));
        }

        [Test]
        public void CanGetPredicate()
        {
            var strings = new List<string>()
            {
                "a_good1",
                "a_good2",
                "bad1",
                "c_good1",
                "c_good2"
            };
            var fluent = new PredicateBuilder<string>();
            var func = fluent.BuildPredicate().WithPredicate(s => s.Contains("1")).And(s => s.Contains("good")).Or(s => s.StartsWith('c')).AsPredicate();
            var result = strings.FindAll(func);
            Assert.AreEqual(3, result.Count());
            Assert.True(result.All(s => s.Contains("good")));
            Assert.False(result.Any(s => s.Contains("bad")));
        }
    }
}