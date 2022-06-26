using System;
using System.Collections.Generic;
using System.Text;

namespace Solo.Model.ViewModel
{
    public class FundDetailView
    {
        public Array xStr { get; set; }
        public Array dataY { get; set; }
        public Array dataYV1 { get; set; }
        public Array dataYV2 { get; set; }
        public Array dataYV3 { get; set; }
        public Array dataYV4 { get; set; }
        public Array dataYVM2 { get; set; }
        public Array dataYVM1 { get; set; }
        public Array dataYVD10 { get; set; }
        public Array dataYVD5 { get; set; }
        public double Linear1 { get; set; }
        public double Linear2 { get; set; }
        public double Linear3 { get; set; }
        public double Linear5 { get; set; }

        public Array dataPoint { get; set; }

        public FundView fundView { get; set; }

        public int CurrentHold { get; set; }

    }
}
