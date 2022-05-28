using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace FISCOBCOS.CSharpSdk.Utis
{
    public static class JsonUtils
    {
        public static object ToJson(this string Json)
        {
            return Json == null ? null : JsonConvert.DeserializeObject(Json);
        }
        public static string ToJson(this object obj)
        {
            var timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
            return JsonConvert.SerializeObject(obj, timeConverter);
        }
        public static string ToJson(this object obj, string datetimeformats)
        {
            var timeConverter = new IsoDateTimeConverter { DateTimeFormat = datetimeformats };
            return JsonConvert.SerializeObject(obj, timeConverter);
        }
        public static T ToObject<T>(this string Json)
        {
            return Json == null ? default(T) : JsonConvert.DeserializeObject<T>(Json);
        }
        public static List<T> ToList<T>(this string Json)
        {
            return Json == null ? null : JsonConvert.DeserializeObject<List<T>>(Json);
        }
        public static DataTable ToTable(this string Json)
        {
            return Json == null ? null : JsonConvert.DeserializeObject<DataTable>(Json);
        }
        public static JObject ToJObject(this string Json)
        {
            return Json == null ? JObject.Parse("{}") : JObject.Parse(Json.Replace("&nbsp;", ""));
        }

        /// <summary>
        /// 通过Json数据中的key获取对应的Value 重名的获取第一个
        /// </summary>
        /// <typeparam name="T">所获取数据的数据类型</typeparam>
        /// <param name="jObject">JObject对象</param>
        /// <param name="key">key</param>
        /// <returns>key对应的Value  没有找到时返回null</returns>
        public static T GetValueByKey<T>(this JObject jObject, string key)
        {
            var tempValue = jObject.GetValue(key);
            if (tempValue != null)
            {
                return tempValue.Value<T>();
            }
            else
            {
                var enumerator = jObject.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    if (enumerator.Current.Value.HasValues)
                    {
                        T tempTValue = GetValueByKey<T>(enumerator.Current.Value as JObject, key);
                        if (tempTValue != null)
                        {
                            return tempTValue;
                        }
                    }
                }
            }

            return default(T);
        }

    }
}
