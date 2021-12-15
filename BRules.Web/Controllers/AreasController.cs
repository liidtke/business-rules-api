using BRules.Domain.RuleAggregate;
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
    public class AreasController : ControllerBase
    {
        private readonly IRepository<Area> repositoryArea;
        private readonly IMapper mapper;

        public AreasController(IRepository<Area> repositoryArea, IMapper mapper)
        {
            this.repositoryArea = repositoryArea ?? throw new ArgumentNullException(nameof(repositoryArea));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

       
        [HttpGet]
        public async Task<IActionResult> GetAreas()
        {
            var areas = await this.repositoryArea.Get();
            var areaModels = mapper.Map<List<AreaModel>>(areas);
            areaModels = areaModels.OrderBy(x => x.Name).ToList();
            return Ok(areaModels);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] Area area)
        {
            await repositoryArea.Add(area);
            return Ok(mapper.Map<AreaModel>(area));
        }


        [HttpPut]
        public async Task<IActionResult> Alter([FromBody] Area area)
        {
            await repositoryArea.Update(area);
            return Ok(mapper.Map<AreaModel>(area));
        }
    }
}
