using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace FlowChart2.Utility
{
    class JSONUtil
    {
        public static string Serialize<T>(T o)
        {
            return new JavaScriptSerializer().Serialize(o);
        }

        public static T Deserialize<T>(string str)
        {
            return new JavaScriptSerializer().Deserialize<T>(str);
        }
    }
}
