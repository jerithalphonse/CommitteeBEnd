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
    public class Wilayats : ControllerBase
    {
        private IWilayatService _wilayatService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public Wilayats(
           IWilayatService wilayatService,
           IMapper mapper,
           IOptions<AppSettings> appSettings)
        {
            _wilayatService = wilayatService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult GetAll()
        {
            var wilayats = _wilayatService.GetAll();
            var wilayatsDtos = _mapper.Map<IList<WilayatsDto>>(wilayats);
            return Ok(wilayatsDtos);
        }
        // GET api/values/5
        [HttpGet("governorate/{id}")]
        public OkObjectResult Get(string id)
        {
            var wilayats = _wilayatService.GetWilayatsByGovernorateId(id);
            var wilayatsDtos = _mapper.Map<IList<WilayatsDto>>(wilayats);
            return Ok(wilayatsDtos);
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
