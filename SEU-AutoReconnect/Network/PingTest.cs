using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace SEU_AutoReconnect.Network
{
    class PingTest
    {
        /// <summary>
        /// 批量进行ping测试
        /// 成功次数>=失败次数的时候返回true，
        /// 否则返回false
        /// </summary>
        /// <returns></returns>
        public static bool batch_ping_test()
        {
            int success_ct=0,fail_ct=0;
            for (int i = 0; i < Config.CurrentConfig.ping_test_time; i++)
                foreach (var ip in Config.CurrentConfig.loacl_ping_address)
                    if (ping_test(ip))
                        success_ct++;
                    else
                        fail_ct++;
            return success_ct >= fail_ct;
        }
        public static bool ping_test(string address){
            //远程服务器IP  
            string ipStr = address;
            //构造Ping实例  
            Ping pingSender = new Ping();
            //Ping 选项设置  
            PingOptions options = new PingOptions();
            options.DontFragment = true;
            //测试数据  
            string data = "test";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            //设置超时时间  
            int timeout = 120;
            //调用同步 send 方法发送数据,将返回结果保存至PingReply实例  
            PingReply reply = pingSender.Send(ipStr, timeout, buffer, options);

            Console.WriteLine("ping: " + address);
            if (reply.Status == IPStatus.Success)
            {
                Console.WriteLine("答复的主机地址：" + reply.Address.ToString());
                Console.WriteLine("往返时间：" + reply.RoundtripTime);
                Console.WriteLine("生存时间（TTL）：" + reply.Options.Ttl);
                Console.WriteLine("是否控制数据包的分段：" + reply.Options.DontFragment);
                Console.WriteLine("缓冲区大小：" + reply.Buffer.Length);
                return true;
            }
            else
            {
                Console.WriteLine(reply.Status.ToString());
                return false;
            }
        }
    }
}
