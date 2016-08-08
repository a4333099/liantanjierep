using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiantanjieModel.CacheRedis;
using Newtonsoft.Json;

namespace LiantanjieModel
{
    public class SessionCache
    {

        public static void SetItem(string sid, string key, Object value, TimeSpan? expiry = default(TimeSpan?))
        {
            Hashtable ht = new Hashtable();
            if (RedisBaseRep.KeyExists(sid))
            {
                ht = RedisBaseRep.GetObject<Hashtable>(sid);
                RedisBaseRep.KeyDelete(sid);
            }

            if (ht.ContainsKey(key))
            {
                ht[key] = value;
            }
            else
            {
                ht.Add(key, value);
            }

            RedisBaseRep.SetObject(sid, ht,expiry);

        }

        public static T GetItem<T>(string sid, string key)
        {
            if (!RedisBaseRep.KeyExists(sid)) return default(T);
            var ht = RedisBaseRep.GetObject<Hashtable>(sid);
            if (ht != null && ht.ContainsKey(key))
            {
                return JsonConvert.DeserializeObject<T>(ht[key].ToString());
            }
            return default(T);
        }

        public static string GetString(string sid, string key)
        {
            if (!RedisBaseRep.KeyExists(sid)) return string.Empty;
            var ht = RedisBaseRep.GetObject<Hashtable>(sid);
            if (ht != null && ht.ContainsKey(key))
            {
                return ht[key].ToString();
            }
            return string.Empty; ;
        }
    }
}
