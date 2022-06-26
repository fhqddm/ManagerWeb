using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Solo.Model
{
    public class IndexInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }     
        public string IndexCode { get; set; }
        public string IndexName { get; set; }
        public int IndexValua { get; set; }// -2低估,-1较为低估,0适中,1较为高估,2高估
        public double PE { get; set; }
        public double PEP100 { get; set; }//PE百分位
        public double PB { get; set; }
        public double PBP100 { get; set; }//PB百分位
        public double GXL { get; set; }//股息率
        public double ROE { get; set; }        
        public string DjCode { get; set; }//蛋卷估值代码

    }
}
