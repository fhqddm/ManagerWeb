using System;
using System.Collections.Generic;
using System.Text;

namespace Solo.Model.ViewModel
{
    public class DiagnosisFund
    {
        public string FundNo { get; set; }
        public string FundName { get; set; }
        public string Strategy { get; set; }
        public double R1year { get; set; }
        public double R2year { get; set; }
        public double R3year { get; set; }
        public double R5year { get; set; }
        public Nullable<double> D_Top { get; set; }
        public double Sharp { get; set; }
        public double Maxretra { get; set; }
        public double Stddev { get; set; }
        public int UserId { get; set; }
        public int Position { get; set; }

    }
}
