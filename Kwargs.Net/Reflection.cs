using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Kwargs.Net
{
    public static class Reflection
    {
        public static T? CreateInstance<T>(object[] fixedParameters, IDictionary<string, object> kwargs)
            where T : class
        {
            Type type = typeof(T);
            (int _, ConstructorInfo ctor, List<object> parameters) = GetParameters(type.GetConstructors(), fixedParameters, kwargs);
            return parameters.Any()
                ? ctor.Invoke(parameters.ToArray()) as T
                : Activator.CreateInstance<T>();
        }

        public static object? CallFunction(Type type, string methodName, object[] fixedParameters, IDictionary<string, object> kwargs)
        {
            MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static).Where(item => item.Name == methodName).ToArray();
            Trace.Assert(methods.Any(), $"Static method: {methodName} not found.");
            (int kwargsUseCount, MethodInfo method, List<object> parameters) = GetParameters(methods, fixedParameters, kwargs);
            return parameters.Any()
                ? method.Invoke(null, parameters.ToArray())
                : method.Invoke(null, null);
        }

        public static object? CallFunction(object o, string methodName, object[] fixedParameters, IDictionary<string, object> kwargs)
        {
            MethodInfo[] methods = o.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(item => item.Name == methodName).ToArray();
            Trace.Assert(methods.Any(), $"Static method: {methodName} not found.");
            (int kwargsUseCount, MethodInfo method, List<object> parameters) = GetParameters(methods, fixedParameters, kwargs);
            return parameters.Any()
                ? method.Invoke(o, parameters.ToArray())
                : method.Invoke(null, null);
        }

        private static (int, T, List<object>) GetParameters<T>(T[] infos, object[] fixedParameters, IDictionary<string, object> kwargs)
            where T : MethodBase
        {
            List<(int, T, List<object>)> methodParameters = new();
            foreach (MethodBase method in infos)
            {
                List<object> parameters = new();
                bool isIterateToTheEnd = true;
                int kwargsUseCount = 0;
                foreach ((int index, ParameterInfo param) in method.GetParameters().Select((value, index) => (index, value)))
                {
                    if (index < fixedParameters.Length)
                    {
                        Type fixedParameterType = fixedParameters[index].GetType();
                        Type paramType = param.ParameterType;
                        if (fixedParameterType != paramType && !fixedParameterType.IsSubclassOf(paramType))
                        {
                            isIterateToTheEnd = false;
                            break;
                        }
                        parameters.Add(fixedParameters[index]);
                    }
                    else if (kwargs.ContainsKey(param.Name!))
                    {
                        parameters.Add(kwargs[param.Name!]);
                        kwargsUseCount++;
                    }
                    else if (ParameterDefaultValue.TryGetDefaultValue(param, out object? defaultValue))
                    {
                        parameters.Add(defaultValue!);
                    }
                    else
                    {
                        isIterateToTheEnd = false;
                        break;
                    }
                }
                if (isIterateToTheEnd)
                    methodParameters.Add((kwargsUseCount, method as T, parameters)!);
            }
            Trace.Assert(methodParameters.Any(), "kwargs is missing a required parameter");
            return methodParameters.OrderByDescending(item => item.Item1).ThenBy(item => item.Item3.Count).First();
        }
    }
}
