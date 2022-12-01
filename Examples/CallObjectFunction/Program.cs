using CallObjectFunction;
using Kwargs.Net;

TestClass testClass = new();
var result = Reflection.CallFunction(testClass, nameof(testClass.Test), new Dictionary<string, object> { { "a", 5 } });
Console.WriteLine(result);
