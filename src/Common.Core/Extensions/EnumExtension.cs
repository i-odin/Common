using System.Diagnostics.CodeAnalysis;

namespace Common.Core.Extensions;

public static class EnumExtension
{
    public static bool HasAttribute<TAttr>(this Enum @enum) 
        where TAttr : Attribute
    {
        var result = false;
        var field = @enum.GetType().GetField(@enum.ToString());
        if (field != null)
            result = field.IsDefined(typeof(TAttr), false);
        
        return result;
    }

    [return: MaybeNull]
    public static TAttr? GetAttribute<TAttr>(this Enum @enum)
        where TAttr : Attribute
    {    
        var field = @enum.GetType().GetField(@enum.ToString());
        if (field != null)
        {
            var attributes = field.GetCustomAttributes(typeof(TAttr), false);
            if(attributes != null && attributes.Length > 0)
                return (TAttr)attributes[0];
        }
        return default;
    }
}