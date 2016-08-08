using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiantanjieCommon.Helper;

namespace LiantanjieCommon.Config
{
   public  class TotalConfigs
   {

       private static  MallConfig _mallConfig;

       static  TotalConfigs()
       {
           _mallConfig = (MallConfig)IOHelper.DeserializeFromXML(typeof(MallConfig), ConfigPath.MallConfigPath);
       }

       public static  MallConfig MallConfig {
           get { return _mallConfig; }
       }
    }
}
