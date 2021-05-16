/*******************************************************************************
 * Copyright © 2017 Xmiot 版权所有
 * Author: Xmiot
 * 
 * 
*********************************************************************************/
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Data;

namespace YC.Common.ShareUtils
{
    public static class JsonUtils
    {
        public static object ToJson(this string Json)
        {
            return Json == null ? null : JsonConvert.DeserializeObject(Json);
        }


        /// <summary>
        /// 如果是object类型就没法tojson  请不要用这个方法，会异常，
        /// 直接去ToString() ,System.Text.Json 的实例化，会出现无法转换某些内容，导致freesql等组件受到影响
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            //参数是object 类 会报错出现异常，无法转换object 类型的数据
            var timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
            return JsonConvert.SerializeObject(obj, timeConverter);
            //return System.Text.Json.JsonSerializer.Serialize(obj);
           
        }

        public static string ToIndentedJson(this object obj)
        {
            var timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
            return JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented);
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
        /// <summary>
        /// 通过json 中指定的key 找到对应的对象，进行实体转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Json"></param>
        /// <param name="key"></param>
        /// <returns></returns>

        public static T GetOjectByJsonKey<T>(this string Json, string key)
        {
            var result = Json.ToJObject().GetValue(key).ToObject<T>();
            return result;
        }

        /// <summary>
        /// 获取指定的json对象
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T GetJsonList<T>(string json) where T : class
        {
            T tempJsonData = json.ToObject<T>();
            return tempJsonData;
        }

        /// <summary>
        /// 获取指定的json字符串
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetConfigJson(string path)
        {
            string jsonfile = path;

            using (System.IO.StreamReader file = System.IO.File.OpenText(jsonfile))
            {
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    var o = JToken.ReadFrom(reader);
                    return o.ToString();
                }
            }

        }

        /// <summary>
        /// 这个方法放在这里不合适，耦合了，废弃
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        //public static bool SetConfigJson(string path, string content)
        //{
        //    string error = "";
        //    bool result = FileUtils.CoverWriteFile(path, content, out error);
        //    return result;
        //}
    }
}
