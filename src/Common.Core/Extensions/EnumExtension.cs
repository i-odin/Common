using System;

namespace Common.Core.Extensions
{
    public static class EnumExtension
    {
        public static bool HasAttribute(this Enum @enum, Type typeAttribute)
        {
            var result = false;
            if (typeAttribute?.BaseType == typeof(Attribute))
            {
                var field = @enum.GetType().GetField(@enum.ToString());
                if (field != null)
                    result = field.IsDefined(typeAttribute, false);
            }
            return result;
        }
    }
}