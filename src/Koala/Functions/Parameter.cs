using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koala.Functions;
public class Parameter
{
    public object? Value { get; }

    public Parameter(object? value)
    {
        Value = value;
    }
}
