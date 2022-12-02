using CallStaticFunction;
using Kwargs.Net;

var result = Reflection.CallFunction(typeof(TestClass), nameof(TestClass.Test), Array.Empty<object>(), new Dictionary<string, object>());
Console.WriteLine(result);
