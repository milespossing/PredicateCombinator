# PredicateCombinator

A simple fluent api to combine predicates as needed

## Background

I've found it necessary several times to author predicates in numerous ways, 
and it always ends up being extremely difficult to read, write, and work around.

## Solution

A simple tree-based predicate combinator which uses a basic fluent api

## Usage

Currently, the api only allows for simple fluent creation of either a 
`Predicate<T>` or a `Func<T,bool>` using and/or statements, as well as 
grouped statements.

### Simple Usage

A `PredicateBuilder<T>` is used to create the predicate. Usage is simple:

```c#
// if (s.Contains('a'))
var builder = new PredicateBuilder<string>();
builder.Build()
       .WithPredicate(s => s.Contains('a'))
       .AsFunc();
```

Of course, more complex predicates can be built:

```c#
// if (s.Contains('a') || string.Equals('b'))
builder.Build()
       .WithPredicate(s => s.Contains('a'))
       .Or(s => string.Equals('b')
       .AsFunc();
```

### Groups

Predicates can include groups. These groups are themselves predicate trees which is 
ultimately added to the main expression tree. See [Combinator Logic](#combinator-logic).

A group requires an `Action<IFluentPredicateBuilder<T>>` to create the group's expression 
tree. Again, usage is relatively straightforward, just recursive.

```c#
// if ((s.Contains('a') && s.Contains('b')) ||  
builder.Build()
       .WithGroup(b => 
            b.WithPredicate(s => s.Contains('a'))
             .And(s => s.Contains('b')))
       .Or(s => s == "Other Test");
```

## Combinator Logic

Predicates are combined in groups. Groups can either be of `And` or `Or` combinators.
The first group is always an `And` group. Whenever a statement is added, if the statements
combinator type is the same as the previous statement's, the statement is simply added to the
current tree. e.g. if a group is already an `And` group, and another `And` statement is added:

```c#
builder.Build().WithPredicate(A).And(B).And(C)
```

results in the equivalent statement `A && B && C`. Wherever the combinator is switched, a new 
tree is created with the current tree as the first child node of the new node.

```c#
builder.Build()
       .WithPredicate(A)
       .And(B)
       .Or(C)
       .OR(D)
       .And(E)
```

Results in `((A && B) || C || D) && E`
