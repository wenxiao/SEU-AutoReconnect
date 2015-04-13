using SEU_AutoReconnect.SerializeBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SEU_AutoReconnect.Network.Wlan
{
    [DataContract]
    class WlanLoginData : SerializeBase<WlanLoginData>
    {
        [DataMember]
        public string login;
        [DataMember]
        public string login_username;
        [DataMember]
        public int login_time;
        [DataMember]
        public string login_expire;
        [DataMember]
        public int login_remain;
        [DataMember]
        public string login_ip;
        [DataMember]
        public string login_location;
    }
}
