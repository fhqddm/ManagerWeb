using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Solo.Model
{
    public class DailyRate
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
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
