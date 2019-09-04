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
    public class KiosksAssignment : ControllerBase
    {
        private IKiosksAssignmentService _kiosksAssignmentService;
        private IUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public KiosksAssignment(
           IKiosksAssignmentService kiosksAssignmentService,
           IUserService userService,
           IMapper mapper,
           IOptions<AppSettings> appSettings)
        {
            _kiosksAssignmentService = kiosksAssignmentService;
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult GetAll()
        {
            var kiosksAssigned = _kiosksAssignmentService.GetAll();
            var kiosksAssignDtos = _mapper.Map<IList<KiosksAssignDto>>(kiosksAssigned);
            return Ok(kiosksAssignDtos);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        [HttpPost("assignUserToKiosks")]
        public IActionResult assignUserToKiosks([FromBody]KiosksAssign kiosksassign)
        {
            var kiosksAssigned = _kiosksAssignmentService.AssignKiosksToUser(kiosksassign);
            var userDto = _userService.GetById((int)kiosksassign.MemberId);
            var user = _mapper.Map<User>(userDto);
            try
            {
                user.KioskId = (int)kiosksassign.KioskId;
               _userService.Update(user, null);
                return Ok(kiosksAssigned);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
            
        }

        [HttpGet("wilayat/attendance/{id}")]
        public OkObjectResult GetUsersByWilayatsId(string id)
        {
            var kiosks = _kiosksAssignmentService.GetUsersByWilayatsId(id);
            //var kiosksDtos = _mapper.Map<IList<KiosksDto>>(kiosks);
            return Ok(kiosks);
        }


        [HttpGet("kiosks/{id}")]
        public OkObjectResult GetUsersByKiosksId(int id)
        {
            var kiosks = _kiosksAssignmentService.GetUsersByKiosksId(id);
            //var kiosksDtos = _mapper.Map<IList<KiosksDto>>(kiosks);
            return Ok(kiosks);
        }

        [HttpGet("pollingstation/{id}")]
        public OkObjectResult GetUsersByPollingStationId(int id)
        {
            var kiosks = _kiosksAssignmentService.GetUsersByPollingStationId(id);
            return Ok(kiosks);
        }


        [HttpPut("{id}")]
        public IActionResult updateUserToKiosks(int id, [FromBody]KiosksAssign kiosksassign)
        {
            //var kiosksAssigned = _kiosksAssignmentService.Update(id, kiosksassign);
            //var userDto = _userService.GetById((int)kiosksassign.MemberId);
            //var user = _mapper.Map<User>(userDto);
            //try
            //{
            //    user.KioskId = (int)kiosksassign.KioskId;
            //    _userService.Update(user, null);
            //    return Ok(kiosksAssigned);
            //}
            //catch (AppException ex)
            //{
            //    // return error message if there was an exception
            //    return BadRequest(new { message = ex.Message });
            //}
            var kiosksAssignDto = _kiosksAssignmentService.GetById(id);
            var kiosksAssigned = _mapper.Map<KiosksAssign>(kiosksAssignDto);

            var userDto = _userService.GetById((int)kiosksAssigned.MemberId);
            var user = _mapper.Map<User>(userDto);
            try
            {
                user.KioskId = null;
                _userService.Update(user, null);
                kiosksAssigned = (WebApi.Entities.KiosksAssign)_kiosksAssignmentService.Update(id, kiosksassign);
                userDto = _userService.GetById((int)kiosksassign.MemberId);
                user = _mapper.Map<User>(userDto);
                try
                {
                    user.KioskId = (int)kiosksassign.KioskId;
                    _userService.Update(user, null);
                    return Ok(kiosksAssigned);
                }
                catch (AppException ex)
                {
                    // return error message if there was an exception
                    return BadRequest(new { message = ex.Message });
                }
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
