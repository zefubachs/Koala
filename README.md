# Koala expression compiler

A simple library to compile string expressions into syntax trees.

Usage
```csharp
var engine = ExpressionEngine.CreateDefault();
var parameters = new DictionaryParameterProvider
{
    ["Param1"] = 2,
    ["Param2"] = 3,
};
var result = engine.Execute("@Param1 + @Param2", parameters);
Assert.AreEqual(5, result.Value);
```
## To do
- Better function model

A better way to develop custom functions.

- Dependency injection friendly

Using Microsoft.Extensions.DependencyInjection.Abstractions package to make it possible to inject services in custom created functions.

- Execution logging

Add logging to AST nodes that is added to the Execute method result object.
