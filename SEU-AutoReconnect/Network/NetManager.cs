using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEU_AutoReconnect.Network
{
    /// <summary>
    /// This work is done with the help of Mitchell Chu's Blog 
    /// http://blog.useasp.net/archive/2013/06/17/the-method-to-disable-or-enable-network-connection-on-windows-with-dot-net.aspx
    /// </summary>
    class NetManager
    {
        /// <summary>
        /// 设置一个本地连接是否启用
        /// </summary>
        /// <param name="enable"></param>
        /// <param name="lan_name"></param>
        public static void setLanEnable(
            bool enable = true,
            string lan_name = "本地连接")
        {
            using (Process process = new Process())
            {
                string netshCmd = "interface set interface name=\"{0}\" admin={1}";
                process.EnableRaisingEvents = false;
                process.StartInfo.Arguments = String.Format(netshCmd, lan_name, enable ? "ENABLED" : "DISABLED");
                process.StartInfo.FileName = "netsh.exe";
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.ErrorDialog = false;
                process.StartInfo.RedirectStandardError = false;
                process.StartInfo.RedirectStandardInput = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.Start();
                string rtn = process.StandardOutput.ReadToEnd();
                if (rtn.Trim().Length == 0)
                {
                    Console.WriteLine("设置成功，连接" + lan_name + "已经被设置为" + enable.ToString());
                }
                else
                {
                    Console.WriteLine("设置成功，连接" + lan_name + "为" + enable.ToString() + "失败" + rtn.Trim());
                }
            }
        }
        /// <summary>
        /// 检查一个本地连接状态
        /// </summary>
        /// <param name="lan_name"></param>
        /// <returns></returns>
        public static bool checkLanEnable(string lan_name)
        {
            using (Process process = new Process())
            {
                string netshCmd = "interface show interface";
                process.EnableRaisingEvents = false;
                process.StartInfo.Arguments = netshCmd;
                process.StartInfo.FileName = "netsh.exe";
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.ErrorDialog = false;
                process.StartInfo.RedirectStandardError = false;
                process.StartInfo.RedirectStandardInput = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.Start();
                string rtn = process.StandardOutput.ReadToEnd();

                foreach (string line in rtn.Split('\n'))
                {
                    if (!line.EndsWith(lan_name))
                        continue;
                    if (line.StartsWith("已启用"))
                        return true;
                    if (line.StartsWith("已禁用"))
                        return false;
                    throw new NotSupportedException("当前语言版本的netsh或操作系统尚未被支持\n Current language version of netsh or Operating System is not supported");
                }
                throw new ArgumentOutOfRangeException("不能找到下面的本地连接: " + lan_name);
            }
        }
    }
}
