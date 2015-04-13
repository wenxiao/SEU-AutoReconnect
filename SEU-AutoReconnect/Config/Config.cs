using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SEU_AutoReconnect.SerializeBase;

namespace SEU_AutoReconnect
{
    [DataContract]
    class Config: SerializeBase<Config>
    {
        public static Config CurrentConfig=new Config();
        /// <summary>
        /// 本地连接名称
        /// </summary>
        [DataMember]
        public string lan_name = "本地连接";
        /// <summary>
        /// 测试的ping地址
        /// 成功次数>=失败次数认为网络正常
        /// </summary>
        [DataMember]
        public string[] loacl_ping_address = { "58.192.112.11", "bbs.seu.edu.cn", "www.seu.edu.cn" };
        /// <summary>
        /// 每个ping地址测试的次数
        /// </summary>
        [DataMember]
        public int ping_test_time = 2;

        internal static void Inital()
        {
            try
            {
                if (!File.Exists("config.json"))
                {
                    Console.WriteLine("No Config.Json File Found.");
                    Console.ReadLine();
                }
                CurrentConfig = Config.DeSerialize(
                    File.ReadAllText(
                        "config.json"
                    ));
            }
            catch(Exception e)
            {
                Console.WriteLine("[{0}] {1}", e.Message, e.StackTrace);
                Trace.WriteLine(e);
            }
        }
    }
}
