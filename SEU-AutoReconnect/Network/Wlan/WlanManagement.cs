using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SEU_AutoReconnect.Network.Wlan
{
    class WlanManagement
    {
        public static bool CheckIfLogined()
        {
            WebRequest request = WebRequest.Create("https://w.seu.edu.cn/portal/init.php");
            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string s = reader.ReadToEnd();
            WlanLoginData t = WlanLoginData.DeSerialize(s);

            return t != null;
        }
        public static void Login()
        {
            WebRequest request = WebRequest.Create("https://w.seu.edu.cn/portal/login.php");

            //Post请求方式
            request.Method = "POST";
            // 内容类型
            request.ContentType = "application/x-www-form-urlencoded";
            //将URL编码后的字符串转化为字节
            byte[] payload = System.Text.Encoding.UTF8.GetBytes("username=220131453&password=172216");
            //设置请求的 ContentLength 
            request.ContentLength = payload.Length;
            //获得请 求流
            System.IO.Stream writer = request.GetRequestStream();
            //将请求参数写入流
            writer.Write(payload, 0, payload.Length);
            // 关闭请求流
            writer.Close();

            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string s = reader.ReadToEnd();
            Console.WriteLine("ResponseData: " + s);
        }
        public static void LoginIfOffline()
        {
            if (!CheckIfLogined())
                Login();
        }
    }
}
