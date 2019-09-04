using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using WebApi.Helpers;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using WebApi.Services;
using WebApi.Dtos;
using WebApi.Entities;


namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class Governorates : ControllerBase
    {
        private IGovernoratesService _governoratesService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public Governorates(
           IGovernoratesService governoratesService,
           IMapper mapper,
           IOptions<AppSettings> appSettings)
        {
            _governoratesService = governoratesService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult GetAll()
        {
            var governorates = _governoratesService.GetAll();
            var governoratesDtos = _mapper.Map<IList<GovernoratesDto>>(governorates);
            return Ok(governoratesDtos);
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
