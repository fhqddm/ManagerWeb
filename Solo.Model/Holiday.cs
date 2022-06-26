using System;
using System.Collections.Generic;
using System.Text;

namespace Solo.Model
{
    public class Holiday
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int HolidayType { get; set; }// 1为假期 2为非假期
    }
}
