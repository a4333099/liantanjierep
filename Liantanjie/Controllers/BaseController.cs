using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Liantanjie.Models;
using LiantanjieCommon;

namespace Liantanjie.Controllers
{
    public class BaseController : Controller
    {
        public MallWebContext MallWebContext;

       

        public BaseController()
        {
            MallWebContext = new MallWebContext();
          
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            this.ValidateRequest = false;
            MallWebContext.UpdateContext();

        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            MallWebContext.PromptLst.Clear();
        }


        
    }
}
