using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WebApi.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using WebApi.Services;
using WebApi.Dtos;
using WebApi.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BankingDetails : Controller
    {
        private IBankingDetailsService _iBankingDetailsService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public BankingDetails(
           IBankingDetailsService IBankingDetailsService,
           IMapper mapper,
           IOptions<AppSettings> appSettings)
        {
            _iBankingDetailsService = IBankingDetailsService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult GetAll()
        {
            var bankDetails = _iBankingDetailsService.GetAll();
            var bankDetailsDtos = _mapper.Map<IList<BankDetailsDto>>(bankDetails);
            return Ok(bankDetailsDtos);
        }
        [HttpGet("{userid}")]
        public IActionResult GetById(int userid)
        {
            var bankDetails = _iBankingDetailsService.GetByUserId(userid);
            var bankDetailsDtos = _mapper.Map<IList<BankDetailsDto>>(bankDetails);
            return Ok(bankDetailsDtos);
        }

        [HttpPost]
        public IActionResult Post([FromBody]BankDetailsDto bankDetails)
        {
            // map dto to entity
            var bankDetailsEntity = _mapper.Map<BankDetails>(bankDetails);
            try
            {
                // save 
                _iBankingDetailsService.Create(bankDetailsEntity);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody]BankDetailsDto bankDetails)
        {
            // map dto to entity and set id
            var bankDetails1 = _mapper.Map<BankDetails>(bankDetails);
            try
            {
                // save 
                _iBankingDetailsService.Update(bankDetails1);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}



