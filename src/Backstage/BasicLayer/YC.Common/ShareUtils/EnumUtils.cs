using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace YC.Common
{
    public static class EnumUtils
    {
        /// <summary>
        /// 根据传入的int返回对应枚举属性名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="num">进制</param>
        /// <returns></returns>
        public static string GetEnumName<T>(int value)
        {
            string name = "";
            name = Enum.Parse(typeof(T), Enum.GetName(typeof(T), value)).ToString();
            return name;
        }

        /// <summary>
        /// 根据传入的枚举属性获得对应值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GetEnumValue<T>(string value)
        {
            Type type = typeof(T);
            var schoolId = Enum.Format(type, Enum.Parse(type, value.ToUpper()), "d");
            return Convert.ToInt32(schoolId);
        }

        public static int GetEnumTypeByIndex<T>(T typeName) where T : Enum
        {
            Type type = typeof(T);
            int value = (int)Enum.Parse(type, typeName.ToString());
            return value;
        }

        public static Dictionary<int, string> EnumToDictionary<T>()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            if (!typeof(T).IsEnum)
            {
                return dic;
            }
            string desc = string.Empty;
            foreach (var item in Enum.GetValues(typeof(T)))
            {
                var attrs = item.GetType().GetField(item.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                {
                    DescriptionAttribute descAttr = attrs[0] as DescriptionAttribute;
                    desc = descAttr.Description;
                }
                dic.Add(Convert.ToInt32(item), desc);
            }
            return dic;
        }

    }
}
