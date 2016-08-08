using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LiantanjieCommon;
using LiantanjieCommon.Config;
using LiantanjieCommon.Helper;
using LiantanjieModel;
using LiantanjieService;

namespace Liantanjie.Models
{
    public class MallWebContext
    {
        public MallWebContext()
        {
           // UpdateContext();
            PromptLst = new List<string>();
        }
        public string Sid { get; set; }
        public int Uid { get; set; }
        public string NickName { get; set; }

        public List<String> PromptLst { get; set; }
        public void UpdateContext()
        {

            this.Uid = int.Parse(WebHelper.GetCookie("uid", -1));
            this.Sid = WebHelper.GetCookie("sid") ?? Utils.GenerateSid();
            WebHelper.SetCookie("sid",Sid,30);
            UserInfo userInfo = null;
            if (Uid < 1)
            {
                userInfo = UserInfoSer.CreatePartGuest();
            }
            else
            {
                string encryptPwd = Utils.GetCookiePassword();

                if (encryptPwd.Length == 0 || !SecureHelper.IsBase64String(encryptPwd))
                {
                    //创建游客
                    userInfo = UserInfoSer.CreatePartGuest();
                    Utils.SetCookie("uid", -1);
                    Utils.SetCookie("password", "");
                }
                else
                {

                    userInfo = UserInfoSer.GetUserInfoByUidandPasswd(Sid,Uid, SecureHelper.AESDecrypt(encryptPwd,TotalConfigs. MallConfig.EncryptKey));
                }
            }
            if (userInfo != null)
            {
                this.NickName = userInfo.NickName;
            }

        }
    }
}
