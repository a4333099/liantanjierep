using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LiantanjieCommon;
using LiantanjieModel;


namespace Liantanjie.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            Utils.SetSid();
            return View();
        }

    }
}
