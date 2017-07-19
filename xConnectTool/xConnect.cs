using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xConnectTool
{
    public class xConnect
    {
        static xConnect()
        {
            xConnect.Initialize();
        }
        static void Initialize()
        {
            xconnectUrl = "Test";
        }

        private static string xconnectUrl;
        public static string xConnectUrl
        {
            get
            {
                return xconnectUrl;
            }
            set
            {
                xconnectUrl = value;
            }
        }
    }
}
