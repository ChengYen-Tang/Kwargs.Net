# Kwargs.Net
[![NuGet](https://img.shields.io/nuget/v/Kwargs.Net)](https://www.nuget.org/packages/Kwargs.Net/#readme-body-tab)<br/>
This library allows dotnet developers to use **kwargs (Keyword Arguments) to create instance or call function like python developers

## Example
### CreateInstance
```
using CreateInstance;
using Kwargs.Net;

Dictionary<string, object> kwargs = new()
{
    { "v1", 5 },
    { "s1", "123" },
};
TestClass testClass = Reflection.CreateInstance<TestClass>(Array.Empty<object>(), kwargs)!;
Console.WriteLine($"V1: {testClass.V1}, S1: {testClass.S1}, V2: {testClass.V2}, S2: {testClass.S2}, F1: {testClass.F1}");
// V1: 5, S1: 123, V2: , S2: , F1:

kwargs = new()
{
    { "v2", 7 },
};
testClass = Reflection.CreateInstance<TestClass>(new object[] { 5, "123" }, kwargs)!;
Console.WriteLine($"V1: {testClass.V1}, S1: {testClass.S1}, V2: {testClass.V2}, S2: {testClass.S2}, F1: {testClass.F1}");
// V1: 5, S1: 123, V2: 7, S2: , F1:

kwargs = new()
{
    { "f1", 5f },
    { "s1", "123" },
};
testClass = Reflection.CreateInstance<TestClass>(new object[] { 5 }, kwargs)!;
Console.WriteLine($"V1: {testClass.V1}, S1: {testClass.S1}, V2: {testClass.V2}, S2: {testClass.S2}, F1: {testClass.F1}");
// V1: 5, S1: 123, V2: , S2: , F1: 5
```
```
public class TestClass
{
    public int? V1 { get; set; }
    public string? S1 { get; set; }
    public int? V2 { get; set; }
    public string? S2 { get; set; }
    public float? F1 { get; set; }

    public TestClass(int v1, string s1, int v2 = 5, string? s2 = null)
    {
        V1 = v1;
        S1 = s1;
        V2 = v2;
        S2 = s2;
    }

    public TestClass(int v1, string s1)
    {
        V1 = v1;
        S1 = s1;
    }

    public TestClass(int v1, string s1, int? v2 = null, float? f1 = 17)
    {
        V1 = v1;
        S1 = s1;
        V2 = v2;
        F1 = f1;
    }
}
```
### CallStaticFunction
```
using CallStaticFunction;
using Kwargs.Net;

var result = Reflection.CallFunction(typeof(TestClass), nameof(TestClass.Test), Array.Empty<object>(), new Dictionary<string, object>());
Console.WriteLine(result);
// 123
```
```
public class TestClass
{
    public static void Test() { Console.WriteLine("123"); }
    public int Test(int a) { Console.WriteLine(a); return a + 1; }
}
```
### CallObjectFunction
```
using CallObjectFunction;
using Kwargs.Net;

TestClass testClass = new();
var result = Reflection.CallFunction(testClass, nameof(testClass.Test), Array.Empty<object>(), new Dictionary<string, object> { { "a", 5 } });
Console.WriteLine(result);
// 5
// 6
```
```
public class TestClass
{
    public static void Test() { Console.WriteLine("123"); }
    public int Test(int a) { Console.WriteLine(a); return a + 1; }
}
```
