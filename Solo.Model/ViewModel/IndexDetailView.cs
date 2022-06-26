using System;
using System.Collections.Generic;
using System.Text;

namespace Solo.Model.ViewModel
{
    public class IndexDetailView
    {
        public List<List<string>> close { get; set; }
        public List<List<string>> a20 { get; set; }
        public List<List<string>> a60 { get; set; }
        public List<List<string>> a240 { get; set; }
        public List<List<string>> trend { get; set; }
    }
}
