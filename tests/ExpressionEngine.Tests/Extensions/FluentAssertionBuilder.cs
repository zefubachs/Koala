using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEngine.Tests.Extensions;
public class FluentAssertionBuilder
{
    public object? Instance { get; }

    public FluentAssertionBuilder(object? instance)
    {
        Instance = instance;
    }
}
