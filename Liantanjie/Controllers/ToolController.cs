using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Liantanjie.Results;
using LiantanjieCommon;
using LiantanjieService;

namespace Liantanjie.Controllers
{
    public class ToolController : Controller
    {
        /// <summary>
        /// 验证图片
        /// </summary>
        /// <param name="width">图片宽度</param>
        /// <param name="height">图片高度</param>
        /// <returns></returns>
        public ImageResult VerifyImage(int width = 56, int height = 20)
        {
            Utils.SetSid();
            var sid = Utils.GetSidCookie();
            //生成验证值
            string verifyValue = RandomSer.CreateRandomValue(4, false).ToLower();
            //生成验证图片
            RandomImage verifyImage = RandomSer.CreateRandomImage(verifyValue, width, height, Color.White, Color.Blue, Color.DarkRed);
            //将验证值保存到session中
            Sessions.SetItem(sid, "verifyCode", verifyValue);

            //输出验证图片
            return new ImageResult(verifyImage.Image, verifyImage.ContentType);
        }
    }
}
