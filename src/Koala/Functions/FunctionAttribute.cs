using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koala.Functions;

[AttributeUsage(AttributeTargets.Method)]
public class FunctionAttribute : Attribute
{
    public string? Name { get; set; }
}
