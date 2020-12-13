using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrayonTest.Models
{
    public class ExchageRateListModel
    {
        public Dictionary<string, Rates> rates { get; set; }
        public string start_at { get; set; }
        public string Base { get; set; }
        public string end_at { get; set; }
    }

}
