using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace bo.boomsecret.Common
{
    public static class HexHelper
    {


        public static string GetHexFromBytes(byte[] dataBytes)
        {
            return string.Concat(Array.ConvertAll(dataBytes, b => b.ToString("x2"))).ToLower();
        }






    }
}
