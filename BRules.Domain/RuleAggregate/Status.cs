using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRules.Domain.RuleAggregate
{
    public enum Status
    {
        Proposed = 1,
        Implemented,
        Deprecated,
        Removed
    }
}
