using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEU_AutoReconnect.SerializeBase
{
    [Serializable]
    public abstract class SerializeBase<T>
    {
        public static string Serialize(T cls)
        {
            return Serialization.Serialize(cls);
        }

        public static T DeSerialize(string text)
        {
            return Serialization.Deserialize<T>(text);
        }
    }
}
