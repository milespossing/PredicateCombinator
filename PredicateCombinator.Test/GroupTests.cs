using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace PredicateCombinator.Test
{
    [TestFixture]
    public class GroupTests
    {
        [Test]
        public void SimpleGroup()
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
            var func = fluent.BuildPredicate().WithGroup(fluent =>fluent.WithPredicate(s => s.Contains("good")).And(s => s.Contains('1'))).AsFunc();
            var result = strings.Where(func);
            Assert.AreEqual(2, result.Count());
            Assert.True(result.All(s => s == "good1"));
        }
        
        [Test]
        public void SimpleAndGroup()
        {
            var strings = new List<string>()
            {
                "a_good1",
                "a_good1",
                "a_good2",
                "a_good1_bad",
                "a_good2_bad",
                "bad",
                "c_good1",
                "c_good2"
            };
            var fluent = new PredicateBuilder<string>();
            var func = fluent.BuildPredicate()
                .WithGroup(fluent =>fluent.WithPredicate(s => s.Contains("good")).And(s => s.Contains('1')))
                .AndGroup(fluent => fluent.WithPredicate(s => s.StartsWith('a')).And(s => !s.Contains("bad")))
                .AsFunc();
            var result = strings.Where(func);
            Assert.AreEqual(2, result.Count());
            Assert.True(result.All(s => s == "a_good1"));
        }
        
        [Test]
        public void SimpleOrGroup()
        {
            var strings = new List<string>()
            {
                "a_good1",
                "a_good1",
                "a_good2",
                "a_good1_bad",
                "a_good2_bad",
                "bad",
                "c_good1",
                "c_good2",
                "c_good1_bad",
                "c_good2_bad",
            };
            var fluent = new PredicateBuilder<string>();
            var func = fluent.BuildPredicate()
                .WithGroup(fluent => fluent.WithPredicate(s => s.Contains("good")).And(s => s.Contains('1')))
                .AndGroup(fluent => fluent.WithPredicate(s => s.StartsWith('a')).And(s => !s.Contains("bad")))
                .OrGroup(fluent => fluent.WithPredicate(s => s.StartsWith('c')).And(s => !s.Contains("bad")))
                .AsFunc();
            var result = strings.Where(func);
            Assert.AreEqual(4, result.Count());
            Assert.True(result.All(s => s.Contains("good")));
        }
    }
}