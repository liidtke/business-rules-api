using BRules.Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRules.Domain.RuleAggregate
{
    public record Area : DomainEntity
    {
        public string Name { get; init; } = "";

        public string Prefix { get; init; } = "";
        public int StartNumber { get; init; } = 0;
        public int CurrentNumber { get; init; } = 0;

        public static Tuple<string, Area> GetNextCode(Area area)
        {
            Area newArea = area with
            {
                CurrentNumber = area.CurrentNumber == 0 
                    ? (area.StartNumber == 0 ? 1 : area.StartNumber) 
                    : area.CurrentNumber + 1,
            };

            var newCode = area.Prefix + newArea.CurrentNumber.ToString("D4");
            return new Tuple<string, Area>(newCode, newArea);
        }
    }
}
