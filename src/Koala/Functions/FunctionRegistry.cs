using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Koala.Functions;
public class FunctionRegistry
{
    private readonly Dictionary<string, FunctionDescriptor> functions = new Dictionary<string, FunctionDescriptor>(StringComparer.OrdinalIgnoreCase);

    public FunctionDescriptor? Find(string name)
    {
        if (functions.TryGetValue(name, out var function))
            return function;

        return null;
    }

    public void Register<T>() where T : class
        => Register(typeof(T));

    public void Register(Type type)
    {
        var methods = from method in type.GetMethods(BindingFlags.Public | BindingFlags.Static)
                      select new
                      {
                          info = method,
                          attr = method.GetCustomAttribute<FunctionAttribute>()
                      }
                      into f
                      where f.attr is not null
                      select f;

        foreach (var method in methods)
        {
            var name = method.attr?.Name ?? method.info.Name;
            var descriptor = new FunctionDescriptor(method.info);
            functions.TryAdd(name, descriptor);
        }
    }

    public void Register(Assembly assembly)
    {
        var types = assembly.ExportedTypes.Where(x => x.IsPublic);
        foreach (var type in types)
        {
            Register(type);
        }
    }
}
