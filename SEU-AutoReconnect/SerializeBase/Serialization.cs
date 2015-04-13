using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace SEU_AutoReconnect.SerializeBase
{
    /// <summary>
    /// 使用Json进行序列号与反序列化
    /// </summary>
    public static class Serialization
    {
        /// <summary>
        /// 将Json串解析为类T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string jsonString)
        {
            try
            {
                using(var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
                {
                    return (T)new DataContractJsonSerializer(typeof(T)).ReadObject(ms);
                }
            }
            catch(Exception e)
            {
                Trace.Write(e, "反序列化错误");
                Trace.Write(jsonString);
                return default(T);
            }
        }

        /// <summary>
        /// 将类序列化为Json
        /// </summary>
        /// <param name="o">需要序列化的对象</param>
        /// <returns>序列化后的字符串</returns>
        public static string Serialize(Object o)
        {
            DataContractJsonSerializer js = new DataContractJsonSerializer(o.GetType());
            MemoryStream ms = new MemoryStream();
            js.WriteObject(ms, o);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            return sr.ReadToEnd();
        }
    }
}
