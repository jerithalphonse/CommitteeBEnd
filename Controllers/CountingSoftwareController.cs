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
    public class CountingSoftware : Controller
    {
        private ICountingSoftwareService _iCountingSoftwareService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public CountingSoftware(
           ICountingSoftwareService iCountingSoftwareService,
           IMapper mapper,
           IOptions<AppSettings> appSettings)
        {
            _iCountingSoftwareService = iCountingSoftwareService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult GetAll()
        {
            var countingsoftwareusers = _iCountingSoftwareService.GetAll();
            var countingsoftwareusersDtos = _mapper.Map<IList<CountingSoftwareUsersDto>>(countingsoftwareusers);
            return Ok(countingsoftwareusersDtos);
        }
        [HttpGet("wilayat/{code}")]
        public OkObjectResult Get(string code)
        {
            var countingsoftwareusers = _iCountingSoftwareService.GetWilayatsByWilayatId(code);
            var countingsoftwareusersDtos = _mapper.Map<IList<CountingSoftwareUsersDto>>(countingsoftwareusers);
            return Ok(countingsoftwareusersDtos);
        }
        [HttpGet("wilayat/{code}/{roleid}")]
        public OkObjectResult GetWilayatsByWilayatIdRoleId(string code, int roleid)
        {
            var countingsoftwareusers = _iCountingSoftwareService.GetWilayatsByWilayatIdRoleId(code, roleid);
            var countingsoftwareusersDtos = _mapper.Map<IList<CountingSoftwareUsersDto>>(countingsoftwareusers);
            return Ok(countingsoftwareusersDtos);
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
