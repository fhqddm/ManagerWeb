using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Solo.Model
{
    public class MyStock
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string StockNo {get;set;}
        public string StockName { get; set; }
        public int UserId { get; set; }
        

    }
}
