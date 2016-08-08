using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiantanjieCommon
{
    public class ConfigPath
    {
        private static readonly string _mallconfigpath = "/App_Data/MallConfig.xml";


        public static string MallConfigPath
        {
            get { return GetPath(_mallconfigpath); }
        }


        private static string GetPath(string filename)
        {
            if (System.Web.HttpContext.Current != null)
            {
                return System.Web.HttpContext.Current.Server.MapPath(filename);
            }
            return string.Empty;

        }
    }
}
