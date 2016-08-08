using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiantanjieModel;

namespace LiantanjieService
{
   public  class Sessions
    {
       public static void SetItem(string sid,string key,string value)
       {
           SessionCache.SetItem(sid,key,value);
       }

       public static T GetItem<T>(string sid, string key)
       {
          return  SessionCache.GetItem<T>(sid,key);
       }

       public static string GetString(string sid, string key)
       {
           return SessionCache.GetString(sid, key);
       }
    }
}
