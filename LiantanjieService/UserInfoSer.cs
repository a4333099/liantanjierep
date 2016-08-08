using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiantanjieCommon;
using LiantanjieCommon.Helper;
using LiantanjieModel;
using LiantanjieModel.CacheRedis;
using LiantanjieModel.NOSQLDB;

namespace LiantanjieService
{
    public class UserInfoSer
    {


        //==============================================================================================
        #region Db
        /// <summary>
        /// 获取用户信息(NOSQL)
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="mdpwd">加密密码</param>
        /// <returns></returns>
        public static UserInfo GetUserInfoByUidandPwd(int uid, string mdpwd)
        {
            var userinfo = BaseMongoDbRep<UserInfo>.GetEntity(u => u.UId == uid);
            if (userinfo != null && userinfo.Password == mdpwd)
            {
                return userinfo;
            }
            return null;
        }
        /// <summary>
        /// 创建用户(NOSQL)
        /// </summary>
        /// <param name="user"></param>
        public static void CreateUserinfointo(UserInfo user)
        {
            BaseMongoDbRep<UserInfo>.Insert(user);
        }
        /// <summary>
        /// 获取UID(NOSQL)
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public static string GetUidByAccount(string account)
        {
            var userinfo = BaseMongoDbRep<UserInfo>.GetEntity(u => u.Mobile == account || u.Email == account || u.Account == account);
            if (userinfo != null)
                return userinfo.UId.ToString();
            return null;
        }
        /// <summary>
        /// 获取账号信息(NOSQL)
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pwd">明文密码</param>
        /// <returns></returns>
        public static UserInfo GetUserinfoByAccountandPwd(string account, string pwd)
        {
            var userinfo = BaseMongoDbRep<UserInfo>.GetEntity(u => u.Account == account);
            if (userinfo != null && userinfo.Password == SecureHelper.MD5(pwd + userinfo.Salt))
            {
                return userinfo;
            }
            return null;
        }

        /// <summary>
        /// 校验登录(NOSQL)
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pwd">明文密码</param>
        /// <returns></returns>
        public static bool CheckLogin(string account, string pwd)
        {
            var userinfo = BaseMongoDbRep<UserInfo>.GetEntity(u=> u.Account == account);
            if (userinfo != null && userinfo.Password == SecureHelper.MD5(pwd + userinfo.Salt))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取账号列表(NOSQL)
        /// </summary>
        /// <returns></returns>
        public static List<string> GetTotalAccount()
        {
            var accs = BaseMongoDbRep<UserInfo>.GetEntities(u => true);
           return  accs.Select(u=>u.Account).ToList();
        }

       /// <summary>
        /// 获取UID列表(NOSQL)
       /// </summary>
       /// <returns></returns>
        public static List<int> GetTotalUid()
        {
            var accs = BaseMongoDbRep<UserInfo>.GetEntities(u => true);
            return accs.Select(u => u.UId).ToList();
        }

        /// <summary>
        /// 生成UID(NOSQL)
        /// </summary>
        /// <returns></returns>
        public static int GenerateUid()
        {
            int uid=0;
            //do
            //{
            //    uid = int.Parse(Randoms.CreateRandomValueWithoutzero(6, true));
            //}
            //while (!GetTotalUid().Contains(uid));

            Func<int> func = null;

            func = () =>
            {
                uid = int.Parse(Randoms.CreateRandomValueWithoutzero(6, true));
                if (!GetTotalUid().Contains(uid))
                {
                    return uid;
                }
                else
                {
                    func();
                }
                return 0;
            };

            uid = func();
            return uid;
        }

        #endregion


        #region Cache
        /// <summary>
        /// 通过sID获取userinfo(缓存)
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static UserInfo GetUserInfofromCache(string sid)
        {
            var user = SessionCache.GetItem<UserInfo>(sid, "userinfo");
            return user;
        }


        /// <summary>
        /// 通过sID获取userinfo(缓存)
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="uid"></param>
        /// <param name="pwd">明文密码</param>
        /// <returns></returns>
        public static UserInfo GetUserInfofromCache(string sid, string uid, string pwd)
        {
            var user = SessionCache.GetItem<UserInfo>(sid, "userinfo");
            if (user != null && user.UId.ToString() == uid && user.Password == pwd)
            {
                return user;
            }
            return null;
        }


        /// <summary>
        /// 保存userinfo到Session缓存中(缓存)
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="user"></param>
        public static void SaveUserInfo(string sid, UserInfo user)
        {
            SessionCache.SetItem(sid, "userinfo", user,TimeSpan.FromMinutes(30));
        }

        #endregion



        //==============================================================================================
        #region Other
        /// <summary>
        /// 创建游客
        /// </summary>
        /// <returns></returns>
        public static UserInfo CreatePartGuest()
        {
            return new UserInfo
            {
                UId = -1,
                Account = "guest",
                Email = "",
                Mobile = "",
                Password = "",
                UserRid = 6,
                StoreId = 0,
                MallAGid = 1,
                NickName = "游客",
                Avatar = "",
                PayCredits = 0,
                RankCredits = 0,
                VerifyEmail = 0,
                VerifyMobile = 0,
                LiftBanTime = new DateTime(1900, 1, 1),
                Salt = ""
            };
        }
        #endregion


        #region Total
        /// <summary>
        /// 汇总获取USERINFO
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="uid"></param>
        /// <param name="mdpwd"></param>
        /// <returns></returns>
        public static UserInfo GetUserInfoByUidandPasswd(string sid, int uid, string mdpwd)
        {
            UserInfo userinfo;
            userinfo = GetUserInfofromCache(sid, uid.ToString(), mdpwd);
            if (userinfo != null) return userinfo;
            userinfo = GetUserInfoByUidandPwd(uid, mdpwd);
            if (userinfo != null)
            {
                SaveUserInfo(sid, userinfo);
                return userinfo;
            }
            return null;
        }


        #endregion

    }
}
