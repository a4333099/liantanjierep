using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Liantanjie.Models
{
    public class Tanweibrief
    {
        public string TwId { get; set; }
        public string Title { get; set; }
        public string PicLoad { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public bool IsOnsale { get; set; }
    }
}