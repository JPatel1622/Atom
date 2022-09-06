using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel;

namespace Atom.Domain.Extension
{
    public static class GenericExtension
    {
        public static string Description<T>(this T t)
        {
            var field = t.GetType().GetField(t.ToString());
            DescriptionAttribute description = (DescriptionAttribute)field.GetCustomAttributes(typeof(DescriptionAttribute), false)[0];
            if (description == null)
            {
                return string.Empty;
            } else
            {
                return description.Description;
            }
        }

    }
}
