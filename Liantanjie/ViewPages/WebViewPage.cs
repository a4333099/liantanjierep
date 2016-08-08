using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Liantanjie.Controllers;
using Liantanjie.Models;

namespace Liantanjie.ViewPages
{
    public abstract class WebViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {
        public MallWebContext MallPageContext;

        public sealed override void InitHelpers()
        {
            base.InitHelpers();
            MallPageContext = ((BaseController)(this.ViewContext.Controller)).MallWebContext;
        }

        public sealed override void Write(object value)
        {
            Output.Write(value);
        }
    }

    /// <summary>
    /// PC前台视图页面基类型
    /// </summary>
    public abstract class WebViewPage : WebViewPage<dynamic>
    {
    }
}