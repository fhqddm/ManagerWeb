using System;
using System.Collections.Generic;
using System.Text;

namespace Solo.Model.ViewModel
{
    public class FundPositionView
    {
        public int Id { get; set; }
        public string FundNo { get; set; }
        public string FundName { get; set; }
        public double XYG_Position { get; set; }
        public double KJ_Position { get; set; }
        public double ALEX_Position { get; set; }
        public double SuggestPosition { get; set; }
        public double MyPosition { get; set; }
        public double MyHolds { get; set; }
        public double Gap { get; set; }


    }
}
