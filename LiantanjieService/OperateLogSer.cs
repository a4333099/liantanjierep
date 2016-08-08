using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiantanjieModel;
using LiantanjieModel.CacheRedis;
using StackExchange.Redis;

namespace LiantanjieService
{
    public class OperateLogSer
    {
        public static  void  AddLoginFailTime(string  sid)
        {
           
            if (RedisBaseRep.KeyExists(sid))
            {
                var times = SessionCache.GetItem<int>(sid, "loginfailtime");
                times++;
                SessionCache.SetItem(sid, "loginfailtime",times,TimeSpan.FromMinutes(3));
            }
            else
            {
                SessionCache.SetItem(sid, "loginfailtime", 1, TimeSpan.FromMinutes(3));
            }
        }
    }
}
