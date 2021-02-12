using BRules.Domain.SharedKernel;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRules.Domain.RuleAggregate
{
    public class CreateRuleService
    {
        private readonly IRepository<Rule> repository;
        private readonly IRepository<Area> repositoryArea;

        public CreateRuleService(IRepository<Rule> repository, IRepository<Area> repositoryArea)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.repositoryArea = repositoryArea ?? throw new ArgumentNullException(nameof(repositoryArea));
        }

        public async Task<Result<Rule>> Handle(Rule rule)
        {
            rule.Id = null;

            Area? area = await repositoryArea.Find(rule.AreaId);
            if (area == null)
            {
                return Result.Fail("Area is required");
            }

            //TODO: history
            var (code, newArea) = Area.GetNextCode(area);
            
            rule = rule with { Code = code };

            //TODO: error correction
            await repository.Add(rule);
            await repositoryArea.Update(newArea);

            return Result.Ok(rule);
        }
    }
}
