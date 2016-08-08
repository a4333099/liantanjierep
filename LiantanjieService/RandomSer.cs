using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiantanjieCommon;
using LiantanjieCommon.Config;


namespace LiantanjieService
{
    public class RandomSer
    {
        static  RandomSer()
        {
            Randoms.RandomLibrary = TotalConfigs.MallConfig.Randomlib.ToCharArray(); ;
        }

        public static string CreateRandomValue(int length, bool onlyNumber)
        {
         return    Randoms.CreateRandomValue(length, onlyNumber);
        }

        public static RandomImage CreateRandomImage(string value, int imageWidth, int imageHeight, Color imageBGColor,
            Color imageTextColor1, Color imageTextColor2)
        {
            return Randoms.CreateRandomImage(value, imageWidth, imageHeight, imageBGColor, imageTextColor1,
                imageTextColor2);
        }
    }
}
