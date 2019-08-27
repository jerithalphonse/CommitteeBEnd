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
    public class PollingStation : ControllerBase
    {
        private IPollingStationService _pollingStationService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public PollingStation(
           IPollingStationService pollingStationService,
           IMapper mapper,
           IOptions<AppSettings> appSettings)
        {
            _pollingStationService = pollingStationService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult GetAll()
        {
            var pollingStations = _pollingStationService.GetAll();
            var pollingStationsDtos = _mapper.Map<IList<PollingStationsDto>>(pollingStations);
            return Ok(pollingStationsDtos);
        }
        // GET api/values/5
        [HttpGet("wilayat/{id}")]
        public OkObjectResult Get(string id)
        {
            var pollingStations = _pollingStationService.GetPollingStationsByWilayatsId(id);
            var pollingStationsDtos = _mapper.Map<IList<PollingStationsDto>>(pollingStations);
            return Ok(pollingStationsDtos);
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
