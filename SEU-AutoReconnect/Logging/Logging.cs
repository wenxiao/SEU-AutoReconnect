using SEU_AutoReconnect.SerializeBase;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEU_AutoReconnect
{
    public class Logging : TraceListener
    {
        public static void Initial(){
            Trace.Listeners.Clear();  //清除系统监听器 (就是输出到Console的那个)
            Trace.Listeners.Add(new Logging()); //添加MyTraceListener实例
        }

        public override void Write(string message)
        {
            Console.Write(message);
            File.AppendAllText(System.Windows.Forms.Application.StartupPath + "/logging.log", message);
        }

        public override void WriteLine(string message)
        {
            message = DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss] ") + message;
            Console.WriteLine(message);
            File.AppendAllText(System.Windows.Forms.Application.StartupPath + "/logging.log", message + Environment.NewLine);
        }

        public override void Write(object o, string category)
        {
            string msg = "";
            if (string.IsNullOrWhiteSpace(category) == false) //category参数不为空
            {
                msg = category + " : ";
            }

            if (o is Exception) //如果参数o是异常类,输出异常消息+堆栈,否则输出o.ToString()
            {
                var ex = (Exception)o;
                msg += ex.Message + Environment.NewLine;
                msg += ex.StackTrace;
            }
            else if (o != null)
            {
                msg = o.ToString() + " [" + Serialization.Serialize(o) + "]";
            }
            else
            {
                msg = o.GetType().ToString() + " is null!";
            }

            WriteLine(msg);
        }
    }
}
