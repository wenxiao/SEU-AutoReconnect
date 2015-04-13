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
        string login;
        [DataMember]
        string login_username;
        [DataMember]
        int login_time;
        [DataMember]
        string login_expire;
        [DataMember]
        int login_remain;
        [DataMember]
        string login_ip;
        [DataMember]
        string login_location;
    }
}
