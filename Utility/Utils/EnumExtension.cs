using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Utils
{
    public static class EnumExtension
    {
        public static string? GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attr = field?.GetCustomAttribute<DescriptionAttribute>();
            return attr?.Description;
        }
    }
}
