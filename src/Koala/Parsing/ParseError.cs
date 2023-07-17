using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koala.Parsing;
public class ParseError
{
    public string Message { get; }
    public int Column { get; }
}
