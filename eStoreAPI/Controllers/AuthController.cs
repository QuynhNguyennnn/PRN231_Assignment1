using AutoMapper;
using BusinessObject;
using DataAccess.Repository;
using DataAccess.Repository.Interface;
using eStoreAPI.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppSetting _appSetting;
        private IAuthRepository repository = new AuthRepository();
        public AuthController(IMapper mapper, IOptionsMonitor<AppSetting> optionsMonitor)
        {
            _mapper = mapper;
            _appSetting = optionsMonitor.CurrentValue;
        }

        [HttpPost("login")]
        public ActionResult<AuthResponse> Login(LoginDto loginDto)
        {
            AuthResponse authResponse = new AuthResponse();
            var userValidate = repository.Login(loginDto.Email, loginDto.Password);
            if (userValidate == false)
            {
                authResponse.Email = loginDto.Email;
                return NotFound("Email not found");
            }
            else
            {
                var memberInfo = repository.GetMemberByEmail(loginDto.Email);
                authResponse.Email = memberInfo.Email;
                authResponse.Password = memberInfo.Password;
                authResponse.MemberId = memberInfo.MemberId;
                authResponse.Roles = memberInfo.Roles;
                return Ok(authResponse);
            }
        }

/*        [HttpPost("register")]
        public ActionResult<AuthResponse> Register(RegisterDto registerDto)
        {
            AuthResponse authResponse = new AuthResponse();
            var userValidate = repository.GetMemberByEmail(registerDto.Email);

            if (userValidate == null)
            {
                authResponse.Email = registerDto.Email;
                return NotFound("Email adready exists");
            }
            else
            {
                Member mem = _mapper.Map<Member>(registerDto);
                var memberInfo = repository.Register(mem);
                authResponse.Email = userValidate.Email;
                authResponse.Password = userValidate.Password;
                authResponse.MemberId = userValidate.MemberId;
                authResponse.Roles = userValidate.Roles;
                return Ok(authResponse);
            }
        }*/

    }
}
