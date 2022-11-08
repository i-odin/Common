using System.Linq.Expressions;
using System.Reflection;

namespace Common.Core.Tests;

public class TestAttr : Attribute
{
    public TestAttr() { }
    public TestAttr(int number)
    {
        Number = number;
    }

    public int Number { get; set; }
}

[TestAttr]
public enum TestEnum
{
    [TestAttr]
    One, 
    Two
}

[TestAttr]
public class TestClass
{
    [TestAttr(1)]
    public string? Name { get; set; }
}

public class TestType
{
    [Fact]
    public void test()
    {
        var attributes = Type<TestEnum>.Attributes/*.HasAttribute<TestEnum, TestAttr>(x=>x.)*/;
        
        var attributes1 = Type<TestClass>.HasAttribute(x=>x.Name, out TestAttr attr);

        var qwe = 0;
        if(qwe == 0)
            qwe = 1;
    }
}

/// <summary>
/// Type helper
/// </summary>
/// <typeparam name="T"></typeparam>
public static class Type<T>
{
    static Attribute[] _attributes;

    static Dictionary<string, PropertyInfo> _properties;

    static MethodInfo[] _methods;

    static ConstructorInfo[] _constructors;

    //private static ConcurrentDictionary<long, ObjectActivator<T>> _activators;

    static Type()
    {
        var type = typeof(T);
        _attributes = type.GetCustomAttributes().ToArray();

        _properties = type
            .GetProperties()
            .ToDictionary(x => x.Name, x => x);

        _methods = type
            .GetMethods()
            .Where(x => x.IsPublic && x.IsAbstract == false)
            .ToArray();

        _constructors = type.GetConstructors();
        //_activators = new ConcurrentDictionary<long, ObjectActivator<T>>();
    }

    public static Dictionary<string, PropertyInfo> PublicProperties => _properties;

    public static MethodInfo[] PublicMethods => _methods;

    public static Attribute[] Attributes => _attributes;
   
    #region HasAttribute
    public static bool HasAttribute<TAttr>()
        where TAttr : Attribute
        => GetAttribute<TAttr>() != null;

    public static bool HasAttribute<TAttr>(out TAttr attr)
       where TAttr : Attribute
    {
        var result = GetAttribute<TAttr>();
        attr = result;
        return result != null;
    }

    public static bool HasAttribute<TProp, TAttr>(Expression<Func<T, TProp>> prop)
        where TAttr : Attribute 
        => GetAttribute<TProp, TAttr>(prop) != null;

    public static bool HasAttribute<TProp, TAttr>(Expression<Func<T, TProp>> prop, out TAttr attr)
        where TAttr : Attribute
    {
        var result = GetAttribute<TProp, TAttr>(prop);
        attr = result;
        return result != null;
    }

    #endregion

    #region GetAttribute
    public static TAttr GetAttribute<TAttr>()
        where TAttr : Attribute
        => (TAttr)_attributes.FirstOrDefault(x => x.GetType() == typeof(TAttr));

    public static TAttr? GetAttribute<TProp, TAttr>(Expression<Func<T, TProp>> prop)
        where TAttr : Attribute 
        => (((MemberExpression)prop.Body).Member as PropertyInfo)?.GetCustomAttribute<TAttr>();
    #endregion
}

public static class MyTypeExtension
{

}