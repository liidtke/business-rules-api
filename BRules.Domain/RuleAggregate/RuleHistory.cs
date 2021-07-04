using BRules.Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRules.Domain.RuleAggregate
{
    public record RuleHistory : DomainEntity
    {
        public Rule Rule { get; private set; }

        public RuleHistory()
        {
            Rule = new Rule();
        }

        public RuleHistory(Rule rule, string user)
        {
            this.CreatedBy = user;
            this.CreationDate = DateTime.UtcNow;
            this.Rule = rule;
        }
    }
}
