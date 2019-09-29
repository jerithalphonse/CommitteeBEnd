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
    public class Kiosks : ControllerBase
    {
        private IKiosksService _kiosksService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public Kiosks(
           IKiosksService kiosksService,
           IMapper mapper,
           IOptions<AppSettings> appSettings)
        {
            _kiosksService = kiosksService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult GetAll()
        {
            var kiosks = _kiosksService.GetAll();
            var kiosksDtos = _mapper.Map<IList<KiosksDto>>(kiosks);
            return Ok(kiosksDtos);
        }

        [HttpGet("serial/{id}")]
        public OkObjectResult GetBySerialId(string id)
        {
            
            var kiosks = _kiosksService.GetBySerialId(id);
            //var kiosksDto = _mapper.Map<KiosksDto>(kiosks);
            return Ok(kiosks);
        }

        // GET api/values/5
        [HttpGet("wilayat/{id}")]
        public OkObjectResult Get(string id)
        {
            var kiosks = _kiosksService.GetKiosksByWilayatsId(id);
            var kiosksDtos = _mapper.Map<IList<KiosksDto>>(kiosks);
            return Ok(kiosksDtos);
        }

        [HttpGet("wilayat/assigned/{id}")]
        public OkObjectResult GetKiosksAssignedByWilayatsId(string id)
        {
            var kiosks = _kiosksService.GetKiosksAssignedByWilayatsId(id);
            //var kiosksDtos = _mapper.Map<IList<KiosksDto>>(kiosks);
            return Ok(kiosks);
        }
        [HttpGet("governorate/assigned/{id}")]
        public OkObjectResult GetKiosksAssignedByGovernorateId(string id)
        {
            var kiosks = _kiosksService.GetKiosksAssignedByGovernorateId(id);
            //var kiosksDtos = _mapper.Map<IList<KiosksDto>>(kiosks);
            return Ok(kiosks);
        }
        [HttpGet("assigned")]
        public OkObjectResult GetKiosksAssignedAll()
        {
            var kiosks = _kiosksService.GetKiosksAssignedAll();
            //var kiosksDtos = _mapper.Map<IList<KiosksDto>>(kiosks);
            return Ok(kiosks);
        }
        [HttpGet("wilayat/kioskslockedstats/{id}")]
        public OkObjectResult GetKiosksLockedStatusByWilayatsId(string id)
        {
            var kiosks = _kiosksService.GetKiosksLockedStatusByWilayatsId(id);
            //var kiosksDtos = _mapper.Map<IList<KiosksDto>>(kiosks);
            return Ok(kiosks);
        }

        [HttpGet("pollingStation/{id}")]
        public OkObjectResult GetKiosksByPollingStationId(int id)
        {
            var kiosks = _kiosksService.GetKiosksByPollingStationId(id);
            //var kiosksDtos = _mapper.Map<IList<KiosksDto>>(kiosks);
            return Ok(kiosks);
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
