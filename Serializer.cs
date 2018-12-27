using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MI.CloudPlatform.Util
{
    /// <summary>
    /// 对象序列化工具类
    /// </summary>
    public class Serializer
    {
        #region 将对象序列化成josn字符串
        /// <summary>
        /// 将对象序列化成josn字符串
        /// </summary>
        /// <typeparam name="T">要序列化的类</typeparam>
        /// <param name="t">要序列化的对象</param>
        /// <returns></returns>
        public static string SerializersJson<T>(T t)
        {
            //将model序列化成字符串
            var enumConverter = new Newtonsoft.Json.Converters.StringEnumConverter();
            var settings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            settings.Converters.Add(enumConverter);
            string requestStr = JsonConvert.SerializeObject(t, settings);

            return requestStr;
        }
        #endregion

        #region 将josn字符串反序列化成对象
        /// <summary>
        /// 将josn字符串反序列化成对象
        /// </summary>
        /// <typeparam name="T">要序列化的类</typeparam>
        /// <param name="t">要序列化的对象</param>
        /// <returns></returns>
        public static T DSerializersJson<T>(string josn)
        {
            try
            {
                var obj = JsonConvert.DeserializeObject<T>(josn);
                return obj;
            }
            catch
            {
                return default(T);
            }
        }
        #endregion

        public static T DeserializeAnonymousType<T>(string value, T anonymousTypeObject)
        {
            return JsonConvert.DeserializeAnonymousType(value, anonymousTypeObject);
        }
    }
}
