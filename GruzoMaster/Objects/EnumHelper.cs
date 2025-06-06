using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GruzoMaster.Objects
{
    public static class EnumHelper
    {
        public static List<KeyValuePair<Enum, string>> GetEnumDescriptionList<T>() where T : Enum
        {
            var type = typeof(T);
            var values = Enum.GetValues(type).Cast<Enum>();

            return values.Select(v =>
            {
                var descAttr = v.GetType()
                    .GetField(v.ToString())
                    ?.GetCustomAttribute<DescriptionAttribute>();

                return new KeyValuePair<Enum, string>(v, descAttr?.Description ?? v.ToString());
            }).ToList();
        }
    }
}
