using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Solo.Common
{
    public class MyUtils
    {
        public static bool HasChinese(string str)
        {
            return Regex.IsMatch(str, @"[\u4e00-\u9fa5]");
        }
    }
}
