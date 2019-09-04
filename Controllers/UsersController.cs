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
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;
        private IHostingEnvironment _env;

        public UsersController(
            IUserService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings,
            IHostingEnvironment env)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _env = env;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]UserDto userDto)
        {
            var user = _userService.Authenticate(userDto.Username, userDto.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info (without password) and token to store client side
            return Ok(new {
                Id = user.Id,
                Username = user.Username,
                NameEnglish = user.NameEnglish,
                NameArabic = user.NameArabic,
                ImageUrl = user.ImageUrl,
                Phone = user.Phone,
                Email = user.Email,
                PollingStationId = user.PollingStationId,
                WilayatCode = user.WilayatCode,
                CommiteeType = user.CommiteeType,
                KioskId = user.KioskId,
                RoleId = user.RoleId,
                GovernorateCode = user.GovernorateCode,
                Roles = user.Roles,
                Wilayat = user.Wilayat,
                PollingStation = user.PollingStation,
                Kiosks = user.Kiosks,
                Token = tokenString,
                Governorate = user.Governorate
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]UserDto userDto)
        {
            // map dto to entity
            var user = _mapper.Map<User>(userDto);
            try 
            {
                // save 
                _userService.Create(user, userDto.Password);
                return Ok();
            } 
            catch(AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPost("assignpollingstationusers")]
        public IActionResult assignPollingStationUsers([FromBody]List<UserDto> users)
        {
            try
            {
                foreach (var tempuser in users)
                {
                    var user = _mapper.Map<User>(tempuser);
                    // save 
                    _userService.Update(user, null);
                }
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
            return Ok();
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var users =  _userService.GetAll();
            var userDtos = _mapper.Map<IList<UserDto>>(users);
            return Ok(userDtos);
        }

        // GET api/values/5
        [HttpGet("pollingstation/{id}")]
        public OkObjectResult Get(int id)
        {
            var users = _userService.GetUsersByPollingStationId(id);
            var userDtos = _mapper.Map<IList<UserDto>>(users);
            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user =  _userService.GetById(id);
            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [HttpGet("wilayat/{code}")]
        public IActionResult GetUsersUnderPollingStation(string code)
        {
            var users = _userService.GetUsersByWilayatId(code);
            var userDtos = _mapper.Map<IList<UserDto>>(users);
            return Ok(userDtos);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]UserDto userDto)
        {
            // map dto to entity and set id
            var user = _mapper.Map<User>(userDto);
            user.Id = id;

            try 
            {
                // save 
                _userService.Update(user, userDto.Password);
                return Ok();
            } 
            catch(AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok();
        }


        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            var FilesUploaded = new List<string>();
            long size = files.Sum(f => f.Length);

            // extract only the filename

            // store the file inside ~/App_Data/uploads folder
            var webRoot = _env.WebRootPath;
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;

            System.IO.DirectoryInfo DirInfo = new System.IO.DirectoryInfo(webRoot + "/" + userId);

            if (DirInfo.Exists == false)
            {
                System.IO.Directory.CreateDirectory(webRoot + "/" + userId);
            }
            foreach (var formFile in files)
            {
                var file = System.IO.Path.Combine(webRoot + "/" + userId, formFile.FileName);
                FilesUploaded.Add("http://192.168.43.9:4000" + "/" + userId + "/" + formFile.FileName);
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(file, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            // process uploaded files
            // Don't rely on or trust the FileName property without validation.
           
            return Ok(new { count = files.Count, size, FilesUploaded });
        }
    }
}
