using BRules.Domain.SharedKernel;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRules.Domain.RuleAggregate
{
    public class AlterRuleService
    {
        private readonly IRepository<Rule> repository;
        private readonly IRepository<Area> repositoryArea;

        public AlterRuleService(IRepository<Rule> repository, IRepository<Area> repositoryArea)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.repositoryArea = repositoryArea ?? throw new ArgumentNullException(nameof(repositoryArea));
        }

        public async Task<Result<Rule>> Handle(Rule input)
        {
            Rule rule = await repository.Find(input.Id!);
            if (rule == null)
            {
                return Result.Fail("Not found");
            }

            if (rule.AreaId != input.AreaId)
            {
                var areaExists = await repositoryArea.Exists(input.AreaId);
                if (!areaExists)
                {
                    return Result.Fail("Area is required");
                }
            }

            if (String.IsNullOrEmpty(input.Code))
            {
                return Result.Fail("Code is required");
            }

            rule = rule with
            {
                AreaId = input.AreaId,
                Status = input.Status,
                Text = input.Text,
                Title = input.Title,
                Code = input.Code,
                Tags = input.Tags,
            };

            //TODO: histórico
            

            await repository.Update(rule);

            return Result.Ok(rule);
        }
    }
}
