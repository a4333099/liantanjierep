using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Liantanjie.Models;
using LiantanjieCommon;
using LiantanjieCommon.Config;
using LiantanjieCommon.Helper;
using LiantanjieModel;
using LiantanjieService;

namespace Liantanjie.Controllers
{
    public class AccountController : BaseController
    {
        //
        // GET: /Account/

        [HttpGet]
        public ActionResult Login()
        {
            string returnUrl = WebHelper.GetQueryString("returnUrl");
            if (returnUrl.Length == 0)
                returnUrl = "/";
            LoginModel model = new LoginModel()
            {
                ReturnUrl = returnUrl

            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {

            if (ModelState.IsValid)
            {
                var account = WebHelper.GetFormString("Account");
                var password = WebHelper.GetFormString("Password");
                if (!UserInfoSer.GetTotalAccount().Contains(account))
                {
                    MallWebContext.PromptLst.Add("登录失败，账号不存在");
                }
                else if (!UserInfoSer.CheckLogin(account, password))
                {
                    MallWebContext.PromptLst.Add("登录失败，提供的账户名或者密码不正确");
                }
                if (MallWebContext.PromptLst.Count > 0)
                {
                    OperateLogSer.AddLoginFailTime(MallWebContext.Sid);
                    loginModel.IsVerifyCode = true;
                    return View(loginModel);
                }
                else
                {
                    Utils.SetCookie("uid", UserInfoSer.GetUidByAccount(account));
                    var user = UserInfoSer.GetUserinfoByAccountandPwd(account, password);
                    Utils.SetPasswordCookie(user.Password, TotalConfigs.MallConfig.EncryptKey);
                    return Redirect(loginModel.ReturnUrl);
                    
                }
            }
            return View(loginModel);
        }



        [HttpGet]
        public ActionResult Register()
        {
            string returnUrl = WebHelper.GetQueryString("returnUrl");
            if (returnUrl.Length == 0)
                returnUrl = "/";
            RegModel regmodel = new RegModel
            {
                ReturnUrl = returnUrl,
                IsVerifyCode = true
            };
            return View(regmodel);

        }


        [HttpPost]
        public ActionResult Register(RegModel model)
        {
            if (ModelState.IsValid)
            {
                var account = WebHelper.GetFormString("Account");
                string password = WebHelper.GetFormString("password");
                string confirmPwd = WebHelper.GetFormString("confirmPwd");
                string verifyCode = WebHelper.GetFormString("verifyCode");
                string sid = Utils.GetSidCookie();
                var varifycd = Sessions.GetString(sid, "verifyCode");
                #region 验证
                //账号验证
                if (string.IsNullOrEmpty(account))
                {
                    MallWebContext.PromptLst.Add("提供的账户名不能为空");
                  //  ModelState.AddModelError("Account", "提供的账户名不能为空");
                }
                else if (account.Length < 4 || account.Length > 20)
                {

                    MallWebContext.PromptLst.Add("账号必须大于4位且小于20位");
                  //  ModelState.AddModelError("Account", "账号必须大于4位且小于20位");
                }
                else if (account.Contains(" "))
                {
                    MallWebContext.PromptLst.Add("账号不能包含空格");

                    //ModelState.AddModelError("Account", "账号不能包含空格");
                }
                else if (account.Contains(":"))
                {

                    MallWebContext.PromptLst.Add("账号不能包含冒号");
                    //ModelState.AddModelError("Account", "账号不能包含冒号");
                }
                else if (account.Contains("<"))
                {
                    MallWebContext.PromptLst.Add("账号不能包含特殊字符'<'");

                  //  ModelState.AddModelError("Account", "账号不能包含特殊字符'<'");
                }
                else if (account.Contains(">"))
                {
                    MallWebContext.PromptLst.Add("账号不能包含特殊字符'>'");

                   // ModelState.AddModelError("Account", "账号不能包含特殊字符'>'");
                }
                else if ((!SecureHelper.IsSafeSqlString(account, false)))
                {
                    MallWebContext.PromptLst.Add("账号不符合系统要求");
                   // ModelState.AddModelError("Account", "账号不符合系统要求");
                }
                else if (UserInfoSer.GetTotalAccount().Contains(account))
                {
                    MallWebContext.PromptLst.Add("该账号已经被注册");
                   // ModelState.AddModelError("Account", "该账号已经被注册");
                }

               else  if (varifycd != verifyCode)
                {
                    MallWebContext.PromptLst.Add("验证码不正确");
                   // ModelState.AddModelError("verifyCode", "验证码不正确");
                }
                #endregion


                if (MallWebContext.PromptLst.Count>0)
                {
                    model.IsVerifyCode = true;
                    return View(model);
                }
                else
                {
                    UserInfo user = new UserInfo { Salt = Randoms.CreateRandomValue(6, true), Account = account };
                    user.Password = SecureHelper.MD5(password + user.Salt);
                    user.UId = UserInfoSer.GenerateUid();
                    user.NickName=  user.NickName ?? user.Account;
                    UserInfoSer.CreateUserinfointo(user);
                    return null;
                }

            }
            else
            {
                return View(model);
            }

        }

        public ActionResult Logout()
        {
            string returnUrl = WebHelper.GetQueryString("returnUrl");
            if (returnUrl.Length == 0)
                returnUrl = "/";
            Utils.SetCookie("uid", -1);
            Utils.SetCookie("password", "");
            return Redirect(returnUrl);
        }




    }
}
