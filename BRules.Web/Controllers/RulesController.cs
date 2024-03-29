﻿using BRules.Domain.RuleAggregate;
using BRules.Domain.SharedKernel;
using BRules.Models;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRules.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class RulesController : ControllerBase
    {
        private readonly IRepository<Rule> repository;
        private readonly IMapper mapper;

        public RulesController(IRepository<Rule> repository, IMapper mapper)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public IActionResult GetRules([FromQuery] string areaId, [FromQuery] int count = 100)
        {
            var query = this.repository.Query();
            if (!string.IsNullOrEmpty(areaId))
            {
                query = query.Where(x => x.AreaId == areaId);
            }
            else
            {
                query = query.Take(count);
            }

            var rules = query.ToList();
            var model = this.mapper.Map<List<RuleModel>>(rules);
            return Ok(model);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(string id)
        {
            var rules = await this.repository.Get(x => x.Id == id);
            var rule = rules.FirstOrDefault();
            if(rule == null)
            {
                return NotFound();
            }
            else
            {
                var model = this.mapper.Map<RuleModel>(rule);
                return Ok(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] RuleModel ruleModel, [FromServices] CreateRuleService service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var rule = this.mapper.Map<Rule>(ruleModel);

                var result = await service.Handle(rule);
                if (result.IsSuccess)
                {
                    return Created("", mapper.Map<RuleModel>(result.Value));
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            catch (FormatException ex)
            {
                return BadRequest("Error parsing data " + ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Alter([FromBody] RuleModel ruleModel, [FromServices] AlterRuleService service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var rule = this.mapper.Map<Rule>(ruleModel);

                var result = await service.Handle(rule);
                if (result.IsSuccess)
                {
                    return Ok(mapper.Map<RuleModel>(rule));
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            catch (FormatException ex)
            {
                return BadRequest("Error parsing data " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await this.repository.Remove(id);
            }
            catch (FormatException)
            {
                return BadRequest("Invalid Id");
            }
            catch (MongoDB.Driver.MongoException)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet("{id}/History")]
        public async Task<IActionResult> GetHistory(string id, [FromServices] IRepository<RuleHistory> repositoryHistory)
        {
            var history = await repositoryHistory.Get(x => x.Rule.Id == id);
            return Ok(history);
        }

    }
}
