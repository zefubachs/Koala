using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Koala.Functions;
public class FunctionDescriptor
{
    private readonly MethodInfo methodInfo;

    private readonly Delegate methodDelegate;

    public FunctionDescriptor(MethodInfo methodInfo)
    {
        this.methodInfo = methodInfo;
        methodDelegate = CreateDelegate(methodInfo);
    }

    public object? Invoke(object?[] arguments)
    {


        return methodDelegate.DynamicInvoke(arguments);
    }

    private static Delegate CreateDelegate(MethodInfo method)
    {
        var parameterInfos = method.GetParameters();
        var arguments = new Expression[parameterInfos.Length];
        var parameterExpressions = new List<ParameterExpression>();
        for (int i = 0; i < arguments.Length; i++)
        {
            var parameter = parameterInfos[i];
            var expression = Expression.Parameter(parameter.ParameterType, parameter.Name);
            arguments[i] = expression;
            parameterExpressions.Add(expression);
        }

        var callExpression = Expression.Call(null, method, arguments);
        var lambda = Expression.Lambda(callExpression, parameterExpressions);
        return lambda.Compile();
    }
}
