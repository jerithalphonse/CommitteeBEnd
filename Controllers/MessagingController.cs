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
    public class Messaging : ControllerBase
    {
        private IMessagingService _messagingService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public Messaging(
           IMessagingService messagingService,
           IMapper mapper,
           IOptions<AppSettings> appSettings)
        {
            _messagingService = messagingService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult GetAll()
        {
            var messaging = _messagingService.GetAll();
            var messagingDtos = _mapper.Map<IList<MessagingDto>>(messaging);
            return Ok(messagingDtos);
        }
        [HttpGet("wilayat/{id}")]
        public OkObjectResult GetMessagesByWilayatsId(string id)
        {
            var messages = _messagingService.GetMessagingByWilayatsId(id);
            var messagingDtos = _mapper.Map<IList<MessagingDto>>(messages);
            return Ok(messagingDtos);
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost("wilayat")]
        public IActionResult Post([FromBody]MessagingDto messaging)
        {
            // map dto to entity
            var messagingdata = _mapper.Map<MessagingModel>(messaging);
            try
            {
                // save 
                var messagingResponse = _messagingService.Create(messagingdata);
                return Ok(messagingResponse);
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
