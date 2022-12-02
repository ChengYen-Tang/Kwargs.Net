using CreateInstance;
using Kwargs.Net;

Dictionary<string, object> kwargs = new()
{
    { "v1", 5 },
    { "s1", "123" },
};
TestClass testClass = Reflection.CreateInstance<TestClass>(Array.Empty<object>(), kwargs)!;
Console.WriteLine($"V1: {testClass.V1}, S1: {testClass.S1}, V2: {testClass.V2}, S2: {testClass.S2}, F1: {testClass.F1}");

kwargs = new()
{
    { "v2", 7 },
};
testClass = Reflection.CreateInstance<TestClass>(new object[] { 5, "123" }, kwargs)!;
Console.WriteLine($"V1: {testClass.V1}, S1: {testClass.S1}, V2: {testClass.V2}, S2: {testClass.S2}, F1: {testClass.F1}");

kwargs = new()
{
    { "f1", 5f },
    { "s1", "123" },
};
testClass = Reflection.CreateInstance<TestClass>(new object[] { 5 }, kwargs)!;
Console.WriteLine($"V1: {testClass.V1}, S1: {testClass.S1}, V2: {testClass.V2}, S2: {testClass.S2}, F1: {testClass.F1}");
