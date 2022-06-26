using System;
using System.Collections.Generic;
using System.Text;

namespace Solo.Model.ViewModel
{
    public class DailyRateView
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Tag { get; set; }
        public double My { get; set; }
        public double XYG { get; set; }
        public double KJ { get; set; }
        public double Alex_My { get; set; }
        public double Alex { get; set; }
        public double HS300 { get; set; }
        public double CYB { get; set; }
        public double WYBCG { get; set; }
    }
}
