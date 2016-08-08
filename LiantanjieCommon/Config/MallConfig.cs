using System;
using System.Xml.Linq;

namespace LiantanjieCommon.Config
{
     [Serializable]
    public class MallConfig
    {
    
        public  string EncryptKey { get; set; }
        public  string MallName { get; set; }
        public  string Randomlib { get; set; }

    }


}
