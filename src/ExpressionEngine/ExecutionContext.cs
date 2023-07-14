using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEngine;
public class ExecutionContext
{
    public IReadOnlyDictionary<string, object?> Parameters { get; }

    public ExecutionContext(Dictionary<string, object?> parameters)
    {
        Parameters = parameters.AsReadOnly();
    }

}
