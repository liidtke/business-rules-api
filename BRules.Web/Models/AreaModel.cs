using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRules.Models
{
    public class AreaModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Prefix { get; set; }
        public int StartNumber { get; set; } = 0;
        public int CurrentNumber { get; set; } = 0;
        public List<RuleModel> Rules { get; set; }
    }
}
