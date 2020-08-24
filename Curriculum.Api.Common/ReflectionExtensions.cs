using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Curriculum.Api.Common
{
    public static class ReflectionExtensions
    {
        public static string GetDisplay(this object obj, string propertyName)
        {
            var property = obj.GetType().GetProperty(propertyName);

            var displayAttribute = property?.GetCustomAttributes(typeof(DisplayAttribute), true)
                                           ?.FirstOrDefault() as DisplayAttribute;

            if (displayAttribute == null || string.IsNullOrEmpty(displayAttribute.Name))
                return propertyName;

            return displayAttribute.Name;
        }

        public static string GetDisplay(this object obj)
        {
            return GetDisplay(obj.GetType());
        }

        public static string GetDisplay(this Type type)
        {
            var displayAttribute = type.GetCustomAttributes(typeof(DisplayAttribute), true)
                          ?.FirstOrDefault() as DisplayAttribute;

            if (displayAttribute == null || string.IsNullOrEmpty(displayAttribute.Name))
                return type.Name;

            return displayAttribute.Name;
        }

        public static void UpdateInstance<T>(this T current, T newer)
            where T : class
        {
            foreach (var property in typeof(T).GetProperties())
            {
                property.SetValue(current, property.GetValue(newer));
            }
        }
    }
}
