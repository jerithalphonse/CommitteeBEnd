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
    public class Witnesses : ControllerBase
    {
        private IWitnessService _witnessService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public Witnesses(
           IWitnessService witnessService,
           IMapper mapper,
           IOptions<AppSettings> appSettings)
        {
            _witnessService = witnessService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult GetAll()
        {
            var witnesses = _witnessService.GetAll();
            var witnessDtos = _mapper.Map<IList<WitnessDto>>(witnesses);
            return Ok(witnessDtos);
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet("wilayat/{id}")]
        public OkObjectResult GetWitnessByWilayatsId(string id)
        {
            var witness = _witnessService.GetWitnessByWilayatsId(id);
            //var kiosksDtos = _mapper.Map<IList<KiosksDto>>(kiosks);
            return Ok(witness);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]WitnessDto witness)
        {
            // map dto to entity
            var witnessdata = _mapper.Map<Witness>(witness);
            try
            {
                // save 
                var witnessResponse = _witnessService.Create(witnessdata);
                return Ok(witnessResponse);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
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
