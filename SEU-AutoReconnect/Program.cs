using SEU_AutoReconnect.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SEU_AutoReconnect
{
    class Program
    {
        static void Main(string[] args)
        {
            //File.WriteAllText("Config.json.sample",Config.Serialize(Config.CurrentConfig));

            Config.Inital();

            while (true)
            {
                Console.Clear();
                if (!PingTest.batch_ping_test())
                {
                    NetManager.setLanEnable(false, Config.CurrentConfig.lan_name);
                    NetManager.setLanEnable(true, Config.CurrentConfig.lan_name);
                }
                Thread.Sleep(1000*120);
            }
        }
    }
}
