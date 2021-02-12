using BRules.Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BRules.Domain.RuleAggregate
{
    public record Rule : DomainEntity
    {
        public string Code { get; init; } = "";
        public string Title { get; init; } = "";
        public string Text { get; init; } = "";
        public string AreaId { get; init; } = "";
        public Status Status { get; init; }

        public List<string> HistoryIds { get; set; } = new List<string>();
        public List<string> Tags { get; set; } = new List<string>();
    }

}
